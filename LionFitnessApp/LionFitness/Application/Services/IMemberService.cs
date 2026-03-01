using LionFitness.Application.DTOS.Member;

namespace LionFitness.Application.Services
{
    public interface IMemberService
    {
        Task<MemberResponse> CreateAsync(MemberCreateRequest request, CancellationToken ct);
        Task<MemberResponse?> GetByIdAsync(int id, CancellationToken ct);
        Task<IReadOnlyList<MemberResponse>> GetAllAsync(CancellationToken ct);
        Task<bool> UpdateAsync(int id, MemberUpdateRequest request, CancellationToken ct);
        Task<bool> DeactivateAsync(int id, CancellationToken ct);
    }
}
