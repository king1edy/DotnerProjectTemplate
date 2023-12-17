using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace TemplateDDD.Shared.Repository
{
    public class Repository<T, Tkey, TContext> : IRepository<T, Tkey> where T : class where TContext : DbContext
    {
        protected readonly DbContext _dbContext;
        protected DbSet<T> _dbSet;

        public Repository(TContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _dbSet = _dbContext.Set<T>();
        }

        public DbSet<T> Entities()
        {
            if (_dbSet == null)
            {
                _dbSet = _dbContext.Set<T>();
            }

            return _dbSet;
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public async ValueTask<EntityEntry<T>> AddAsync(T entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await _dbSet.AddAsync(entity, cancellationToken);

            return result;
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public T? GetItem(Tkey pkid)
        {
            if (_dbSet != null)
            {
                return _dbSet.Find(pkid);
            }

            return null;
        }

        public IEnumerable<T> GetItems(Func<T, bool> predicate, string? navigation)
        {
            if (navigation == null)
                return _dbSet.Where(predicate).ToList();

            return _dbSet.Include(navigation).Where(predicate).ToList();
        }

        public void Remove(Tkey id)
        {
            var item = GetItem(id);
            if (item == null)
                return;
            _dbSet.Remove(item);
        }

        public void Remove(T entity)
        {
            if (entity == null)
                return;
            _dbSet.Remove(entity);
        }

        public void Update(Tkey id, T entity)
        {
            var item = GetItem(id);
            
            if (item == null)
                throw new KeyNotFoundException($"Item with id {id} not found");

            _dbSet.Update(entity);
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> selector)
        {
            if (selector == null)
            {
                return await _dbSet.AnyAsync();
            }
            else
            {
                return await _dbSet.AnyAsync(selector);
            }
        }
    }
}