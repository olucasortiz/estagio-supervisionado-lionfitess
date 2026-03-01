using LionFitness.Application.DTOS.Member;
using LionFitness.Domain.Entities;

namespace LionFitness.Application.Mappers
{
    public static class MemberMapper
    {
        public static MemberResponse ToResponse(this Member member) => new(
            member.Id,
            member.Name,
            member.Cpf,
            member.Birthdate,
            member.PhotoUrl,
            member.IsActive,
            member.CreatedAt
        );
    }
}
