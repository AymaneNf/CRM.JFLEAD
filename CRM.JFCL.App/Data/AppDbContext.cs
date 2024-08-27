using CRM.JFCL.Domain;
using Microsoft.EntityFrameworkCore;

namespace CRM.JFCL.App
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
    }
}
