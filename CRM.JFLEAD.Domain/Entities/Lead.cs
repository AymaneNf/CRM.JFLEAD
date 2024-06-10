namespace CRM.JFLEAD.Domain
{
    public enum LeadType
    {
        Societe,
        Individu
    }
    public enum LeadStatus
    {
        Nouveau,
        Assigne,
        EnCours,
        ConvertiGagne,
        ConvertiPerdu
    }

    public class Lead
    {
        public int Id { get; set; }
        public LeadType Type { get; set; }
        public string? Denomination { get; set; } // For Societe
        public string? Civilite { get; set; } // For Individu
        public string? Nom { get; set; }
        public string? Prenom { get; set; }
        public string? Adresse { get; set; }
        public string? CodePostale { get; set; }
        public string? Ville { get; set; }
        public string? Region { get; set; }
        public string? Paye { get; set; }
        public string? TelephoneFixe { get; set; }
        public string? Fax { get; set; }
        public string? Email { get; set; }
        public string? SiteWeb { get; set; }
        public string? Categorie { get; set; }
        public string? SecteurDActivite { get; set; }
        public string? Provenance { get; set; }
        public LeadStatus Status { get; set; }
        public int AssignedTo { get; set; }
    }
}
