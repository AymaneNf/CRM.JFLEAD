namespace CRM.JFOP.App
{
    public class DatabaseManagement
    {
        private readonly AppDbContext _context;
        public DatabaseManagement(AppDbContext context)
        {
            _context = context;
        }

        async public Task InitDatabase()
        {
            await _context.Database.EnsureCreatedAsync();
        }
    }
}
