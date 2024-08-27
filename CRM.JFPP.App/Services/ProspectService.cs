using CRM.JFCL.App;
using CRM.JFCL.Core;
using CRM.JFCL.Domain;
using CRM.JFPP.Core;
using CRM.JFPP.Domain;
using Microsoft.Extensions.Logging;

namespace CRM.JFPP.App
{
    public class ProspectService : IProspectService
    {
        private readonly IProspectRepository _prospectRepository;
        private readonly ILogger<ProspectService> _logger;
        private readonly IClientService _clientService;

        public ProspectService(ILogger<ProspectService> logger, IProspectRepository prospectRepository, IClientService clientService)
        {
            _logger = logger;
            _prospectRepository = prospectRepository;
            _clientService = clientService;
        }

        public async Task<Prospect?> CreateProspectAsync(Prospect prospect)
        {
            try
            {
                prospect.Id = Guid.NewGuid();
                var createdProspect = await _prospectRepository.AddProspectAsync(prospect);
                return createdProspect;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the prospect");
                return null;
            }
        }

        public async Task<Prospect?> UpdateProspectAsync(Prospect prospect)
        {
            try
            {
                var updatedProspect = await _prospectRepository.UpdateProspectAsync(prospect);
                if (updatedProspect != null)
                {
                    _logger.LogInformation($"Prospect updated with ID: {prospect.Id}");
                }
                return updatedProspect;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the prospect");
                return null;
            }
        }

        public async Task<Prospect?> DeleteProspectAsync(Guid prospectId)
        {
            try
            {
                var deletedProspect = await _prospectRepository.DeleteProspectAsync(prospectId);
                if (deletedProspect != null)
                {
                    _logger.LogInformation($"Prospect deleted with ID: {prospectId}");
                }
                return deletedProspect;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the prospect");
                return null;
            }
        }

        public async Task<Prospect?> GetProspectByIdAsync(Guid prospectId)
        {
            try
            {
                var prospect = await _prospectRepository.GetProspectByIdAsync(prospectId);
                if (prospect == null)
                {
                    _logger.LogWarning($"Prospect not found with ID: {prospectId}");
                }
                return prospect;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the prospect by ID");
                return null;
            }
        }

        public async Task<IEnumerable<Prospect>?> GetAllProspectsAsync()
        {
            try
            {
                var prospects = await _prospectRepository.GetAllProspectsAsync();
                _logger.LogInformation($"Retrieved {prospects?.Count() ?? 0} prospects");
                return prospects;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all prospects");
                return null;
            }
        }

        public async Task<IEnumerable<Prospect>?> SearchProspectsAsync(string searchTerm)
        {
            try
            {
                if (string.IsNullOrEmpty(searchTerm))
                    return Enumerable.Empty<Prospect>();

                var prospects = await _prospectRepository.GetAllProspectsAsync();

                if (prospects == null)
                    return Enumerable.Empty<Prospect>();

                var searchResults = prospects.Where(p =>
                    (p.Nom != null && p.Nom.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) ||
                    (p.Prenom != null && p.Prenom.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) ||
                    (p.Denomination != null && p.Denomination.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) ||
                    (p.Email != null && p.Email.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                ).ToList();

                _logger.LogInformation($"Found {searchResults.Count} prospects matching search term '{searchTerm}'");
                return searchResults;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while searching for prospects");
                return null;
            }
        }

        public async Task<Prospect?> ConvertToClientAsync(Guid prospectId)
        {
            try
            {
                // Retrieve the prospect by its ID
                var prospect = await _prospectRepository.GetProspectByIdAsync(prospectId);
                if (prospect == null)
                {
                    _logger.LogWarning($"Prospect not found with ID: {prospectId}");
                    return null;
                }

                // Create a new client based on the prospect's details
                var client = new Client
                {
                    Id = Guid.NewGuid(),
                    Type = prospect.Type,
                    Denomination = prospect.Denomination,
                    Civilite = prospect.Civilite,
                    Nom = prospect.Nom,
                    Prenom = prospect.Prenom,
                    Adresse = prospect.Adresse,
                    CodePostale = prospect.CodePostale,
                    Ville = prospect.Ville,
                    Region = prospect.Region,
                    Paye = prospect.Paye,
                    TelephoneFixe = prospect.TelephoneFixe,
                    Fax = prospect.Fax,
                    Email = prospect.Email,
                    SiteWeb = prospect.SiteWeb,
                    Categorie = prospect.Categorie,
                    SecteurActivite = prospect.SecteurActivite,
                    Provenance = prospect.Provenance,
                    Contacts = prospect.Contacts,
                    Opportunites = prospect.Opportunites,
                    Evenements = prospect.Evenements
                };

                // Save the new client using the injected ClientService instance
                var createdClient = await _clientService.CreateClientAsync(client);
                if (createdClient != null)
                {
                    _logger.LogInformation($"Prospect with ID: {prospectId} converted to client with ID: {createdClient.Id}");
                }

                // Optionally, deactivate or mark the prospect as converted
                prospect.IsActive = false; // Optionally deactivate the prospect after conversion
                await _prospectRepository.UpdateProspectAsync(prospect);

                return prospect; // Return the original prospect, possibly with updated status
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while converting the prospect to client");
                return null;
            }
        }

        public async Task<Prospect?> DeactivateProspectAsync(Guid prospectId)
        {
            try
            {
                var prospect = await _prospectRepository.GetProspectByIdAsync(prospectId);
                if (prospect == null)
                {
                    _logger.LogWarning($"Prospect not found with ID: {prospectId}");
                    return null;
                }

                prospect.IsActive = false; // Mark the prospect as inactive
                var updatedProspect = await _prospectRepository.UpdateProspectAsync(prospect);

                if (updatedProspect != null)
                {
                    _logger.LogInformation($"Prospect with ID: {prospectId} has been deactivated");
                }

                return updatedProspect;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deactivating the prospect");
                return null;
            }
        }
    }
}
