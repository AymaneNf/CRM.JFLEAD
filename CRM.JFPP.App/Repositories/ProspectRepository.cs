using CRM.JFPP.App;
using CRM.JFPP.Core;
using CRM.JFPP.Domain;
using Microsoft.EntityFrameworkCore;


namespace CRM.JFPP.App
{
    public class ProspectRepository : IProspectRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Prospect> _dbSet;

        public ProspectRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Prospect>();
        }

        public async Task<Prospect?> AddProspectAsync(Prospect entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Prospect?> UpdateProspectAsync(Prospect entity)
        {
            var existingEntity = await _dbSet.FindAsync(entity.Id);
            if (existingEntity == null)
            {
                return null;
            }

            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return existingEntity;
        }

        public async Task<Prospect?> DeleteProspectAsync(Guid entityId)
        {
            var entity = await _dbSet.FindAsync(entityId);
            if (entity == null)
            {
                return null;
            }

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Prospect?> GetProspectByIdAsync(Guid entityId)
        {
            return await _dbSet.FindAsync(entityId);
        }

        public async Task<IEnumerable<Prospect>?> GetAllProspectsAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<Prospect>> SearchAsync(Func<Prospect, bool> predicate)
        {
            return await Task.FromResult(_dbSet.Where(predicate).ToList());
        }
    }
}
