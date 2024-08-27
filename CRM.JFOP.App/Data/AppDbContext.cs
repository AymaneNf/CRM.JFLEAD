using CRM.JFOP.Domain;
using Microsoft.EntityFrameworkCore;

namespace CRM.JFOP.App
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Opportunite> Opportunites { get; set; }

        
        
    }
}
