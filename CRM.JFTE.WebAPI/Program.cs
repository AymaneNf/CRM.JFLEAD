using CRM.JFTE.App;
using CRM.JFTE.Core;
using CRM.JFTE.Domain;
using CRM.JFTE.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Register services and dependencies
        builder.Services.AddTaskEventServices(builder.Configuration);
        builder.Services.AddLogging();

        var app = builder.Build();

        // Configure the HTTP request pipeline
        app.UseHttpsRedirection();

        // Initialisation of the database
        app.MapGet("/initdb", async (DatabaseManagement dbManagement) =>
        {
            await dbManagement.InitDatabase();
            return Results.Ok("Database initialized successfully.");
        });

        // Endpoints for TaskEvent Management

        // Create a new TaskEvent
        app.MapPost("/api/task-events", async (TaskEvent taskEvent, ITaskEventService taskEventService) =>
        {
            try
            {
                var createdTaskEvent = await taskEventService.CreateTaskEventAsync(taskEvent);
                return createdTaskEvent != null ? Results.Created($"/api/task-events/{createdTaskEvent.Id}", createdTaskEvent) : Results.BadRequest();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        });

        // Update an existing TaskEvent
        app.MapPut("/api/task-events/{id:guid}", async (Guid id, TaskEvent taskEvent, ITaskEventService taskEventService) =>
        {
            try
            {
                if (id != taskEvent.Id) return Results.BadRequest("TaskEvent ID mismatch.");
                var updatedTaskEvent = await taskEventService.UpdateTaskEventAsync(taskEvent);
                return updatedTaskEvent != null ? Results.Ok(updatedTaskEvent) : Results.NotFound();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        });

        // Delete a TaskEvent
        app.MapDelete("/api/task-events/{id:guid}", async (Guid id, ITaskEventService taskEventService) =>
        {
            try
            {
                var deletedTaskEvent = await taskEventService.DeleteTaskEventAsync(id);
                return deletedTaskEvent != null ? Results.Ok(deletedTaskEvent) : Results.NotFound();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        });

        // Get a TaskEvent by ID
        app.MapGet("/api/task-events/{id:guid}", async (Guid id, ITaskEventService taskEventService) =>
        {
            try
            {
                var taskEvent = await taskEventService.GetTaskEventByIdAsync(id);
                return taskEvent != null ? Results.Ok(taskEvent) : Results.NotFound();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        });

        // Get all TaskEvents
        app.MapGet("/api/task-events", async (ITaskEventService taskEventService) =>
        {
            try
            {
                var taskEvents = await taskEventService.GetAllTaskEventsAsync();
                return taskEvents != null && taskEvents.Any() ? Results.Ok(taskEvents) : Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        });

        // Search TaskEvents
        app.MapGet("/api/task-events/search", async (string searchTerm, ITaskEventService taskEventService) =>
        {
            try
            {
                var searchResults = await taskEventService.SearchTaskEventsAsync(searchTerm);
                return searchResults != null && searchResults.Any() ? Results.Ok(searchResults) : Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        });

        // Mark a TaskEvent as completed
        app.MapPost("/api/task-events/{id:guid}/complete", async (Guid id, ITaskEventService taskEventService) =>
        {
            try
            {
                var completedTaskEvent = await taskEventService.MarkAsCompletedAsync(id);
                return completedTaskEvent != null ? Results.Ok(completedTaskEvent) : Results.NotFound();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        });

        // Mark a TaskEvent as pending
        app.MapPost("/api/task-events/{id:guid}/pending", async (Guid id, ITaskEventService taskEventService) =>
        {
            try
            {
                var pendingTaskEvent = await taskEventService.MarkAsPendingAsync(id);
                return pendingTaskEvent != null ? Results.Ok(pendingTaskEvent) : Results.NotFound();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        });

        // Cancel a TaskEvent
        app.MapPost("/api/task-events/{id:guid}/cancel", async (Guid id, ITaskEventService taskEventService) =>
        {
            try
            {
                var cancelledTaskEvent = await taskEventService.CancelTaskEventAsync(id);
                return cancelledTaskEvent != null ? Results.Ok(cancelledTaskEvent) : Results.NotFound();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        });

        // Postpone a TaskEvent
        app.MapPost("/api/task-events/{id:guid}/postpone", async (Guid id, DateTime newDateTime, ITaskEventService taskEventService) =>
        {
            try
            {
                var postponedTaskEvent = await taskEventService.PostponeTaskEventAsync(id, newDateTime);
                return postponedTaskEvent != null ? Results.Ok(postponedTaskEvent) : Results.NotFound();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        });

        // Create a reminder for a TaskEvent
        app.MapPost("/api/task-events/{id:guid}/reminder", async (Guid id, DateTime reminderDateTime, ITaskEventService taskEventService) =>
        {
            try
            {
                var reminderTaskEvent = await taskEventService.CreateReminderAsync(id, reminderDateTime);
                return reminderTaskEvent != null ? Results.Ok(reminderTaskEvent) : Results.NotFound();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        });

        // Plan a TaskEvent
        app.MapPost("/api/task-events/{id:guid}/plan", async (Guid id, DateTime planDateTime, ITaskEventService taskEventService) =>
        {
            try
            {
                var plannedTaskEvent = await taskEventService.PlanTaskEventAsync(id, planDateTime);
                return plannedTaskEvent != null ? Results.Ok(plannedTaskEvent) : Results.NotFound();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        });

        // Run the application
        app.Run();
    }
}
