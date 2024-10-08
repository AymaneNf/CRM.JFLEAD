﻿using CRM.JFCT.Domain;
using CRM.JFOP.Domain;
using CRM.JFTE.Domain;
using CRM.SharedKernel.Domain;

namespace CRM.JFPP.Domain
{
    public class Prospect : BaseEntity
    {
        public string? Type { get; set; } // Societe or Individu
        public string? Denomination { get; set; } // If Societe
        public string? Civilite { get; set; } // If Individu
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
        public string? SecteurActivite { get; set; }
        public string? Provenance { get; set; }
        public bool IsActive { get; set; } = true;
        public ICollection<Contact>? Contacts { get; set; } // Assuming a Prospect can have multiple contacts
        public ICollection<Opportunite>? Opportunites { get; set; } // List of opportunities
        public ICollection<TaskEvent>? Evenements { get; set; } // List of events
    }
}
