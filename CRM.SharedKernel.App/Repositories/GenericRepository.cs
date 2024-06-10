using CRM.SharedKernel.Core;
using CRM.SharedKernel.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CRM.SharedKernel.App
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DbContext _context;
        private readonly ILogger<GenericRepository<TEntity>> _logger;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(DbContext context, ILogger<GenericRepository<TEntity>> logger)
        {
            _context = context;
            _logger = logger;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<TEntity?> AddAsync(TEntity entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while adding an entity of type {typeof(TEntity).Name}");
                return null;
            }
        }

        public async Task<TEntity?> UpdateAsync(TEntity entity)
        {
            try
            {
                _dbSet.Update(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating an entity of type {typeof(TEntity).Name}");
                return null;
            }
        }

        public async Task<TEntity?> DeleteAsync(Guid entityId)
        {
            try
            {
                var entity = await _dbSet.FindAsync(entityId);
                if (entity == null)
                {
                    _logger.LogWarning($"Entity of type {typeof(TEntity).Name} not found with ID: {entityId}");
                    return null;
                }
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting an entity of type {typeof(TEntity).Name}");
                return null;
            }
        }

        public async Task<TEntity?> GetByIdAsync(Guid entityId)
        {
            try
            {
                return await _dbSet.FindAsync(entityId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving an entity of type {typeof(TEntity).Name} by ID: {entityId}");
                return null;
            }
        }

        public async Task<IEnumerable<TEntity>?> GetAllAsync()
        {
            try
            {
                return await _dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving all entities of type {typeof(TEntity).Name}");
                return null;
            }
        }
    }
}