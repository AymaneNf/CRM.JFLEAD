using CRM.JFLEAD.Domain;
using Microsoft.EntityFrameworkCore;

namespace CRM.JFLEAD.App
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Lead> Leads { get; set; }
    }
}
