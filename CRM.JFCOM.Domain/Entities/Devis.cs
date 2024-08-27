using CRM.SharedKernel.Domain;

namespace CRM.JFCOM.Domain
{
    public class Devis : BaseEntity
    {
        public DateTime Date { get; set; } // Document date
        public Guid ClientId { get; set; } // Client associated with the quote
        public string NumeroPiece { get; set; } // Document number
        public List<DevisLigne> Lignes { get; set; } = new(); // List of items in the quote
        public decimal MontantTotalHT { get; set; } // Total amount excluding tax
        public decimal MontantTaxe { get; set; } // Total tax amount
        public decimal MontantTTC { get; set; } // Total amount including tax
    }

    public class DevisLigne
    {
        public string ReferenceArticle { get; set; } // Reference of the article
        public string Designation { get; set; } // Description
        public int Quantitee { get; set; } // Quantity
        public decimal PU { get; set; } // Unit Price
        public decimal Remise { get; set; } // Discount
        public decimal PUNet { get; set; } // Net unit price after discount
        public decimal Taxe { get; set; } // Tax amount
        public decimal TotalHTBrut { get; set; } // Total before discount
        public decimal TotalHTNet { get; set; } // Total after discount
    }
}
