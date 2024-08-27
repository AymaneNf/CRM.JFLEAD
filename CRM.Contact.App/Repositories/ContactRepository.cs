using CRM.JFCT.Core;
using CRM.JFCT.Domain;
using CRM.SharedKernel.Core;


namespace CRM.JFCT.App
{
    public class ContactRepository : IContactRepository
    {
        private readonly IGenericRepository<Contact, AppDbContext> _repository;

        public ContactRepository(IGenericRepository<Contact, AppDbContext> repository)
        {
            _repository = repository;
        }

        public async Task<Contact?> AddContactAsync(Contact entity)
        {
            return await _repository.AddAsync(entity);
        }

        public async Task<Contact?> UpdateContactAsync(Contact entity)
        {
            return await _repository.UpdateAsync(entity);
        }

        public async Task<Contact?> DeleteContactAsync(Guid entityId)
        {
            return await _repository.DeleteAsync(entityId);
        }

        public async Task<Contact?> GetContactByIdAsync(Guid entityId)
        {
            return await _repository.GetByIdAsync(entityId);
        }

        public async Task<IEnumerable<Contact>?> GetAllContactsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<IEnumerable<Contact>> SearchAsync(Func<Contact, bool> predicate)
        {
            // Assuming _repository exposes a SearchAsync method or can implement search logic
            return await _repository.SearchAsync(predicate);
        }
    }
}
