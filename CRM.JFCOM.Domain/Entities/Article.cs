using CRM.SharedKernel.Domain;

namespace CRM.JFCOM.Domain
{
    public class Article : BaseEntity
    {
        public string Reference { get; set; } // Unique reference code
        public string Designation { get; set; } // Name or description of the article
        public string Famille { get; set; } // Family or category
        public decimal PrixVente { get; set; } // Selling price
        public decimal PrixAchat { get; set; } // Purchase price
        public string UniteVente { get; set; } // Sales unit
        public decimal TauxTaxe { get; set; } // Default tax rate
        public string Photo { get; set; } // Photo URL or path
        public string DocumentJoint { get; set; } // Attached document URL or path

        // Additional properties if needed
        public int Stock { get; set; } // Stock quantity
    }
}
