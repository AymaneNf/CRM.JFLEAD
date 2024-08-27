using System.Linq.Expressions;
using CRM.JFCOM.Domain;

namespace CRM.JFCOM.Core
{
    public interface IDevisRepository
    {
        Task<Devis?> AddDevisAsync(Devis entity);
        Task<Devis?> UpdateDevisAsync(Devis entity);
        Task<Devis?> DeleteDevisAsync(Guid entityId);
        Task<Devis?> GetDevisByIdAsync(Guid entityId);
        Task<IEnumerable<Devis>?> GetAllDevisAsync();
        Task<IEnumerable<Devis>?> SearchDevisAsync(Expression<Func<Devis, bool>> predicate);
    }
}
