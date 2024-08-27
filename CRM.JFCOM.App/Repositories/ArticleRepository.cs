using CRM.JFCOM.Core;
using CRM.JFCOM.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CRM.JFCOM.App
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Article> _dbSet;

        public ArticleRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Article>();
        }

        public async Task<Article?> AddArticleAsync(Article entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Article?> UpdateArticleAsync(Article entity)
        {
            var existingEntity = await _dbSet.FindAsync(entity.Id);
            if (existingEntity == null) return null;

            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return existingEntity;
        }

        public async Task<Article?> DeleteArticleAsync(Guid entityId)
        {
            var entity = await _dbSet.FindAsync(entityId);
            if (entity == null) return null;

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Article?> GetArticleByIdAsync(Guid entityId)
        {
            return await _dbSet.FindAsync(entityId);
        }

        public async Task<IEnumerable<Article>?> GetAllArticlesAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<Article>?> SearchArticlesAsync(Expression<Func<Article, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }
    }
}
