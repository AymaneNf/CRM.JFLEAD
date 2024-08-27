using CRM.JFCOM.Core;
using CRM.JFCOM.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CRM.JFCOM.App
{
    public class BonDeCommandeRepository : IBonDeCommandeRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<BonDeCommande> _dbSet;

        public BonDeCommandeRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<BonDeCommande>();
        }

        public async Task<BonDeCommande?> AddBonDeCommandeAsync(BonDeCommande entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<BonDeCommande?> UpdateBonDeCommandeAsync(BonDeCommande entity)
        {
            var existingEntity = await _dbSet.FindAsync(entity.Id);
            if (existingEntity == null) return null;

            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return existingEntity;
        }

        public async Task<BonDeCommande?> DeleteBonDeCommandeAsync(Guid entityId)
        {
            var entity = await _dbSet.FindAsync(entityId);
            if (entity == null) return null;

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<BonDeCommande?> GetBonDeCommandeByIdAsync(Guid entityId)
        {
            return await _dbSet.FindAsync(entityId);
        }

        public async Task<IEnumerable<BonDeCommande>?> GetAllBonDeCommandesAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<BonDeCommande>?> SearchBonDeCommandesAsync(Expression<Func<BonDeCommande, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }
    }
}
