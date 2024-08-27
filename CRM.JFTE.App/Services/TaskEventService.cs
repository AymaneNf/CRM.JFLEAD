using CRM.JFTE.Core;
using CRM.JFTE.Domain;
using Microsoft.Extensions.Logging;


namespace CRM.JFTE.App
{
    public class TaskEventService : ITaskEventService
    {
        private readonly ITaskEventRepository _taskEventRepository;
        private readonly ILogger<TaskEventService> _logger;

        public TaskEventService(ILogger<TaskEventService> logger, ITaskEventRepository taskEventRepository)
        {
            _logger = logger;
            _taskEventRepository = taskEventRepository;
        }

        public async Task<TaskEvent?> CreateTaskEventAsync(TaskEvent taskEvent)
        {
            try
            {
                taskEvent.Id = Guid.NewGuid();
                var createdTaskEvent = await _taskEventRepository.AddTaskEventAsync(taskEvent);
                return createdTaskEvent;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the task/event");
                return null;
            }
        }

        public async Task<TaskEvent?> UpdateTaskEventAsync(TaskEvent taskEvent)
        {
            try
            {
                var updatedTaskEvent = await _taskEventRepository.UpdateTaskEventAsync(taskEvent);
                if (updatedTaskEvent != null)
                {
                    _logger.LogInformation($"Task/Event updated with ID: {taskEvent.Id}");
                }
                return updatedTaskEvent;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the task/event");
                return null;
            }
        }

        public async Task<TaskEvent?> DeleteTaskEventAsync(Guid taskEventId)
        {
            try
            {
                var deletedTaskEvent = await _taskEventRepository.DeleteTaskEventAsync(taskEventId);
                if (deletedTaskEvent != null)
                {
                    _logger.LogInformation($"Task/Event deleted with ID: {taskEventId}");
                }
                return deletedTaskEvent;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the task/event");
                return null;
            }
        }

        public async Task<TaskEvent?> GetTaskEventByIdAsync(Guid taskEventId)
        {
            try
            {
                var taskEvent = await _taskEventRepository.GetTaskEventByIdAsync(taskEventId);
                if (taskEvent == null)
                {
                    _logger.LogWarning($"Task/Event not found with ID: {taskEventId}");
                }
                return taskEvent;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the task/event by ID");
                return null;
            }
        }

        public async Task<IEnumerable<TaskEvent>?> GetAllTaskEventsAsync()
        {
            try
            {
                var taskEvents = await _taskEventRepository.GetAllTaskEventsAsync();
                _logger.LogInformation($"Retrieved {taskEvents?.Count() ?? 0} task/events");
                return taskEvents;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all tasks/events");
                return null;
            }
        }

        public async Task<IEnumerable<TaskEvent>?> SearchTaskEventsAsync(string searchTerm)
        {
            try
            {
                if (string.IsNullOrEmpty(searchTerm))
                    return Enumerable.Empty<TaskEvent>();

                var taskEvents = await _taskEventRepository.GetAllTaskEventsAsync();

                if (taskEvents == null)
                    return Enumerable.Empty<TaskEvent>();

                var searchResults = taskEvents.Where(te =>
                    te.Nom.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    te.Type.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    te.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
                ).ToList();

                _logger.LogInformation($"Found {searchResults.Count} tasks/events matching search term '{searchTerm}'");
                return searchResults;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while searching for tasks/events");
                return null;
            }
        }

        public async Task<TaskEvent?> MarkAsCompletedAsync(Guid taskEventId)
        {
            try
            {
                var taskEvent = await _taskEventRepository.GetTaskEventByIdAsync(taskEventId);
                if (taskEvent == null)
                {
                    _logger.LogWarning($"Task/Event not found with ID: {taskEventId}");
                    return null;
                }

                taskEvent.IsCompleted = true;
                var updatedTaskEvent = await _taskEventRepository.UpdateTaskEventAsync(taskEvent);
                _logger.LogInformation($"Task/Event with ID: {taskEventId} marked as completed");
                return updatedTaskEvent;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while marking the task/event as completed");
                return null;
            }
        }

        public async Task<TaskEvent?> MarkAsPendingAsync(Guid taskEventId)
        {
            try
            {
                var taskEvent = await _taskEventRepository.GetTaskEventByIdAsync(taskEventId);
                if (taskEvent == null)
                {
                    _logger.LogWarning($"Task/Event not found with ID: {taskEventId}");
                    return null;
                }

                taskEvent.IsPending = true;
                var updatedTaskEvent = await _taskEventRepository.UpdateTaskEventAsync(taskEvent);
                _logger.LogInformation($"Task/Event with ID: {taskEventId} marked as pending");
                return updatedTaskEvent;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while marking the task/event as pending");
                return null;
            }
        }

        public async Task<TaskEvent?> CancelTaskEventAsync(Guid taskEventId)
        {
            try
            {
                var taskEvent = await _taskEventRepository.GetTaskEventByIdAsync(taskEventId);
                if (taskEvent == null)
                {
                    _logger.LogWarning($"Task/Event not found with ID: {taskEventId}");
                    return null;
                }

                taskEvent.IsCancelled = true;
                var updatedTaskEvent = await _taskEventRepository.UpdateTaskEventAsync(taskEvent);
                _logger.LogInformation($"Task/Event with ID: {taskEventId} has been cancelled");
                return updatedTaskEvent;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while cancelling the task/event");
                return null;
            }
        }

        public async Task<TaskEvent?> PostponeTaskEventAsync(Guid taskEventId, DateTime newDateTime)
        {
            try
            {
                var taskEvent = await _taskEventRepository.GetTaskEventByIdAsync(taskEventId);
                if (taskEvent == null)
                {
                    _logger.LogWarning($"Task/Event not found with ID: {taskEventId}");
                    return null;
                }

                taskEvent.DateHeureDebut = newDateTime;
                var updatedTaskEvent = await _taskEventRepository.UpdateTaskEventAsync(taskEvent);
                _logger.LogInformation($"Task/Event with ID: {taskEventId} has been postponed to {newDateTime}");
                return updatedTaskEvent;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while postponing the task/event");
                return null;
            }
        }

        public async Task<TaskEvent?> CreateReminderAsync(Guid taskEventId, DateTime reminderDateTime)
        {
            try
            {
                var taskEvent = await _taskEventRepository.GetTaskEventByIdAsync(taskEventId);
                if (taskEvent == null)
                {
                    _logger.LogWarning($"Task/Event not found with ID: {taskEventId}");
                    return null;
                }

                // Logic for creating a reminder (this can be an email notification, calendar event, etc.)
                // Example: taskEvent.ReminderDateTime = reminderDateTime;

                var updatedTaskEvent = await _taskEventRepository.UpdateTaskEventAsync(taskEvent);
                _logger.LogInformation($"Reminder for Task/Event with ID: {taskEventId} set for {reminderDateTime}");
                return updatedTaskEvent;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a reminder for the task/event");
                return null;
            }
        }

        public async Task<TaskEvent?> PlanTaskEventAsync(Guid taskEventId, DateTime planDateTime)
        {
            try
            {
                var taskEvent = await _taskEventRepository.GetTaskEventByIdAsync(taskEventId);
                if (taskEvent == null)
                {
                    _logger.LogWarning($"Task/Event not found with ID: {taskEventId}");
                    return null;
                }

                taskEvent.DateHeureDebut = planDateTime;
                var updatedTaskEvent = await _taskEventRepository.UpdateTaskEventAsync(taskEvent);
                _logger.LogInformation($"Task/Event with ID: {taskEventId} planned for {planDateTime}");
                return updatedTaskEvent;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while planning the task/event");
                return null;
            }
        }
    }
}
