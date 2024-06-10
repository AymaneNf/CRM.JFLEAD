using CRM.JFLEAD.Core;
using CRM.JFLEAD.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace CRM.JFLEAD.App
{
    public class LeadService : ILeadService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<LeadService> _logger;

        public LeadService(AppDbContext context, ILogger<LeadService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Lead?> CreateLeadAsync(Lead lead)
        {
            try
            {
                lead.Status = LeadStatus.Nouveau;
                _context.Leads.Add(lead);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Lead created with ID: {lead.Id}");
                return lead;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the lead");
                return null;
            }
        }

        public async Task<Lead?> UpdateLeadAsync(Lead lead)
        {
            try
            {
                _context.Leads.Update(lead);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Lead updated with ID: {lead.Id}");
                return lead;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the lead");
                return null;
            }
        }

        public async Task<Lead?> DeleteLeadAsync(int leadId)
        {
            try
            {
                var lead = await _context.Leads.FindAsync(leadId);
                if (lead == null)
                {
                    _logger.LogWarning($"Lead not found with ID: {leadId}");
                    return null;
                }
                _context.Leads.Remove(lead);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Lead deleted with ID: {leadId}");
                return lead;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the lead");
                return null;
            }
        }

        public async Task<Lead?> GetLeadByIdAsync(int leadId)
        {
            try
            {
                var lead = await _context.Leads.FindAsync(leadId);
                if (lead == null)
                {
                    _logger.LogWarning($"Lead not found with ID: {leadId}");
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

        public async Task AssignLeadAsync(int leadId, int collaboratorId)
        {
            try
            {
                var lead = await _context.Leads.FindAsync(leadId);
                if (lead != null)
                {
                    lead.AssignedTo = collaboratorId;
                    lead.Status = LeadStatus.Assigne;
                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"Lead with ID: {leadId} assigned to collaborator with ID: {collaboratorId}");
                }
                else
                {
                    _logger.LogWarning($"Lead not found with ID: {leadId}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while assigning the lead");
            }
        }

        public async Task StartLeadAsync(int leadId)
        {
            try
            {
                var lead = await _context.Leads.FindAsync(leadId);
                if (lead != null)
                {
                    lead.Status = LeadStatus.EnCours;
                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"Lead with ID: {leadId} started");
                }
                else
                {
                    _logger.LogWarning($"Lead not found with ID: {leadId}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while starting the lead");
            }
        }

        public async Task ConvertLeadToWonAsync(int leadId)
        {
            try
            {
                var lead = await _context.Leads.FindAsync(leadId);
                if (lead != null)
                {
                    lead.Status = LeadStatus.ConvertiGagne;
                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"Lead with ID: {leadId} converted to won");
                }
                else
                {
                    _logger.LogWarning($"Lead not found with ID: {leadId}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while converting the lead to won");
            }
        }

        public async Task MarkLeadAsLostAsync(int leadId)
        {
            try
            {
                var lead = await _context.Leads.FindAsync(leadId);
                if (lead != null)
                {
                    lead.Status = LeadStatus.ConvertiPerdu;
                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"Lead with ID: {leadId} marked as lost");
                }
                else
                {
                    _logger.LogWarning($"Lead not found with ID: {leadId}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while marking the lead as lost");
            }
        }

        public async Task CreateEventFromLeadAsync(int leadId, string eventDetails)
        {
            try
            {
                var lead = await _context.Leads.FindAsync(leadId);
                if (lead != null)
                {
                    // Implement the event creation logic here
                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"Event created from lead with ID: {leadId}");
                }
                else
                {
                    _logger.LogWarning($"Lead not found with ID: {leadId}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating an event from the lead");
            }
        }
    }
}
