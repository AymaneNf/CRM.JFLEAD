using CRM.JFLEAD.Core;
using CRM.JFLEAD.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CRM.JFLEAD.App
{
    public class LeadRepository : ILeadRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<LeadRepository> _logger;

        public LeadRepository(AppDbContext context, ILogger<LeadRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Lead?> AddLeadAsync(Lead entity)
        {
            try
            {
                await _context.Leads.AddAsync(entity);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Lead created with ID: {entity.Id}");
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the lead");
                return null;
            }
        }

        public async Task<Lead?> UpdateLeadAsync(Lead entity)
        {
            try
            {
                _context.Leads.Update(entity);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Lead updated with ID: {entity.Id}");
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the lead");
                return null;
            }
        }

        public async Task<Lead?> DeleteLeadAsync(Guid entityId)
        {
            try
            {
                var lead = await _context.Leads.FindAsync(entityId);
                if (lead == null)
                {
                    _logger.LogWarning($"Lead not found with ID: {entityId}");
                    return null;
                }
                _context.Leads.Remove(lead);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Lead deleted with ID: {entityId}");
                return lead;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the lead");
                return null;
            }
        }

        public async Task<Lead?> GetLeadByIdAsync(Guid entityId)
        {
            try
            {
                var lead = await _context.Leads.FindAsync(entityId);
                if (lead == null)
                {
                    _logger.LogWarning($"Lead not found with ID: {entityId}");
                }
                return lead;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the lead by ID");
                return null;
            }
        }

        public async Task<IEnumerable<Lead>?> GetAllLeadsAsync()
        {
            try
            {
                var leads = await _context.Leads.ToListAsync();
                _logger.LogInformation($"Retrieved {leads.Count} leads");
                return leads;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all leads");
                return null;
            }
        }
    }
}

