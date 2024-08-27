using CRM.JFLEAD.Core;
using CRM.JFLEAD.Domain;
using CRM.SharedKernel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.JFLEAD.App
{
    public class LeadRepository : ILeadRepository
    {
        private readonly IGenericRepository<Lead, AppDbContext> _repository;

        public LeadRepository(IGenericRepository<Lead, AppDbContext> repository)
        {
            _repository = repository;
        }

        public async Task<Lead?> AddLeadAsync(Lead entity)
        {
            return await _repository.AddAsync(entity);
        }

        public async Task<Lead?> UpdateLeadAsync(Lead entity)
        {
            return await _repository.UpdateAsync(entity);
        }

        public async Task<Lead?> DeleteLeadAsync(Guid entityId)
        {
            return await _repository.DeleteAsync(entityId);
        }

        public async Task<Lead?> GetLeadByIdAsync(Guid entityId)
        {
           return await _repository.GetByIdAsync(entityId);
        }

        public async Task<IEnumerable<Lead>?> GetAllLeadsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<IEnumerable<Lead>> SearchLeadsAsync(Func<Lead, bool> predicate)
        {
            var allLeads = await _repository.GetAllAsync();
            return allLeads?.Where(predicate) ?? Enumerable.Empty<Lead>();
        }
    }
}
