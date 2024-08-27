using CRM.JFCOM.Domain;
using Microsoft.EntityFrameworkCore;

namespace CRM.JFCOM.App
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Define DbSet for TaskEvent entity
        public DbSet<Article> Articles { get; set; }
        public DbSet<Devis> Devis { get; set; }
        public DbSet<BonDeCommande> BonDeCommandes { get; set; }


    }
}
