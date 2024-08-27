using CRM.JFCT.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.JFCT.Core
{
    public interface IContactRepository
    {
        Task<Contact?> AddContactAsync(Contact entity);
        Task<Contact?> UpdateContactAsync(Contact entity);
        Task<Contact?> DeleteContactAsync(Guid entityId);
        Task<Contact?> GetContactByIdAsync(Guid entityId);
        Task<IEnumerable<Contact>?> GetAllContactsAsync();
        Task<IEnumerable<Contact>> SearchAsync(Func<Contact, bool> predicate); 
    }
}
