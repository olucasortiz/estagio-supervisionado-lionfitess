namespace LionFitness.Domain.Entities
{
    public class Plan
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public PlanType Type { get; set; }
        public decimal Price { get; set; }
        public int DurationDays { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }

        public Plan() {}
    }
}
