using CRM.JFCOM.Infrastructure;  
using CRM.JFCOM.Core;            
using CRM.JFCOM.Domain;          
using Microsoft.AspNetCore.Mvc;  
using Microsoft.EntityFrameworkCore;
using CRM.JFCOM.App;


public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Register services and dependencies for the "Commerciale" module
        builder.Services.AddCommercialeServices(builder.Configuration);
        builder.Services.AddLogging();

        var app = builder.Build();

        // Configure the HTTP request pipeline
        app.UseHttpsRedirection();

        // Initialize the database
        app.MapGet("/initdb", async (DatabaseManagement dbManagement) =>
        {
            await dbManagement.InitDatabase();
            return "Database initialized successfully";
        });

        // Endpoints for managing Articles

        app.MapPost("/api/articles", async ([FromBody] Article article, IArticleService articleService) =>
        {
            var createdArticle = await articleService.CreateArticleAsync(article);
            return createdArticle != null
                ? Results.Created($"/api/articles/{createdArticle.Id}", createdArticle)
                : Results.BadRequest();
        });

        app.MapPut("/api/articles/{id:guid}", async (Guid id, [FromBody] Article article, IArticleService articleService) =>
        {
            if (id != article.Id)
                return Results.BadRequest("Article ID mismatch.");

            var updatedArticle = await articleService.UpdateArticleAsync(article);
            return updatedArticle != null
                ? Results.Ok(updatedArticle)
                : Results.NotFound();
        });

        app.MapDelete("/api/articles/{id:guid}", async (Guid id, IArticleService articleService) =>
        {
            var deletedArticle = await articleService.DeleteArticleAsync(id);
            return deletedArticle != null
                ? Results.Ok(deletedArticle)
                : Results.NotFound();
        });

        app.MapGet("/api/articles/{id:guid}", async (Guid id, IArticleService articleService) =>
        {
            var article = await articleService.GetArticleByIdAsync(id);
            return article != null
                ? Results.Ok(article)
                : Results.NotFound();
        });

        app.MapGet("/api/articles", async (IArticleService articleService) =>
        {
            var articles = await articleService.GetAllArticlesAsync();
            return articles != null && articles.Any()
                ? Results.Ok(articles)
                : Results.NoContent();
        });

        app.MapGet("/api/articles/search", async (string query, IArticleService articleService) =>
        {
            var articles = await articleService.SearchArticlesAsync(a => a.Designation.Contains(query, StringComparison.OrdinalIgnoreCase));
            return articles != null && articles.Any()
                ? Results.Ok(articles)
                : Results.NoContent();
        });

        app.MapPost("/api/articles/{articleId:guid}/transform", async (Guid articleId, [FromBody] TransformArticleRequest request, IArticleService articleService) =>
        {
            if (request == null || request.ClientId == Guid.Empty || request.Quantity <= 0)
            {
                return Results.BadRequest("Invalid request parameters.");
            }

            var bonDeCommande = await articleService.TransformerEnBonDeCommandeAsync(articleId, request.Quantity, request.ClientId);
            return bonDeCommande != null
                ? Results.Ok(bonDeCommande)
                : Results.BadRequest("Failed to transform article into bon de commande.");
        });


        // Endpoints for managing Devis

        app.MapPost("/api/devis", async ([FromBody] Devis devis, IDevisService devisService) =>
        {
            var createdDevis = await devisService.CreateDevisAsync(devis);
            return createdDevis != null
                ? Results.Created($"/api/devis/{createdDevis.Id}", createdDevis)
                : Results.BadRequest();
        });

        app.MapPut("/api/devis/{id:guid}", async (Guid id, [FromBody] Devis devis, IDevisService devisService) =>
        {
            if (id != devis.Id)
                return Results.BadRequest("Devis ID mismatch.");

            var updatedDevis = await devisService.UpdateDevisAsync(devis);
            return updatedDevis != null
                ? Results.Ok(updatedDevis)
                : Results.NotFound();
        });

        app.MapDelete("/api/devis/{id:guid}", async (Guid id, IDevisService devisService) =>
        {
            var deletedDevis = await devisService.DeleteDevisAsync(id);
            return deletedDevis != null
                ? Results.Ok(deletedDevis)
                : Results.NotFound();
        });

        app.MapGet("/api/devis/{id:guid}", async (Guid id, IDevisService devisService) =>
        {
            var devis = await devisService.GetDevisByIdAsync(id);
            return devis != null
                ? Results.Ok(devis)
                : Results.NotFound();
        });

        app.MapGet("/api/devis", async (IDevisService devisService) =>
        {
            var devisList = await devisService.GetAllDevisAsync();
            return devisList != null && devisList.Any()
                ? Results.Ok(devisList)
                : Results.NoContent();
        });

        app.MapGet("/api/devis/search", async (string query, IDevisService devisService) =>
        {
            var devisList = await devisService.SearchDevisAsync(d =>
                d.NumeroPiece.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                d.Lignes.Any(ligne => ligne.Designation.Contains(query, StringComparison.OrdinalIgnoreCase))
            );

            return devisList != null && devisList.Any()
                ? Results.Ok(devisList)
                : Results.NoContent();
        });


        // Endpoints for managing Bon de Commande

        app.MapPost("/api/bondecommande", async ([FromBody] BonDeCommande bonDeCommande, IBonDeCommandeService bonDeCommandeService) =>
        {
            var createdBonDeCommande = await bonDeCommandeService.CreateBonDeCommandeAsync(bonDeCommande);
            return createdBonDeCommande != null
                ? Results.Created($"/api/bondecommande/{createdBonDeCommande.Id}", createdBonDeCommande)
                : Results.BadRequest();
        });

        app.MapPut("/api/bondecommande/{id:guid}", async (Guid id, [FromBody] BonDeCommande bonDeCommande, IBonDeCommandeService bonDeCommandeService) =>
        {
            if (id != bonDeCommande.Id)
                return Results.BadRequest("Bon de Commande ID mismatch.");

            var updatedBonDeCommande = await bonDeCommandeService.UpdateBonDeCommandeAsync(bonDeCommande);
            return updatedBonDeCommande != null
                ? Results.Ok(updatedBonDeCommande)
                : Results.NotFound();
        });

        app.MapDelete("/api/bondecommande/{id:guid}", async (Guid id, IBonDeCommandeService bonDeCommandeService) =>
        {
            var deletedBonDeCommande = await bonDeCommandeService.DeleteBonDeCommandeAsync(id);
            return deletedBonDeCommande != null
                ? Results.Ok(deletedBonDeCommande)
                : Results.NotFound();
        });

        app.MapGet("/api/bondecommande/{id:guid}", async (Guid id, IBonDeCommandeService bonDeCommandeService) =>
        {
            var bonDeCommande = await bonDeCommandeService.GetBonDeCommandeByIdAsync(id);
            return bonDeCommande != null
                ? Results.Ok(bonDeCommande)
                : Results.NotFound();
        });

        app.MapGet("/api/bondecommande", async (IBonDeCommandeService bonDeCommandeService) =>
        {
            var bonDeCommandes = await bonDeCommandeService.GetAllBonDeCommandesAsync();
            return bonDeCommandes != null && bonDeCommandes.Any()
                ? Results.Ok(bonDeCommandes)
                : Results.NoContent();
        });

        app.MapGet("/api/bondecommande/search", async (string query, IBonDeCommandeService bonDeCommandeService) =>
        {
            var bonDeCommandes = await bonDeCommandeService.SearchBonDeCommandesAsync(b => b.NumeroPiece.Contains(query, StringComparison.OrdinalIgnoreCase));
            return bonDeCommandes != null && bonDeCommandes.Any()
                ? Results.Ok(bonDeCommandes)
                : Results.NoContent();
        });

        app.Run();
    }
}
