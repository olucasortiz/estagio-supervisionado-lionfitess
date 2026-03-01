namespace LionFitness.Application.DTOS.Member
{
    public record MemberUpdateRequest(
        string Name,
        DateTime Birthdate,
        string? PhotoUrl,
        bool IsActive
        );
}
