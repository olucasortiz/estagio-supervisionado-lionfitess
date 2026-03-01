using Microsoft.EntityFrameworkCore;
using LionFitness.Domain.Entities;

namespace LionFitness.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Member> Members => Set<Member>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Member>(entity =>
            {
                entity.ToTable("Members");
                entity.HasKey(a => a.Id);
                entity.Property(a => a.Name).IsRequired().HasMaxLength(100);
                entity.Property(a => a.Cpf).IsRequired().HasMaxLength(11);
                entity.HasIndex(a => a.Cpf).IsUnique();
                entity.Property(a => a.Birthdate).IsRequired();
                entity.Property(a => a.PhotoUrl).HasMaxLength(500);
                entity.Property(a => a.IsActive).HasDefaultValue(true);
                entity.Property(a => a.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            });
        }

    }
}
