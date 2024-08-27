using CRM.JFTE.Domain;

namespace CRM.JFTE.Core
{
    public interface ITaskEventService
    {
        Task<TaskEvent?> CreateTaskEventAsync(TaskEvent taskEvent);
        Task<TaskEvent?> UpdateTaskEventAsync(TaskEvent taskEvent);
        Task<TaskEvent?> DeleteTaskEventAsync(Guid taskEventId);
        Task<TaskEvent?> GetTaskEventByIdAsync(Guid taskEventId);
        Task<IEnumerable<TaskEvent>?> GetAllTaskEventsAsync();
        Task<IEnumerable<TaskEvent>?> SearchTaskEventsAsync(string searchTerm);

        // Workflow management methods in the service layer
        Task<TaskEvent?> MarkAsCompletedAsync(Guid taskEventId);
        Task<TaskEvent?> MarkAsPendingAsync(Guid taskEventId);
        Task<TaskEvent?> CancelTaskEventAsync(Guid taskEventId);
        Task<TaskEvent?> PostponeTaskEventAsync(Guid taskEventId, DateTime newDateTime);
        Task<TaskEvent?> CreateReminderAsync(Guid taskEventId, DateTime reminderDateTime);
        Task<TaskEvent?> PlanTaskEventAsync(Guid taskEventId, DateTime planDateTime);
    }
}
