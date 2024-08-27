using CRM.JFCL.Core;
using CRM.JFCL.Domain;
using Microsoft.EntityFrameworkCore;

namespace CRM.JFCL.App
{
    public class ClientRepository : IClientRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Client> _dbSet;

        public ClientRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Client>();
        }

        public async Task<Client?> AddClientAsync(Client entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Client?> UpdateClientAsync(Client entity)
        {
            var existingEntity = await _dbSet.FindAsync(entity.Id);
            if (existingEntity == null)
            {
                return null;
            }

            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return existingEntity;
        }

        public async Task<Client?> DeleteClientAsync(Guid entityId)
        {
            var entity = await _dbSet.FindAsync(entityId);
            if (entity == null)
            {
                return null;
            }

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Client?> GetClientByIdAsync(Guid entityId)
        {
            return await _dbSet.FindAsync(entityId);
        }

        public async Task<IEnumerable<Client>?> GetAllClientsAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<Client>> SearchAsync(Func<Client, bool> predicate)
        {
            return await Task.FromResult(_dbSet.Where(predicate).ToList());
        }
    }
}
