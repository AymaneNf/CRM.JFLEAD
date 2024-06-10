using CRM.SharedKernel.Domain;

namespace CRM.SharedKernel.Core
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity?> AddAsync(TEntity entity);
        Task<TEntity?> UpdateAsync(TEntity entity);
        Task<TEntity?> DeleteAsync(Guid entityId);
        Task<TEntity?> GetByIdAsync(Guid entityId);
        Task<IEnumerable<TEntity>?> GetAllAsync();
    }
}
