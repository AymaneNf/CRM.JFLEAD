using CRM.JFPP.Domain;


namespace CRM.JFPP.Core
{
    public interface IProspectRepository
    {
        Task<Prospect?> AddProspectAsync(Prospect entity);
        Task<Prospect?> UpdateProspectAsync(Prospect entity);
        Task<Prospect?> DeleteProspectAsync(Guid entityId);
        Task<Prospect?> GetProspectByIdAsync(Guid entityId);
        Task<IEnumerable<Prospect>?> GetAllProspectsAsync();
        Task<IEnumerable<Prospect>> SearchAsync(Func<Prospect, bool> predicate);

    }
}
