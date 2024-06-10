using CRM.JFLEAD.App;
using CRM.JFLEAD.Core;
using CRM.JFLEAD.Domain;
using CRM.JFLEAD.Infrastructure;
using Microsoft.AspNetCore.Mvc;


public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddLeadService(builder.Configuration);
        builder.Services.AddLogging();



        var app = builder.Build();

        // Configure the HTTP request pipeline.

        app.UseHttpsRedirection();

        // Initialisation de la base de données
        app.MapGet("/initdb", async (DataManagement dbManagement) =>
        {
            await dbManagement.InitDatabase();
            return "OK";
        });

        // Endpoints for leads
        app.MapPost("/api/leads", async ([FromBody] Lead lead, ILeadService leadService) =>
        {
            var createdLead = await leadService.CreateLeadAsync(lead);
            return createdLead != null ? Results.Created($"/api/leads/{createdLead.Id}", createdLead) : Results.BadRequest();
        });

        app.MapPut("/api/leads/{id}", async (Guid id, [FromBody] Lead lead, ILeadService leadService) =>
        {
            if (id != lead.Id)
            {
                return Results.BadRequest();
            }
            var updatedLead = await leadService.UpdateLeadAsync(lead);
            return updatedLead != null ? Results.Ok(updatedLead) : Results.BadRequest();
        });

        app.MapDelete("/api/leads/{id}", async (Guid id, ILeadService leadService) =>
        {
            var deletedLead = await leadService.DeleteLeadAsync(id);
            return deletedLead != null ? Results.Ok(deletedLead) : Results.NotFound();
        });

        app.MapGet("/api/leads/{id}", async (Guid id, ILeadService leadService) =>
        {
            var lead = await leadService.GetLeadByIdAsync(id);
            return lead != null ? Results.Ok(lead) : Results.NotFound();
        });

        app.MapGet("/api/leads", async (ILeadService leadService) =>
        {
            var leads = await leadService.GetAllLeadsAsync();
            return leads != null ? Results.Ok(leads) : Results.NoContent();
        });

        app.MapPost("/api/leads/{id}/assign", async (Guid id, [FromBody] int collaboratorId, ILeadService leadService) =>
        {
            await leadService.AssignLeadAsync(id, collaboratorId);
            return Results.NoContent();
        });

        app.MapPost("/api/leads/{id}/start", async (Guid id, ILeadService leadService) =>
        {
            await leadService.StartLeadAsync(id);
            return Results.NoContent();
        });

        app.MapPost("/api/leads/{id}/convert/won", async (Guid id, ILeadService leadService) =>
        {
            await leadService.ConvertLeadToWonAsync(id);
            return Results.NoContent();
        });

        app.MapPost("/api/leads/{id}/convert/lost", async (Guid id, ILeadService leadService) =>
        {
            await leadService.MarkLeadAsLostAsync(id);
            return Results.NoContent();
        });

        app.MapPost("/api/leads/{id}/event", async (Guid id, [FromBody] string eventDetails, ILeadService leadService) =>
        {
            await leadService.CreateEventFromLeadAsync(id, eventDetails);
            return Results.NoContent();
        });



        app.Run();
    }
}