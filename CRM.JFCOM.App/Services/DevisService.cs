using CRM.JFCOM.Core;
using CRM.JFCOM.Domain;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace CRM.JFCOM.App
{
    public class DevisService : IDevisService
    {
        private readonly IDevisRepository _devisRepository;
        private readonly ILogger<DevisService> _logger;

        public DevisService(ILogger<DevisService> logger, IDevisRepository devisRepository)
        {
            _logger = logger;
            _devisRepository = devisRepository;
        }

        public async Task<Devis?> CreateDevisAsync(Devis devis)
        {
            try
            {
                devis.Id = Guid.NewGuid();
                var createdDevis = await _devisRepository.AddDevisAsync(devis);
                return createdDevis;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the devis");
                return null;
            }
        }

        public async Task<Devis?> UpdateDevisAsync(Devis devis)
        {
            try
            {
                var updatedDevis = await _devisRepository.UpdateDevisAsync(devis);
                if (updatedDevis != null)
                {
                    _logger.LogInformation($"Devis updated with ID: {devis.Id}");
                }
                return updatedDevis;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the devis");
                return null;
            }
        }

        public async Task<Devis?> DeleteDevisAsync(Guid devisId)
        {
            try
            {
                var deletedDevis = await _devisRepository.DeleteDevisAsync(devisId);
                if (deletedDevis != null)
                {
                    _logger.LogInformation($"Devis deleted with ID: {devisId}");
                }
                return deletedDevis;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the devis");
                return null;
            }
        }

        public async Task<Devis?> GetDevisByIdAsync(Guid devisId)
        {
            try
            {
                var devis = await _devisRepository.GetDevisByIdAsync(devisId);
                if (devis == null)
                {
                    _logger.LogWarning($"Devis not found with ID: {devisId}");
                }
                return devis;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the devis by ID");
                return null;
            }
        }

        public async Task<IEnumerable<Devis>?> GetAllDevisAsync()
        {
            try
            {
                var devisList = await _devisRepository.GetAllDevisAsync();
                _logger.LogInformation($"Retrieved {devisList?.Count() ?? 0} devis");
                return devisList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all devis");
                return null;
            }
        }

        public async Task<IEnumerable<Devis>?> SearchDevisAsync(Expression<Func<Devis, bool>> predicate)
        {
            try
            {
                var devisList = await _devisRepository.SearchDevisAsync(predicate);
                return devisList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while searching for devis");
                return null;
            }
        }
    }
}
