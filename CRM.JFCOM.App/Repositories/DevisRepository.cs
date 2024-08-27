using CRM.JFCOM.Core;
using CRM.JFCOM.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CRM.JFCOM.App
{
    public class DevisRepository : IDevisRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Devis> _dbSet;

        public DevisRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Devis>();
        }

        public async Task<Devis?> AddDevisAsync(Devis entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Devis?> UpdateDevisAsync(Devis entity)
        {
            var existingEntity = await _dbSet.FindAsync(entity.Id);
            if (existingEntity == null) return null;

            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return existingEntity;
        }

        public async Task<Devis?> DeleteDevisAsync(Guid entityId)
        {
            var entity = await _dbSet.FindAsync(entityId);
            if (entity == null) return null;

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Devis?> GetDevisByIdAsync(Guid entityId)
        {
            return await _dbSet.FindAsync(entityId);
        }

        public async Task<IEnumerable<Devis>?> GetAllDevisAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<Devis>?> SearchDevisAsync(Expression<Func<Devis, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }
    }
}
