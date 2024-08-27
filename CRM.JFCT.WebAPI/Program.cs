using CRM.JFCT.App;
using CRM.JFCT.Core;
using CRM.JFCT.Domain;
using CRM.JFCT.Infrastructure;
using Microsoft.AspNetCore.Mvc;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddContactService(builder.Configuration);
        builder.Services.AddLogging();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseHttpsRedirection();

        // Initialisation de la base de données
        app.MapGet("/initdb", async (DataManagement dbManagement) =>
        {
            await dbManagement.InitDatabase();
            return Results.Ok("Database initialized.");
        });

        // Endpoints for contacts
        app.MapPost("/api/contacts", async ([FromBody] Contact contact, IContactService contactService) =>
        {
            var createdContact = await contactService.CreateContactAsync(contact);
            return createdContact != null ? Results.Created($"/api/contacts/{createdContact.Id}", createdContact) : Results.BadRequest();
        });

        app.MapPut("/api/contacts/{id:guid}", async (Guid id, [FromBody] Contact contact, IContactService contactService) =>
        {
            if (id != contact.Id)
            {
                return Results.BadRequest("Contact ID mismatch.");
            }

            var updatedContact = await contactService.UpdateContactAsync(contact);
            return updatedContact != null ? Results.Ok(updatedContact) : Results.BadRequest("Failed to update contact.");
        });

        app.MapDelete("/api/contacts/{id:guid}", async (Guid id, IContactService contactService) =>
        {
            var deletedContact = await contactService.DeleteContactAsync(id);
            return deletedContact != null ? Results.Ok(deletedContact) : Results.NotFound();
        });

        app.MapGet("/api/contacts/{id:guid}", async (Guid id, IContactService contactService) =>
        {
            var contact = await contactService.GetContactByIdAsync(id);
            return contact != null ? Results.Ok(contact) : Results.NotFound();
        });

        app.MapGet("/api/contacts", async (IContactService contactService) =>
        {
            var contacts = await contactService.GetAllContactsAsync();
            return contacts != null && contacts.Any() ? Results.Ok(contacts) : Results.NoContent();
        });

        app.MapGet("/api/contacts/search", async ([FromQuery] string term, IContactService contactService) =>
        {
            var contacts = await contactService.SearchContactsAsync(term);
            return contacts != null && contacts.Any() ? Results.Ok(contacts) : Results.NotFound();
        });

        app.Run();
    }
}
