using System.Linq.Expressions;
using CRM.JFCOM.Domain;

namespace CRM.JFCOM.Core
{
    public interface IDevisService
    {
        Task<Devis?> CreateDevisAsync(Devis devis);
        Task<Devis?> UpdateDevisAsync(Devis devis);
        Task<Devis?> DeleteDevisAsync(Guid devisId);
        Task<Devis?> GetDevisByIdAsync(Guid devisId);
        Task<IEnumerable<Devis>?> GetAllDevisAsync();
        Task<IEnumerable<Devis>?> SearchDevisAsync(Expression<Func<Devis, bool>> predicate);
    }
}
