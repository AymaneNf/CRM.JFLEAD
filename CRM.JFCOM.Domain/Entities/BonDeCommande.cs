using CRM.SharedKernel.Domain;

namespace CRM.JFCOM.Domain
{
    public class BonDeCommande : BaseEntity
    {
        public DateTime Date { get; set; }
        public Guid ClientId { get; set; }
        public string NumeroPiece { get; set; }
        public List<BonDeCommandeLigne> Lignes { get; set; } = new();
        public decimal MontantTotalHT { get; set; }
        public decimal MontantTaxe { get; set; }
        public decimal MontantTTC { get; set; }
    }

    public class BonDeCommandeLigne
    {
        public string ReferenceArticle { get; set; }
        public string Designation { get; set; }
        public int Quantitee { get; set; }
        public decimal PU { get; set; }
        public decimal Remise { get; set; }
        public decimal PUNet { get; set; }
        public decimal Taxe { get; set; }
        public decimal TotalHTBrut { get; set; }
        public decimal TotalHTNet { get; set; }
    }
}
