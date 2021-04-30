using Blog.Data;
using Blog.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Blog.Repository.Repositories
{
    public abstract class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        protected readonly DataContext Context;
        protected readonly DbSet<TEntity> _entities;
        public BaseRepository(DataContext context)
        {
            Context = context;
            _entities = Context.Set<TEntity>();
        }

        public Task<List<TEntity>> GetAllAsync()
        {
            return _entities.ToListAsync();
        }

        public Task<List<TEntity>> GetAllAsync(string include)
        {
            var query = _entities.Include(include);

            return query.ToListAsync();
        }

        public Task<List<TEntity>> GetAllAsync(IEnumerable<string> includes)
        {
            var query = _entities.AsQueryable();
            query = includes.Aggregate(query, (current, include) => current.Include(include));

            return query.ToListAsync();
        }

        public Task<TEntity> GetAsync(TKey id)
        {
            return _entities.FindAsync(id).AsTask();
        }

        public Task<TEntity> GetAsync(TKey id, string include)
        {
            return _entities.Include(include).FirstOrDefaultAsync(e => e.Id.Equals(id));
        }

        public Task<TEntity> GetAsync(TKey id, IEnumerable<string> includes)
        {
            var query = _entities.AsQueryable();
            query = includes.Aggregate(query, (current, include) => current.Include(include));

            return query.FirstOrDefaultAsync(e => e.Id.Equals(id));
        }

        public Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var query = _entities.Where(predicate);

            return query.ToListAsync();
        }

        public Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, string include)
        {
            var query = _entities
                .Include(include)
                .Where(predicate);

            return query.ToListAsync();
        }

        public Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, IEnumerable<string> includes)
        {
            var query = _entities.Where(predicate).AsNoTracking();
            query = includes.Aggregate(query, (current, include) => current.Include(include));

            return query.ToListAsync();
        }

        public TEntity Add(TEntity entity)
        {
            _entities.Add(entity);
            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            _entities.Update(entity);
            return entity;
        }

        public void Remove(TEntity entity)
        {
            _entities.Remove(entity);
        }

        public void RemoveWithId(TKey id)
        {
            TEntity entity = _entities.Find(id);
            _entities.Remove(entity);
        }
    }
}
