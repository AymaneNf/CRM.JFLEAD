using CRM.JFCL.Domain;

namespace CRM.JFCL.Core
{
    public interface IClientService
    {
        Task<Client?> CreateClientAsync(Client client);
        Task<Client?> UpdateClientAsync(Client client);
        Task<Client?> DeleteClientAsync(Guid clientId);
        Task<Client?> GetClientByIdAsync(Guid clientId);
        Task<IEnumerable<Client>?> GetAllClientsAsync();
        Task<IEnumerable<Client>?> SearchClientsAsync(string searchTerm);
        Task<Client?> DeactivateClientAsync(Guid clientId);
    }
}
