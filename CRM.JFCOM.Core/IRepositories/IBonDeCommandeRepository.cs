using System.Linq.Expressions;
using CRM.JFCOM.Domain;

namespace CRM.JFCOM.Core
{
    public interface IBonDeCommandeRepository
    {
        Task<BonDeCommande?> AddBonDeCommandeAsync(BonDeCommande entity);
        Task<BonDeCommande?> UpdateBonDeCommandeAsync(BonDeCommande entity);
        Task<BonDeCommande?> DeleteBonDeCommandeAsync(Guid entityId);
        Task<BonDeCommande?> GetBonDeCommandeByIdAsync(Guid entityId);
        Task<IEnumerable<BonDeCommande>?> GetAllBonDeCommandesAsync();
        Task<IEnumerable<BonDeCommande>?> SearchBonDeCommandesAsync(Expression<Func<BonDeCommande, bool>> predicate);
    }
}
