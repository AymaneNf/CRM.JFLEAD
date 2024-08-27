using CRM.JFCT.Core;
using CRM.JFCT.Domain;
using Microsoft.Extensions.Logging;

namespace CRM.JFCT.App
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        private readonly ILogger<ContactService> _logger;

        public ContactService(ILogger<ContactService> logger, IContactRepository contactRepository)
        {
            _logger = logger;
            _contactRepository = contactRepository;
        }

        public async Task<Contact?> CreateContactAsync(Contact contact)
        {
            try
            {
                contact.Id = Guid.NewGuid();
                var createdContact = await _contactRepository.AddContactAsync(contact);
                return createdContact;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the contact");
                return null;
            }
        }

        public async Task<Contact?> UpdateContactAsync(Contact contact)
        {
            try
            {
                var updatedContact = await _contactRepository.UpdateContactAsync(contact);
                if (updatedContact != null)
                {
                    _logger.LogInformation($"Contact updated with ID: {contact.Id}");
                }
                return updatedContact;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the contact");
                return null;
            }
        }

        public async Task<Contact?> DeleteContactAsync(Guid contactId)
        {
            try
            {
                var deletedContact = await _contactRepository.DeleteContactAsync(contactId);
                if (deletedContact != null)
                {
                    _logger.LogInformation($"Contact deleted with ID: {contactId}");
                }
                return deletedContact;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the contact");
                return null;
            }
        }

        public async Task<Contact?> GetContactByIdAsync(Guid contactId)
        {
            try
            {
                var contact = await _contactRepository.GetContactByIdAsync(contactId);
                if (contact == null)
                {
                    _logger.LogWarning($"Contact not found with ID: {contactId}");
                }
                return contact;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the contact by ID");
                return null;
            }
        }

        public async Task<IEnumerable<Contact>?> GetAllContactsAsync()
        {
            try
            {
                var contacts = await _contactRepository.GetAllContactsAsync();
                _logger.LogInformation($"Retrieved {contacts?.Count() ?? 0} contacts");
                return contacts;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all contacts");
                return null;
            }
        }

        public async Task<IEnumerable<Contact>?> SearchContactsAsync(string searchTerm)
        {
            try
            {
                if (string.IsNullOrEmpty(searchTerm))
                    return Enumerable.Empty<Contact>();

                var contacts = await _contactRepository.GetAllContactsAsync();

                if (contacts == null)
                    return Enumerable.Empty<Contact>();

                var searchResults = contacts.Where(c =>
                    (c.Nom != null && c.Nom.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) ||
                    (c.Prenom != null && c.Prenom.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) ||
                    (c.Entreprise != null && c.Entreprise.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) ||
                    (c.Email != null && c.Email.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                ).ToList();

                _logger.LogInformation($"Found {searchResults.Count} contacts matching search term '{searchTerm}'");
                return searchResults;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while searching for contacts");
                return null;
            }
        }
    }
}
