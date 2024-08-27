using CRM.JFCL.Domain;

namespace CRM.JFCL.Core
{
    public interface IClientRepository
    {
        Task<Client?> AddClientAsync(Client entity);
        Task<Client?> UpdateClientAsync(Client entity);
        Task<Client?> DeleteClientAsync(Guid entityId);
        Task<Client?> GetClientByIdAsync(Guid entityId);
        Task<IEnumerable<Client>?> GetAllClientsAsync();
        Task<IEnumerable<Client>> SearchAsync(Func<Client, bool> predicate);
    }
}
