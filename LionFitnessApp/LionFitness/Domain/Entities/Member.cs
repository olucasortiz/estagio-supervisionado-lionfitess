    
namespace LionFitness.Domain.Entities
{
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public DateTime Birthdate { get; set; }
        public string? PhotoUrl { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;  

    }
}
