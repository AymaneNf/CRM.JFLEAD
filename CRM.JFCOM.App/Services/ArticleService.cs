using CRM.JFCOM.Core;
using CRM.JFCOM.Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CRM.JFCOM.App
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IBonDeCommandeRepository _bonDeCommandeRepository;
        private readonly ILogger<ArticleService> _logger;

        public ArticleService(ILogger<ArticleService> logger, IArticleRepository articleRepository, IBonDeCommandeRepository bonDeCommandeRepository)
        {
            _logger = logger;
            _articleRepository = articleRepository;
            _bonDeCommandeRepository = bonDeCommandeRepository;
        }

        public async Task<Article?> CreateArticleAsync(Article article)
        {
            try
            {
                article.Id = Guid.NewGuid();
                var createdArticle = await _articleRepository.AddArticleAsync(article);
                return createdArticle;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the article");
                return null;
            }
        }

        public async Task<Article?> UpdateArticleAsync(Article article)
        {
            try
            {
                var updatedArticle = await _articleRepository.UpdateArticleAsync(article);
                return updatedArticle;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the article");
                return null;
            }
        }

        public async Task<Article?> DeleteArticleAsync(Guid articleId)
        {
            try
            {
                var deletedArticle = await _articleRepository.DeleteArticleAsync(articleId);
                return deletedArticle;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the article");
                return null;
            }
        }

        public async Task<Article?> GetArticleByIdAsync(Guid articleId)
        {
            try
            {
                return await _articleRepository.GetArticleByIdAsync(articleId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the article by ID");
                return null;
            }
        }

        public async Task<IEnumerable<Article>?> GetAllArticlesAsync()
        {
            try
            {
                return await _articleRepository.GetAllArticlesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all articles");
                return null;
            }
        }

        public async Task<IEnumerable<Article>?> SearchArticlesAsync(Expression<Func<Article, bool>> predicate)
        {
            try
            {
                return await _articleRepository.SearchArticlesAsync(predicate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while searching for articles");
                return null;
            }
        }

        // Method to transform an Article into a BonDeCommande
        public async Task<BonDeCommande?> TransformerEnBonDeCommandeAsync(Guid articleId, int quantity, Guid clientId)
        {
            try
            {
                var article = await _articleRepository.GetArticleByIdAsync(articleId);

                if (article == null)
                {
                    _logger.LogWarning($"Article with ID: {articleId} not found.");
                    return null;
                }

                // Create a new BonDeCommandeLigne for the article
                var bonDeCommandeLigne = new BonDeCommandeLigne
                {
                    ReferenceArticle = article.Reference,
                    Designation = article.Designation,
                    Quantitee = quantity,
                    PU = article.PrixVente,
                    Remise = 0, // Assuming no discount
                    PUNet = article.PrixVente, // Assuming no discount
                    Taxe = article.PrixVente * article.TauxTaxe / 100 * quantity,
                    TotalHTBrut = article.PrixVente * quantity,
                    TotalHTNet = article.PrixVente * quantity // Assuming no discount
                };

                // Calculate total amounts for BonDeCommande
                var bonDeCommande = new BonDeCommande
                {
                    ClientId = clientId,
                    Date = DateTime.UtcNow,
                    NumeroPiece = Guid.NewGuid().ToString(), // Example piece number, adjust as necessary
                    Lignes = new List<BonDeCommandeLigne> { bonDeCommandeLigne },
                    MontantTotalHT = bonDeCommandeLigne.TotalHTNet,
                    MontantTaxe = bonDeCommandeLigne.Taxe,
                    MontantTTC = bonDeCommandeLigne.TotalHTNet + bonDeCommandeLigne.Taxe
                };

                // Save the BonDeCommande to the repository
                await _bonDeCommandeRepository.AddBonDeCommandeAsync(bonDeCommande);

                _logger.LogInformation($"Transformed article {article.Id} into bon de commande");
                return bonDeCommande;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while transforming article into bon de commande");
                return null;
            }
        }

        // Method to get alternative products directly in the service
        public async Task<IEnumerable<Article>?> GetAlternativeProductsAsync(string articleReference)
        {
            try
            {
                // Fetch all articles from the repository
                var allArticles = await _articleRepository.GetAllArticlesAsync();
                if (allArticles == null || !allArticles.Any())
                {
                    _logger.LogWarning("No articles found in the repository.");
                    return null;
                }

                // Find the original article to compare
                var originalArticle = allArticles.FirstOrDefault(a => a.Reference == articleReference);
                if (originalArticle == null)
                {
                    _logger.LogWarning($"Article not found with reference: {articleReference}");
                    return null;
                }

                // Filter articles to find alternatives: same category and in stock
                var alternativeProducts = allArticles
                    .Where(a => a.Reference != articleReference && // Different reference
                                a.Famille == originalArticle.Famille &&    // Same category
                                a.Stock > 0)                              // In stock
                    .ToList();

                return alternativeProducts;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving alternative products");
                return null;
            }
        }
    }
}
