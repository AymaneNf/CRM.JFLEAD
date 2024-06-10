using CRM.JFLEAD.Domain;

namespace CRM.JFLEAD.Core
{
    public interface ILeadRepository
    {
        Task<Lead?> AddLeadAsync(Lead entity);
        Task<Lead?> UpdateLeadAsync(Lead entity);
        Task<Lead?> DeleteLeadAsync(Guid entityId);
        Task<Lead?> GetLeadByIdAsync(Guid entityId);
        Task<IEnumerable<Lead>?> GetAllLeadsAsync();
    }
}
