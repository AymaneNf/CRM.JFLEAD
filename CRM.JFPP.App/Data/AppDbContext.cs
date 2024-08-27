using CRM.JFPP.Domain;
using Microsoft.EntityFrameworkCore;

namespace CRM.JFPP.App
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Prospect> Prospects { get; set; }
    }
}
