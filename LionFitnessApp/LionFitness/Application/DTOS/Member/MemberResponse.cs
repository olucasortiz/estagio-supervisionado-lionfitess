namespace LionFitness.Application.DTOS.Member
{
    public record MemberResponse(
        int Id, 
        string Name, 
        string Cpf, 
        DateTime Birthdate, 
        string? PhotoUrl, 
        bool Active, 
        DateTime CreatedAt
        );
}
