using CRM.JFTE.Domain;
using Microsoft.EntityFrameworkCore;

namespace CRM.JFTE.App
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Define DbSet for TaskEvent entity
        public DbSet<TaskEvent> TaskEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure TaskEvent entity
            modelBuilder.Entity<TaskEvent>(entity =>
            {
                entity.HasKey(e => e.Id); // Primary key

                // Property configurations
                entity.Property(e => e.Nom)
                      .IsRequired()
                      .HasMaxLength(255);

                entity.Property(e => e.Type)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.DateHeureDebut)
                      .IsRequired();

                entity.Property(e => e.DateHeureFin)
                      .IsRequired();

                entity.Property(e => e.Description)
                      .HasMaxLength(1000);

                entity.Property(e => e.Lieu)
                      .HasMaxLength(255);

                entity.Property(e => e.CoordonneesGeographiques)
                      .HasMaxLength(255);

                entity.Property(e => e.AssigneA)
                      .HasMaxLength(255);

                entity.Property(e => e.Priorite)
                      .HasMaxLength(50);

                entity.Property(e => e.CompteRendu)
                      .HasMaxLength(2000);

                entity.Property(e => e.PieceJointe)
                      .HasMaxLength(500);

                // Relationships and indexes can be configured here if needed
                // For example, if TaskEvent has foreign keys to other entities:
                // entity.HasOne(e => e.Client)
                //       .WithMany(c => c.TaskEvents)
                //       .HasForeignKey(e => e.ClientId)
                //       .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
