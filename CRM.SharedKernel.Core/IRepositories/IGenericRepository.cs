using CRM.SharedKernel.Domain;
using Microsoft.EntityFrameworkCore;

namespace CRM.SharedKernel.Core
{
    public interface IGenericRepository<TEntity, TDbContext>
        where TEntity : BaseEntity
        where TDbContext : DbContext
    {
        Task<TEntity?> AddAsync(TEntity entity);
        Task<TEntity?> UpdateAsync(TEntity entity);
        Task<TEntity?> DeleteAsync(Guid entityId);
        Task<TEntity?> GetByIdAsync(Guid entityId);
        Task<IEnumerable<TEntity>?> GetAllAsync();
        Task<IEnumerable<TEntity>> SearchAsync(Func<TEntity, bool> predicate); // New: For searching with a predicate
    }
}
