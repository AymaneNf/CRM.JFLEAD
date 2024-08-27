using System.Linq.Expressions;
using CRM.JFCOM.Domain;

namespace CRM.JFCOM.Core
{
    public interface IBonDeCommandeService
    {
        Task<BonDeCommande?> CreateBonDeCommandeAsync(BonDeCommande bonDeCommande);
        Task<BonDeCommande?> UpdateBonDeCommandeAsync(BonDeCommande bonDeCommande);
        Task<BonDeCommande?> DeleteBonDeCommandeAsync(Guid bonDeCommandeId);
        Task<BonDeCommande?> GetBonDeCommandeByIdAsync(Guid bonDeCommandeId);
        Task<IEnumerable<BonDeCommande>?> GetAllBonDeCommandesAsync();
        Task<IEnumerable<BonDeCommande>?> SearchBonDeCommandesAsync(Expression<Func<BonDeCommande, bool>> predicate);
    }
}
