using Microsoft.EntityFrameworkCore;
using TemplateDDD.Shared.UnitOfWork;

namespace TemplateDDD.Shared.BaseService
{
    public interface IBaseService<T, Tkey> : IDisposable where T : class
    {
        IUnitOfWork UnitOfWork { get; }

        DbSet<T> Entities();

        IQueryable<T> GetAll();

        T? GetItem(Tkey id);

        IEnumerable<T> GetItems(Func<T, bool> predicate, string? navigation = null);

        void Add(T entity);

        Task AddAsync(T entity);

        void AddRange(IEnumerable<T> entities);

        Task AddRangeAsync(IEnumerable<T> entities);

        void Remove(Tkey id);

        void Remove(T entity);

        Task<Int32> DeleteAsync(T entity);

        void Update(Tkey id, T entity);

        Task UpdateAsync(Tkey id, T entity);
    }
}