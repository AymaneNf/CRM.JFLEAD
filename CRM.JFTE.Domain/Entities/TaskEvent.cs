using CRM.SharedKernel.Domain;

namespace CRM.JFTE.Domain
{
    public class TaskEvent : BaseEntity
    {
        public string Nom { get; set; } // Task/Event Name
        public string Type { get; set; } // Type of Task/Event
        public DateTime DateHeureDebut { get; set; } // Planned Start DateTime
        public DateTime DateHeureFin { get; set; } // Planned End DateTime
        public string Description { get; set; } // Description of the Task/Event
        public string Lieu { get; set; } // Location
        public string CoordonneesGeographiques { get; set; } // Geographical Coordinates
        public string AssigneA { get; set; } // Assigned To
        public string Priorite { get; set; } // Priority
        public string CompteRendu { get; set; } // Report
        public Guid? ClientId { get; set; } // Related Client or Prospect ID
        public Guid? PisteId { get; set; } // Related Lead (Piste) ID
        public Guid? OpportuniteId { get; set; } // Related Opportunity ID
        public string PieceJointe { get; set; } // Attachment

        // Workflow States
        public bool IsCompleted { get; set; } // Status of the Task/Event
        public bool IsPending { get; set; } // If the task is in Pending state
        public bool IsCancelled { get; set; } // If the task is cancelled
    }
}
