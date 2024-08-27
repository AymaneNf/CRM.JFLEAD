using CRM.SharedKernel.Core;
using CRM.SharedKernel.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.SharedKernel.App
{
    public class GenericRepository<TEntity, TDbContext> : IGenericRepository<TEntity, TDbContext>
        where TEntity : BaseEntity
        where TDbContext : DbContext
    {
        private readonly TDbContext _context;
        private readonly ILogger<GenericRepository<TEntity, TDbContext>> _logger;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(TDbContext context, ILogger<GenericRepository<TEntity, TDbContext>> logger)
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
                _logger.LogInformation($"{typeof(TEntity).Name} with Id {entity.Id} created successfully");
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
                _logger.LogInformation($"{typeof(TEntity).Name} with Id {entity.Id} updated successfully");
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
                _logger.LogInformation($"{typeof(TEntity).Name} with Id {entity.Id} deleted successfully");
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

        public async Task<IEnumerable<TEntity>> SearchAsync(Func<TEntity, bool> predicate)
        {
            try
            {
                // Since predicate is a function, we need to bring all data in memory to apply it.
                // Be cautious with this in large datasets, prefer using IQueryable for large datasets.
                var allEntities = await _dbSet.ToListAsync();
                var filteredEntities = allEntities.Where(predicate);
                _logger.LogInformation($"{filteredEntities.Count()} entities of type {typeof(TEntity).Name} found based on search criteria");
                return filteredEntities;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while searching entities of type {typeof(TEntity).Name}");
                return Enumerable.Empty<TEntity>();
            }
        }
    }
}
