using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CRM.JFCOM.Domain;

namespace CRM.JFCOM.Core
{
    public interface IArticleRepository
    {
        Task<Article?> AddArticleAsync(Article entity);
        Task<Article?> UpdateArticleAsync(Article entity);
        Task<Article?> DeleteArticleAsync(Guid entityId);
        Task<Article?> GetArticleByIdAsync(Guid entityId);
        Task<IEnumerable<Article>?> GetAllArticlesAsync();
        Task<IEnumerable<Article>?> SearchArticlesAsync(Expression<Func<Article, bool>> predicate);
    }
}
