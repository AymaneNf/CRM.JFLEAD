using CRM.JFOP.Domain;

namespace CRM.JFOP.Core
{
    public interface IOpportuniteRepository
    {
        Task<Opportunite?> AddOpportuniteAsync(Opportunite entity);
        Task<Opportunite?> UpdateOpportuniteAsync(Opportunite entity);
        Task<Opportunite?> DeleteOpportuniteAsync(Guid entityId);
        Task<Opportunite?> GetOpportuniteByIdAsync(Guid entityId);
        Task<IEnumerable<Opportunite>?> GetAllOpportunitesAsync();
        Task<IEnumerable<Opportunite>> SearchAsync(Func<Opportunite, bool> predicate);
    }
}
