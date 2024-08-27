using System.Linq.Expressions;
using CRM.JFCOM.Domain;

namespace CRM.JFCOM.Core
{
    public interface IArticleService
    {
        Task<Article?> CreateArticleAsync(Article article);
        Task<Article?> UpdateArticleAsync(Article article);
        Task<Article?> DeleteArticleAsync(Guid articleId);
        Task<Article?> GetArticleByIdAsync(Guid articleId);
        Task<IEnumerable<Article>?> GetAllArticlesAsync();
        Task<IEnumerable<Article>?> SearchArticlesAsync(Expression<Func<Article, bool>> predicate);

        // Method to transform an Article into a BonDeCommande
        Task<BonDeCommande?> TransformerEnBonDeCommandeAsync(Guid articleId, int quantity, Guid clientId);

        // Method to get alternative products
        Task<IEnumerable<Article>?> GetAlternativeProductsAsync(string articleReference);
    }
}
