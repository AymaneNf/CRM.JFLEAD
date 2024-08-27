using CRM.JFTE.Domain;

namespace CRM.JFTE.Core
{
    public interface ITaskEventRepository
    {
        Task<TaskEvent?> AddTaskEventAsync(TaskEvent entity);
        Task<TaskEvent?> UpdateTaskEventAsync(TaskEvent entity);
        Task<TaskEvent?> DeleteTaskEventAsync(Guid entityId);
        Task<TaskEvent?> GetTaskEventByIdAsync(Guid entityId);
        Task<IEnumerable<TaskEvent>?> GetAllTaskEventsAsync();
        Task<IEnumerable<TaskEvent>> SearchAsync(Func<TaskEvent, bool> predicate);
    }
}
