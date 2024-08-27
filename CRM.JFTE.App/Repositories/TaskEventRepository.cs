using CRM.JFTE.Core;
using CRM.JFTE.Domain;
using Microsoft.EntityFrameworkCore;

namespace CRM.JFTE.App
{
    public class TaskEventRepository : ITaskEventRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<TaskEvent> _dbSet;

        public TaskEventRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TaskEvent>();
        }

        public async Task<TaskEvent?> AddTaskEventAsync(TaskEvent entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TaskEvent?> UpdateTaskEventAsync(TaskEvent entity)
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

        public async Task<TaskEvent?> DeleteTaskEventAsync(Guid entityId)
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

        public async Task<TaskEvent?> GetTaskEventByIdAsync(Guid entityId)
        {
            return await _dbSet.FindAsync(entityId);
        }

        public async Task<IEnumerable<TaskEvent>?> GetAllTaskEventsAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<TaskEvent>> SearchAsync(Func<TaskEvent, bool> predicate)
        {
            return await Task.FromResult(_dbSet.Where(predicate).ToList());
        }
    }
}
