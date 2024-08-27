using CRM.JFPP.Domain;

namespace CRM.JFPP.Core
{
    public interface IProspectService
    {
        Task<Prospect?> CreateProspectAsync(Prospect prospect);
        Task<Prospect?> UpdateProspectAsync(Prospect prospect);
        Task<Prospect?> DeleteProspectAsync(Guid prospectId);
        Task<Prospect?> GetProspectByIdAsync(Guid prospectId);
        Task<IEnumerable<Prospect>?> GetAllProspectsAsync();
        Task<IEnumerable<Prospect>?> SearchProspectsAsync(string searchTerm);
        Task<Prospect?> ConvertToClientAsync(Guid prospectId); // Additional method for conversion
        Task<Prospect?> DeactivateProspectAsync(Guid prospectId);
    }
}
