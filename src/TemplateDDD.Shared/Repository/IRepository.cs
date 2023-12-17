using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace TemplateDDD.Shared.Repository
{
    public interface IRepository<T, Tkey> where T : class
    {
        DbSet<T> Entities();

        IQueryable<T> GetAll();

        T? GetItem(Tkey id);

        IEnumerable<T> GetItems(Func<T, bool> predicate, string? navigation);

        void Add(T entity);

        ValueTask<EntityEntry<T>> AddAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));

        void AddRange(IEnumerable<T> entities);

        void Remove(Tkey id);

        void Remove(T entity);

        void Update(Tkey id, T entity);

        Task<bool> ExistsAsync(Expression<Func<T, bool>> selector);
    }
}