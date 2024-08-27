using CRM.JFCL.App;
using CRM.JFCL.Core;
using CRM.JFPP.App;
using CRM.JFPP.Core;
using CRM.JFPP.Domain;
using CRM.JFPP.Infrastructure;
using Microsoft.AspNetCore.Mvc;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddProspectService(builder.Configuration);
        builder.Services.AddLogging();
        builder.Services.AddScoped<IClientService, ClientService>();


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseHttpsRedirection();

        // Initialisation de la base de données
        app.MapGet("/initdb", async (DataManagement dbManagement) =>
        {
            await dbManagement.InitDatabase();
            return Results.Ok("Database initialized.");
        });

        // Endpoints for prospects
        app.MapPost("/api/prospects", async ([FromBody] Prospect prospect, IProspectService prospectService) =>
        {
            var createdProspect = await prospectService.CreateProspectAsync(prospect);
            return createdProspect != null ? Results.Created($"/api/prospects/{createdProspect.Id}", createdProspect) : Results.BadRequest();
        });

        app.MapPut("/api/prospects/{id:guid}", async (Guid id, [FromBody] Prospect prospect, IProspectService prospectService) =>
        {
            if (id != prospect.Id)
            {
                return Results.BadRequest("Prospect ID mismatch.");
            }

            var updatedProspect = await prospectService.UpdateProspectAsync(prospect);
            return updatedProspect != null ? Results.Ok(updatedProspect) : Results.BadRequest("Failed to update prospect.");
        });

        app.MapDelete("/api/prospects/{id:guid}", async (Guid id, IProspectService prospectService) =>
        {
            var deletedProspect = await prospectService.DeleteProspectAsync(id);
            return deletedProspect != null ? Results.Ok(deletedProspect) : Results.NotFound();
        });

        app.MapGet("/api/prospects/{id:guid}", async (Guid id, IProspectService prospectService) =>
        {
            var prospect = await prospectService.GetProspectByIdAsync(id);
            return prospect != null ? Results.Ok(prospect) : Results.NotFound();
        });

        app.MapGet("/api/prospects", async (IProspectService prospectService) =>
        {
            var prospects = await prospectService.GetAllProspectsAsync();
            return prospects != null && prospects.Any() ? Results.Ok(prospects) : Results.NoContent();
        });

        app.MapGet("/api/prospects/search", async ([FromQuery] string term, IProspectService prospectService) =>
        {
            var prospects = await prospectService.SearchProspectsAsync(term);
            return prospects != null && prospects.Any() ? Results.Ok(prospects) : Results.NotFound();
        });

        // Endpoint to convert a prospect to a client
        app.MapPost("/api/prospects/{id:guid}/convert-to-client", async (Guid id, IProspectService prospectService) =>
        {
            var convertedProspect = await prospectService.ConvertToClientAsync(id);
            return convertedProspect != null ? Results.Ok(convertedProspect) : Results.NotFound();
        });

        // Endpoint for deactivating a prospect
        app.MapPost("/api/prospects/{id:guid}/deactivate", async (Guid id, IProspectService prospectService) =>
        {
            var deactivatedProspect = await prospectService.DeactivateProspectAsync(id);
            return deactivatedProspect != null ? Results.Ok(deactivatedProspect) : Results.NotFound();
        });


        app.Run();
    }
}
