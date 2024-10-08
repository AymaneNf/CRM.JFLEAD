using CRM.SharedKernel.Domain;

namespace CRM.JFLEAD.Domain
{

    public class Lead : BaseEntity
    {
        public LeadType Type { get; set; }
        public string? Denomination { get; set; } // For Societe
        public string? Civilite { get; set; } // For Individu
        public string? Nom { get; set; }
        public string? Prenom { get; set; }
        public string? Adresse { get; set; }
        public string? CodePostale { get; set; }
        public string? Ville { get; set; }
        public string? Region { get; set; }

        public string? Pays { get; set; }
        public string? Telephone { get; set; }

        public string? Fax { get; set; }
        public string? Email { get; set; }
        public string? SiteWeb { get; set; }
        public string? Categorie { get; set; }
        public string? SecteurActivite { get; set; }
        public string? Provenance { get; set; }
        public LeadStatus Status { get; set; }
        public int AssignedTo { get; set; }
    }
}
