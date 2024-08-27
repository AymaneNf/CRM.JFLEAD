using CRM.JFOP.Domain;

namespace CRM.JFOP.Core
{
    public interface IOpportuniteService
    {
        Task<Opportunite?> CreateOpportuniteAsync(Opportunite opportunite);
        Task<Opportunite?> UpdateOpportuniteAsync(Opportunite opportunite);
        Task<Opportunite?> DeleteOpportuniteAsync(Guid opportuniteId);
        Task<Opportunite?> GetOpportuniteByIdAsync(Guid opportuniteId);
        Task<IEnumerable<Opportunite>?> GetAllOpportunitesAsync();
        Task<IEnumerable<Opportunite>?> SearchOpportunitesAsync(string searchTerm);

        // Workflow management methods
        Task<Opportunite?> MarkAsWonAsync(Guid opportuniteId);
        Task<Opportunite?> MarkAsLostAsync(Guid opportuniteId);
        Task<Opportunite?> MoveToNegotiationAsync(Guid opportuniteId);
        Task<Opportunite?> MoveToProposalAsync(Guid opportuniteId);
        Task<Opportunite?> CreateEventForOpportuniteAsync(Guid opportuniteId, string eventDetails);
    }
}
