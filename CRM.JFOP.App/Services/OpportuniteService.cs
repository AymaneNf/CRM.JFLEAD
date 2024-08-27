using CRM.JFOP.Core;
using CRM.JFOP.Domain;
using Microsoft.Extensions.Logging;


namespace CRM.JFOP.App
{
    public class OpportuniteService : IOpportuniteService
    {
        private readonly IOpportuniteRepository _opportuniteRepository;
        private readonly ILogger<OpportuniteService> _logger;

        public OpportuniteService(ILogger<OpportuniteService> logger, IOpportuniteRepository opportuniteRepository)
        {
            _logger = logger;
            _opportuniteRepository = opportuniteRepository;
        }

        public async Task<Opportunite?> CreateOpportuniteAsync(Opportunite opportunite)
        {
            try
            {
                opportunite.Id = Guid.NewGuid();
                var createdOpportunite = await _opportuniteRepository.AddOpportuniteAsync(opportunite);
                return createdOpportunite;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the opportunity");
                return null;
            }
        }

        public async Task<Opportunite?> UpdateOpportuniteAsync(Opportunite opportunite)
        {
            try
            {
                var updatedOpportunite = await _opportuniteRepository.UpdateOpportuniteAsync(opportunite);
                if (updatedOpportunite != null)
                {
                    _logger.LogInformation($"Opportunity updated with ID: {opportunite.Id}");
                }
                return updatedOpportunite;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the opportunity");
                return null;
            }
        }

        public async Task<Opportunite?> DeleteOpportuniteAsync(Guid opportuniteId)
        {
            try
            {
                var deletedOpportunite = await _opportuniteRepository.DeleteOpportuniteAsync(opportuniteId);
                if (deletedOpportunite != null)
                {
                    _logger.LogInformation($"Opportunity deleted with ID: {opportuniteId}");
                }
                return deletedOpportunite;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the opportunity");
                return null;
            }
        }

        public async Task<Opportunite?> GetOpportuniteByIdAsync(Guid opportuniteId)
        {
            try
            {
                var opportunite = await _opportuniteRepository.GetOpportuniteByIdAsync(opportuniteId);
                if (opportunite == null)
                {
                    _logger.LogWarning($"Opportunity not found with ID: {opportuniteId}");
                }
                return opportunite;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the opportunity by ID");
                return null;
            }
        }

        public async Task<IEnumerable<Opportunite>?> GetAllOpportunitesAsync()
        {
            try
            {
                var opportunites = await _opportuniteRepository.GetAllOpportunitesAsync();
                _logger.LogInformation($"Retrieved {opportunites?.Count() ?? 0} opportunities");
                return opportunites;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all opportunities");
                return null;
            }
        }

        public async Task<IEnumerable<Opportunite>?> SearchOpportunitesAsync(string searchTerm)
        {
            try
            {
                if (string.IsNullOrEmpty(searchTerm))
                    return Enumerable.Empty<Opportunite>();

                var opportunites = await _opportuniteRepository.GetAllOpportunitesAsync();

                if (opportunites == null)
                    return Enumerable.Empty<Opportunite>();

                var searchResults = opportunites.Where(o =>
                    o.Nom.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    o.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
                ).ToList();

                _logger.LogInformation($"Found {searchResults.Count} opportunities matching search term '{searchTerm}'");
                return searchResults;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while searching for opportunities");
                return null;
            }
        }

        public async Task<Opportunite?> MarkAsWonAsync(Guid opportuniteId)
        {
            try
            {
                var opportunite = await _opportuniteRepository.GetOpportuniteByIdAsync(opportuniteId);
                if (opportunite == null)
                {
                    _logger.LogWarning($"Opportunity not found with ID: {opportuniteId}");
                    return null;
                }

                opportunite.Statut = "Fermé Gagnée";
                var updatedOpportunite = await _opportuniteRepository.UpdateOpportuniteAsync(opportunite);
                _logger.LogInformation($"Opportunity with ID: {opportuniteId} marked as won");
                return updatedOpportunite;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while marking the opportunity as won");
                return null;
            }
        }

        public async Task<Opportunite?> MarkAsLostAsync(Guid opportuniteId)
        {
            try
            {
                var opportunite = await _opportuniteRepository.GetOpportuniteByIdAsync(opportuniteId);
                if (opportunite == null)
                {
                    _logger.LogWarning($"Opportunity not found with ID: {opportuniteId}");
                    return null;
                }

                opportunite.Statut = "Fermé perdue";
                var updatedOpportunite = await _opportuniteRepository.UpdateOpportuniteAsync(opportunite);
                _logger.LogInformation($"Opportunity with ID: {opportuniteId} marked as lost");
                return updatedOpportunite;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while marking the opportunity as lost");
                return null;
            }
        }

        public async Task<Opportunite?> MoveToNegotiationAsync(Guid opportuniteId)
        {
            try
            {
                var opportunite = await _opportuniteRepository.GetOpportuniteByIdAsync(opportuniteId);
                if (opportunite == null)
                {
                    _logger.LogWarning($"Opportunity not found with ID: {opportuniteId}");
                    return null;
                }

                opportunite.Statut = "Negociation";
                var updatedOpportunite = await _opportuniteRepository.UpdateOpportuniteAsync(opportunite);
                _logger.LogInformation($"Opportunity with ID: {opportuniteId} moved to negotiation");
                return updatedOpportunite;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while moving the opportunity to negotiation");
                return null;
            }
        }

        public async Task<Opportunite?> MoveToProposalAsync(Guid opportuniteId)
        {
            try
            {
                var opportunite = await _opportuniteRepository.GetOpportuniteByIdAsync(opportuniteId);
                if (opportunite == null)
                {
                    _logger.LogWarning($"Opportunity not found with ID: {opportuniteId}");
                    return null;
                }

                opportunite.Statut = "Proposition";
                var updatedOpportunite = await _opportuniteRepository.UpdateOpportuniteAsync(opportunite);
                _logger.LogInformation($"Opportunity with ID: {opportuniteId} moved to proposal");
                return updatedOpportunite;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while moving the opportunity to proposal");
                return null;
            }
        }

        public async Task<Opportunite?> CreateEventForOpportuniteAsync(Guid opportuniteId, string eventDetails)
        {
            try
            {
                var opportunite = await _opportuniteRepository.GetOpportuniteByIdAsync(opportuniteId);
                if (opportunite == null)
                {
                    _logger.LogWarning($"Opportunity not found with ID: {opportuniteId}");
                    return null;
                }

                // Implement event creation logic here
                // Example: opportunite.Events.Add(new Event { Details = eventDetails, Date = DateTime.Now });

                var updatedOpportunite = await _opportuniteRepository.UpdateOpportuniteAsync(opportunite);
                _logger.LogInformation($"Event created for opportunity with ID: {opportuniteId}");
                return updatedOpportunite;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating an event for the opportunity");
                return null;
            }
        }
    }
}
