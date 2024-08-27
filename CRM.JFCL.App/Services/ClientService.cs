using CRM.JFCL.Core;
using CRM.JFCL.Domain;
using Microsoft.Extensions.Logging;

namespace CRM.JFCL.App
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly ILogger<ClientService> _logger;

        public ClientService(ILogger<ClientService> logger, IClientRepository clientRepository)
        {
            _logger = logger;
            _clientRepository = clientRepository;
        }

        public async Task<Client?> CreateClientAsync(Client client)
        {
            try
            {
                client.Id = Guid.NewGuid();
                var createdClient = await _clientRepository.AddClientAsync(client);
                return createdClient;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the client");
                return null;
            }
        }

        public async Task<Client?> UpdateClientAsync(Client client)
        {
            try
            {
                var updatedClient = await _clientRepository.UpdateClientAsync(client);
                if (updatedClient != null)
                {
                    _logger.LogInformation($"Client updated with ID: {client.Id}");
                }
                return updatedClient;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the client");
                return null;
            }
        }

        public async Task<Client?> DeleteClientAsync(Guid clientId)
        {
            try
            {
                var deletedClient = await _clientRepository.DeleteClientAsync(clientId);
                if (deletedClient != null)
                {
                    _logger.LogInformation($"Client deleted with ID: {clientId}");
                }
                return deletedClient;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the client");
                return null;
            }
        }

        public async Task<Client?> GetClientByIdAsync(Guid clientId)
        {
            try
            {
                var client = await _clientRepository.GetClientByIdAsync(clientId);
                if (client == null)
                {
                    _logger.LogWarning($"Client not found with ID: {clientId}");
                }
                return client;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the client by ID");
                return null;
            }
        }

        public async Task<IEnumerable<Client>?> GetAllClientsAsync()
        {
            try
            {
                var clients = await _clientRepository.GetAllClientsAsync();
                _logger.LogInformation($"Retrieved {clients?.Count() ?? 0} clients");
                return clients;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all clients");
                return null;
            }
        }

        public async Task<IEnumerable<Client>?> SearchClientsAsync(string searchTerm)
        {
            try
            {
                if (string.IsNullOrEmpty(searchTerm))
                    return Enumerable.Empty<Client>();

                var clients = await _clientRepository.GetAllClientsAsync();

                if (clients == null)
                    return Enumerable.Empty<Client>();

                var searchResults = clients.Where(c =>
                    (c.Nom != null && c.Nom.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) ||
                    (c.Prenom != null && c.Prenom.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) ||
                    (c.Denomination != null && c.Denomination.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) ||
                    (c.Email != null && c.Email.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                ).ToList();

                _logger.LogInformation($"Found {searchResults.Count} clients matching search term '{searchTerm}'");
                return searchResults;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while searching for clients");
                return null;
            }
        }
        public async Task<Client?> DeactivateClientAsync(Guid clientId)
        {
            try
            {
                var client = await _clientRepository.GetClientByIdAsync(clientId);
                if (client == null)
                {
                    _logger.LogWarning($"Client not found with ID: {clientId}");
                    return null;
                }

                client.IsActive = false; // Mark the client as inactive
                var updatedClient = await _clientRepository.UpdateClientAsync(client);

                if (updatedClient != null)
                {
                    _logger.LogInformation($"Client with ID: {clientId} has been deactivated");
                }

                return updatedClient;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deactivating the client");
                return null;
            }
        }
    }
}
