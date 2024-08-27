using CRM.JFCT.Domain;

namespace CRM.JFCT.Core
{
    public interface IContactService
    {
        Task<Contact?> CreateContactAsync(Contact contact);
        Task<Contact?> UpdateContactAsync(Contact contact);
        Task<Contact?> DeleteContactAsync(Guid contactId);
        Task<Contact?> GetContactByIdAsync(Guid contactId);
        Task<IEnumerable<Contact>?> GetAllContactsAsync();
        Task<IEnumerable<Contact>?> SearchContactsAsync(string searchTerm);
    }
}