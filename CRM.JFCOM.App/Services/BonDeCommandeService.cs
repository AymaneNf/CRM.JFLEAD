using CRM.JFCOM.Core;
using CRM.JFCOM.Domain;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;


namespace CRM.JFCOM.App
{
    public class BonDeCommandeService : IBonDeCommandeService
    {
        private readonly IBonDeCommandeRepository _bonDeCommandeRepository;
        private readonly ILogger<BonDeCommandeService> _logger;

        public BonDeCommandeService(ILogger<BonDeCommandeService> logger, IBonDeCommandeRepository bonDeCommandeRepository)
        {
            _logger = logger;
            _bonDeCommandeRepository = bonDeCommandeRepository;
        }

        public async Task<BonDeCommande?> CreateBonDeCommandeAsync(BonDeCommande bonDeCommande)
        {
            try
            {
                bonDeCommande.Id = Guid.NewGuid();
                var createdBonDeCommande = await _bonDeCommandeRepository.AddBonDeCommandeAsync(bonDeCommande);
                return createdBonDeCommande;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the bon de commande");
                return null;
            }
        }

        public async Task<BonDeCommande?> UpdateBonDeCommandeAsync(BonDeCommande bonDeCommande)
        {
            try
            {
                var updatedBonDeCommande = await _bonDeCommandeRepository.UpdateBonDeCommandeAsync(bonDeCommande);
                if (updatedBonDeCommande != null)
                {
                    _logger.LogInformation($"Bon de commande updated with ID: {bonDeCommande.Id}");
                }
                return updatedBonDeCommande;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the bon de commande");
                return null;
            }
        }

        public async Task<BonDeCommande?> DeleteBonDeCommandeAsync(Guid bonDeCommandeId)
        {
            try
            {
                var deletedBonDeCommande = await _bonDeCommandeRepository.DeleteBonDeCommandeAsync(bonDeCommandeId);
                if (deletedBonDeCommande != null)
                {
                    _logger.LogInformation($"Bon de commande deleted with ID: {bonDeCommandeId}");
                }
                return deletedBonDeCommande;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the bon de commande");
                return null;
            }
        }

        public async Task<BonDeCommande?> GetBonDeCommandeByIdAsync(Guid bonDeCommandeId)
        {
            try
            {
                var bonDeCommande = await _bonDeCommandeRepository.GetBonDeCommandeByIdAsync(bonDeCommandeId);
                if (bonDeCommande == null)
                {
                    _logger.LogWarning($"Bon de commande not found with ID: {bonDeCommandeId}");
                }
                return bonDeCommande;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the bon de commande by ID");
                return null;
            }
        }

        public async Task<IEnumerable<BonDeCommande>?> GetAllBonDeCommandesAsync()
        {
            try
            {
                var bonDeCommandes = await _bonDeCommandeRepository.GetAllBonDeCommandesAsync();
                _logger.LogInformation($"Retrieved {bonDeCommandes?.Count() ?? 0} bon de commandes");
                return bonDeCommandes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all bon de commandes");
                return null;
            }
        }

        public async Task<IEnumerable<BonDeCommande>?> SearchBonDeCommandesAsync(Expression<Func<BonDeCommande, bool>> predicate)
        {
            try
            {
                var bonDeCommandes = await _bonDeCommandeRepository.SearchBonDeCommandesAsync(predicate);
                return bonDeCommandes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while searching for bon de commandes");
                return null;
            }
        }
    }
}
