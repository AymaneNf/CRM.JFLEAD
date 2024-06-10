using CRM.JFLEAD.Core;
using CRM.JFLEAD.Domain;
using Microsoft.Extensions.Logging;


namespace CRM.JFLEAD.App
{
    public class LeadService : ILeadService
    {
        private readonly ILeadRepository _leadRepository;
        private readonly ILogger<LeadService> _logger;

        public LeadService(ILogger<LeadService> logger, ILeadRepository leadRepository)
        {
            _logger = logger;
            _leadRepository = leadRepository;
        }

        public async Task<Lead?> CreateLeadAsync(Lead lead)
        {
            try
            {
                lead.Status = LeadStatus.Nouveau;
                var createdLead = await _leadRepository.AddLeadAsync(lead);
                if (createdLead != null)
                {
                    _logger.LogInformation($"Lead created with ID: {createdLead.Id}");
                }
                return createdLead;
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
                var updatedLead = await _leadRepository.UpdateLeadAsync(lead);
                if (updatedLead != null)
                {
                    _logger.LogInformation($"Lead updated with ID: {lead.Id}");
                }
                return updatedLead;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the lead");
                return null;
            }
        }

        public async Task<Lead?> DeleteLeadAsync(Guid leadId)
        {
            try
            {
                var deletedLead = await _leadRepository.DeleteLeadAsync(leadId);
                if (deletedLead != null)
                {
                    _logger.LogInformation($"Lead deleted with ID: {leadId}");
                }
                return deletedLead;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the lead");
                return null;
            }
        }

        public async Task<Lead?> GetLeadByIdAsync(Guid leadId)
        {
            try
            {
                var lead = await _leadRepository.GetLeadByIdAsync(leadId);
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
                var leads = await _leadRepository.GetAllLeadsAsync();
                _logger.LogInformation($"Retrieved {leads?.Count() ?? 0} leads");
                return leads;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all leads");
                return null;
            }
        }

        public async Task AssignLeadAsync(Guid leadId, int collaboratorId)
        {
            try
            {
                var lead = await _leadRepository.GetLeadByIdAsync(leadId);
                if (lead != null)
                {
                    lead.AssignedTo = collaboratorId;
                    lead.Status = LeadStatus.Assigne;
                    await _leadRepository.UpdateLeadAsync(lead);
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

        public async Task StartLeadAsync(Guid leadId)
        {
            try
            {
                var lead = await _leadRepository.GetLeadByIdAsync(leadId);
                if (lead != null)
                {
                    lead.Status = LeadStatus.EnCours;
                    await _leadRepository.UpdateLeadAsync(lead);
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

        public async Task ConvertLeadToWonAsync(Guid leadId)
        {
            try
            {
                var lead = await _leadRepository.GetLeadByIdAsync(leadId);
                if (lead != null)
                {
                    lead.Status = LeadStatus.ConvertiGagne;
                    await _leadRepository.UpdateLeadAsync(lead);
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

        public async Task MarkLeadAsLostAsync(Guid leadId)
        {
            try
            {
                var lead = await _leadRepository.GetLeadByIdAsync(leadId);
                if (lead != null)
                {
                    lead.Status = LeadStatus.ConvertiPerdu;
                    await _leadRepository.UpdateLeadAsync(lead);
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

        public async Task CreateEventFromLeadAsync(Guid leadId, string eventDetails)
        {
            try
            {
                var lead = await _leadRepository.GetLeadByIdAsync(leadId);
                if (lead != null)
                {
                    // Implement the event creation logic here
                    await _leadRepository.UpdateLeadAsync(lead);
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
