using CRM.JFCL.Core;
using CRM.JFCL.Domain;
using CRM.JFCL.Infrastructure;
using Microsoft.AspNetCore.Mvc;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddClientService(builder.Configuration);
        builder.Services.AddLogging();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseHttpsRedirection();

        // Endpoints for clients
        app.MapPost("/api/clients", async (Client client, IClientService clientService) =>
        {
            var createdClient = await clientService.CreateClientAsync(client);
            return createdClient != null ? Results.Created($"/api/clients/{createdClient.Id}", createdClient) : Results.BadRequest();
        });

        app.MapPut("/api/clients/{id:guid}", async (Guid id, Client client, IClientService clientService) =>
        {
            if (id != client.Id) return Results.BadRequest("Client ID mismatch.");
            var updatedClient = await clientService.UpdateClientAsync(client);
            return updatedClient != null ? Results.Ok(updatedClient) : Results.NotFound();
        });

        app.MapDelete("/api/clients/{id:guid}", async (Guid id, IClientService clientService) =>
        {
            var deletedClient = await clientService.DeleteClientAsync(id);
            return deletedClient != null ? Results.Ok(deletedClient) : Results.NotFound();
        });

        app.MapGet("/api/clients/{id:guid}", async (Guid id, IClientService clientService) =>
        {
            var client = await clientService.GetClientByIdAsync(id);
            return client != null ? Results.Ok(client) : Results.NotFound();
        });

        app.MapGet("/api/clients", async (IClientService clientService) =>
        {
            var clients = await clientService.GetAllClientsAsync();
            return clients != null && clients.Any() ? Results.Ok(clients) : Results.NoContent();
        });

        app.MapGet("/api/clients/search", async ([FromQuery] string term, IClientService clientService) =>
        {
            var clients = await clientService.SearchClientsAsync(term);
            return clients != null && clients.Any() ? Results.Ok(clients) : Results.NotFound();
        });

        // Endpoint for deactivating a client
        app.MapPost("/api/clients/{id:guid}/deactivate", async (Guid id, IClientService clientService) =>
        {
            var deactivatedClient = await clientService.DeactivateClientAsync(id);
            return deactivatedClient != null ? Results.Ok(deactivatedClient) : Results.NotFound();
        });


        app.Run();
    }
}
