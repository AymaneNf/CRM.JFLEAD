using CRM.SharedKernel.Domain;

namespace CRM.JFOP.Domain
{
    public class Opportunite : BaseEntity
    {
        public string Nom { get; set; } // Name of the opportunity
        public Guid? ClientId { get; set; } // Related client ID
        public string TypeDeBesoin { get; set; } // Type of need: New/Existing/Recurring
        public string Provenance { get; set; } // Origin of the opportunity
        public DateTime DatePrevueCloture { get; set; } // Expected closing date
        public int Probabilite { get; set; } // Probability percentage
        public decimal Montant { get; set; } // Amount
        public string Description { get; set; } // Description of the opportunity
        public string Statut { get; set; } // Status: New, Proposal, Negotiation, Closed Won, Closed Lost
        public Guid? AssigneA { get; set; } // Assigned to (User ID)
    }
}
