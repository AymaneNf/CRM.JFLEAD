using CRM.JFLEAD.Domain;

namespace CRM.JFLEAD.Core
{
    public interface ILeadService
    {
        Task<Lead?> CreateLeadAsync(Lead lead);
        Task<Lead?> UpdateLeadAsync(Lead lead);
        Task<Lead?> DeleteLeadAsync(Guid leadId);
        Task<Lead?> GetLeadByIdAsync(Guid leadId);
        Task<IEnumerable<Lead>?> GetAllLeadsAsync();
        Task AssignLeadAsync(Guid leadId, int collaboratorId);
        Task StartLeadAsync(Guid leadId);
        Task ConvertLeadToWonAsync(Guid leadId);
        Task MarkLeadAsLostAsync(Guid leadId);
        Task CreateEventFromLeadAsync(Guid leadId, string eventDetails);

    }
}
