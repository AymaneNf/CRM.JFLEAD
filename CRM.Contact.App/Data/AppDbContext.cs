using CRM.JFCT.Domain;
using Microsoft.EntityFrameworkCore;

namespace CRM.JFCT.App
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }
    }
}
