namespace LionFitness.Application.DTOS.Member
{
    public record MemberCreateRequest(
        string Name, 
        string Cpf, 
        DateTime Birthdate, 
        string? PhotoUrl);
}
