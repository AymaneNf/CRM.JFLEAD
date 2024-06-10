using CRM.JFLEAD.Domain;

namespace CRM.JFLEAD.Core
{
    public interface ILeadService
    {
        Task<Lead?> CreateLeadAsync(Lead lead);
        Task<Lead?> UpdateLeadAsync(Lead lead);
        Task<Lead?> DeleteLeadAsync(int leadId);
        Task<Lead?> GetLeadByIdAsync(int leadId);
        Task<IEnumerable<Lead>?> GetAllLeadsAsync();
        Task AssignLeadAsync(int leadId, int collaboratorId);
        Task StartLeadAsync(int leadId);
        Task ConvertLeadToWonAsync(int leadId);
        Task MarkLeadAsLostAsync(int leadId);
        Task CreateEventFromLeadAsync(int leadId, string eventDetails);
    }
}
