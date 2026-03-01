
using LionFitness.Application.DTOS.Member;
using LionFitness.Application.Exceptions;
using LionFitness.Application.Mappers;
using LionFitness.Domain.Entities;
using LionFitness.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using ZstdSharp;


namespace LionFitness.Application.Services
{
    public class MemberService
    {
        public readonly AppDbContext _db;

        public MemberService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<MemberResponse?> GetByIdAsync(int id, CancellationToken ct)
        {
            var member = await _db.Members
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id, ct);
            return member?.ToResponse();
        }

        public async Task<MemberResponse> CreateAsync(MemberCreateRequest request, CancellationToken ct)
        {
            var cpfExists = await _db.Members.AnyAsync(m => m.Cpf == request.Cpf, ct);
            if (cpfExists)
                throw new DuplicateCpfException(request.Cpf);
            var member = new Member
            {
                Name = request.Name,
                Cpf = request.Cpf,
                Birthdate = request.Birthdate,
                PhotoUrl = request.PhotoUrl,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };
            _db.Members.Add(member);
            await _db.SaveChangesAsync(ct);
            return member.ToResponse();
        }

        public async Task<IReadOnlyList<MemberResponse>> GetAllAsync(CancellationToken ct)
        {
            var members = await _db.Members
                .AsNoTracking()
                .ToListAsync(ct);
            return members.Select(m => m.ToResponse()).ToList();
        }

        public async Task<bool> UpdateAsync(int id, MemberUpdateRequest request, CancellationToken ct)
        {
            var member = await _db.Members
                .FirstOrDefaultAsync(m => m.Id == id, ct);

            if (member is null)
                return false;

            member.Name = request.Name;
            member.Birthdate = request.Birthdate;
            member.PhotoUrl = request.PhotoUrl;
            member.IsActive = request.IsActive;

            await _db.SaveChangesAsync(ct);
            return true;
        }
        public async Task<bool> DeactivateAsync(int id, CancellationToken ct)
        {
            var member = await _db.Members
                .FirstOrDefaultAsync(m => m.Id == id, ct);

            if (member is null)
                return false;

            member.IsActive = false;

            await _db.SaveChangesAsync(ct);
            return true;
        }
    }
}
