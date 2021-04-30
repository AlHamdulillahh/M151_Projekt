using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Blog.Repository.Interfaces
{
    public interface IBaseRepository<TEntity, in TKey> where TEntity : class
    {
        // GetAll
        Task<List<TEntity>> GetAllAsync();
        Task<List<TEntity>> GetAllAsync(string include);
        Task<List<TEntity>> GetAllAsync(IEnumerable<string> includes);
        // Get
        Task<TEntity> GetAsync(TKey id);
        Task<TEntity> GetAsync(TKey id, string include);
        Task<TEntity> GetAsync(TKey id, IEnumerable<string> includes);
        // Find
        public Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        public Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, string include);
        public Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, IEnumerable<string> includes);
        // Add
        TEntity Add(TEntity entity);
        // Update
        TEntity Update(TEntity entity);
        // Remove
        void Remove(TEntity entity);
        void RemoveWithId(TKey id);
    }
}
