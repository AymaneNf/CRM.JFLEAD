using CRM.JFOP.Infrastructure;
using CRM.JFOP.Core;
using CRM.JFOP.Domain;
using Microsoft.AspNetCore.Mvc;
using CRM.JFOP.App;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Register services and dependencies
        builder.Services.AddOpportuniteServices(builder.Configuration);
        builder.Services.AddLogging();

        var app = builder.Build();

        // Configure the HTTP request pipeline
        app.UseHttpsRedirection();

        // Initialisation de la base de données
        app.MapGet("/initdb", async (DatabaseManagement dbManagement) =>
        {
            await dbManagement.InitDatabase();
            return Results.Ok("Database initialized successfully.");
        });

        // Endpoints for Opportunite Management

        // Create a new Opportunite
        app.MapPost("/api/opportunites", async (Opportunite opportunite, IOpportuniteService opportuniteService) =>
        {
            try
            {
                var createdOpportunite = await opportuniteService.CreateOpportuniteAsync(opportunite);
                return createdOpportunite != null
                    ? Results.Created($"/api/opportunites/{createdOpportunite.Id}", createdOpportunite)
                    : Results.BadRequest();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        });

        // Update an existing Opportunite
        app.MapPut("/api/opportunites/{id:guid}", async (Guid id, Opportunite opportunite, IOpportuniteService opportuniteService) =>
        {
            try
            {
                if (id != opportunite.Id)
                    return Results.BadRequest("Opportunite ID mismatch.");

                var updatedOpportunite = await opportuniteService.UpdateOpportuniteAsync(opportunite);
                return updatedOpportunite != null
                    ? Results.Ok(updatedOpportunite)
                    : Results.NotFound();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        });

        // Delete an Opportunite
        app.MapDelete("/api/opportunites/{id:guid}", async (Guid id, IOpportuniteService opportuniteService) =>
        {
            try
            {
                var deletedOpportunite = await opportuniteService.DeleteOpportuniteAsync(id);
                return deletedOpportunite != null
                    ? Results.Ok(deletedOpportunite)
                    : Results.NotFound();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        });

        // Get an Opportunite by ID
        app.MapGet("/api/opportunites/{id:guid}", async (Guid id, IOpportuniteService opportuniteService) =>
        {
            try
            {
                var opportunite = await opportuniteService.GetOpportuniteByIdAsync(id);
                return opportunite != null
                    ? Results.Ok(opportunite)
                    : Results.NotFound();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        });

        // Get all Opportunites
        app.MapGet("/api/opportunites", async (IOpportuniteService opportuniteService) =>
        {
            try
            {
                var opportunites = await opportuniteService.GetAllOpportunitesAsync();
                return opportunites != null && opportunites.Any()
                    ? Results.Ok(opportunites)
                    : Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        });

        // Search Opportunites
        app.MapGet("/api/opportunites/search", async (string searchTerm, IOpportuniteService opportuniteService) =>
        {
            try
            {
                var searchResults = await opportuniteService.SearchOpportunitesAsync(searchTerm);
                return searchResults != null && searchResults.Any()
                    ? Results.Ok(searchResults)
                    : Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        });

        // Mark an Opportunite as won
        app.MapPost("/api/opportunites/{id:guid}/won", async (Guid id, IOpportuniteService opportuniteService) =>
        {
            try
            {
                var wonOpportunite = await opportuniteService.MarkAsWonAsync(id);
                return wonOpportunite != null
                    ? Results.Ok(wonOpportunite)
                    : Results.NotFound();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        });

        // Mark an Opportunite as lost
        app.MapPost("/api/opportunites/{id:guid}/lost", async (Guid id, IOpportuniteService opportuniteService) =>
        {
            try
            {
                var lostOpportunite = await opportuniteService.MarkAsLostAsync(id);
                return lostOpportunite != null
                    ? Results.Ok(lostOpportunite)
                    : Results.NotFound();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        });

        // Move an Opportunite to negotiation
        app.MapPost("/api/opportunites/{id:guid}/negotiate", async (Guid id, IOpportuniteService opportuniteService) =>
        {
            try
            {
                var negotiationOpportunite = await opportuniteService.MoveToNegotiationAsync(id);
                return negotiationOpportunite != null
                    ? Results.Ok(negotiationOpportunite)
                    : Results.NotFound();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        });

        // Move an Opportunite to proposal
        app.MapPost("/api/opportunites/{id:guid}/proposal", async (Guid id, IOpportuniteService opportuniteService) =>
        {
            try
            {
                var proposalOpportunite = await opportuniteService.MoveToProposalAsync(id);
                return proposalOpportunite != null
                    ? Results.Ok(proposalOpportunite)
                    : Results.NotFound();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        });

        // Create an event for an Opportunite
        app.MapPost("/api/opportunites/{id:guid}/event", async (Guid id, string eventDetails, IOpportuniteService opportuniteService) =>
        {
            try
            {
                var eventOpportunite = await opportuniteService.CreateEventForOpportuniteAsync(id, eventDetails);
                return eventOpportunite != null
                    ? Results.Ok(eventOpportunite)
                    : Results.NotFound();
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
