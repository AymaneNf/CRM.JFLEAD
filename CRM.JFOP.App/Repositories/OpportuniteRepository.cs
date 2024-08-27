using CRM.JFOP.Core;
using CRM.JFOP.Domain;
using Microsoft.EntityFrameworkCore;


namespace CRM.JFOP.App
{
    public class OpportuniteRepository : IOpportuniteRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Opportunite> _dbSet;

        public OpportuniteRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Opportunite>();
        }

        public async Task<Opportunite?> AddOpportuniteAsync(Opportunite entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Opportunite?> UpdateOpportuniteAsync(Opportunite entity)
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

        public async Task<Opportunite?> DeleteOpportuniteAsync(Guid entityId)
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

        public async Task<Opportunite?> GetOpportuniteByIdAsync(Guid entityId)
        {
            return await _dbSet.FindAsync(entityId);
        }

        public async Task<IEnumerable<Opportunite>?> GetAllOpportunitesAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<Opportunite>> SearchAsync(Func<Opportunite, bool> predicate)
        {
            return await Task.FromResult(_dbSet.Where(predicate).ToList());
        }
    }
}
