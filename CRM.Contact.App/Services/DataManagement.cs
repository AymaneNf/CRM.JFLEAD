namespace CRM.JFCT.App
{
    public class DataManagement
    {
        private readonly AppDbContext _context;
        public DataManagement(AppDbContext context)
        {
            _context = context;
        }

        async public Task InitDatabase()
        {
            await _context.Database.EnsureCreatedAsync();
        }
    }
}
