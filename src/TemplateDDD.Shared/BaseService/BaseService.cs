using Microsoft.EntityFrameworkCore;
using TemplateDDD.Shared.Repository;
using TemplateDDD.Shared.UnitOfWork;

namespace TemplateDDD.Shared.BaseService
{
    public class BaseService<T, Tkey> : IBaseService<T, Tkey> where T : class
    {
        private readonly IRepository<T, Tkey> _repository;
        private bool _disposed;

        public IUnitOfWork UnitOfWork { get; private set; }

        public BaseService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            _repository = UnitOfWork.GetRepository<T, Tkey>();
        }

        public DbSet<T> Entities()
        {
            return _repository.Entities();
        }

        public IQueryable<T> GetAll()
        {
            return _repository.GetAll();
        }

        public T? GetItem(Tkey key)
        {
            return _repository.GetItem(key);
        }

        public IEnumerable<T> GetItems(Func<T, bool> predicate, string? navigation)
        {
            return _repository.GetItems(predicate, navigation);
        }

        public void Add(T entity)
        {
            _repository.Add(entity);
            UnitOfWork.SaveChanges();
        }

        public async Task AddAsync(T entity)
        {
            _repository.Add(entity);
            await UnitOfWork.SaveChangesAsync();
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _repository.AddRange(entities);
            UnitOfWork.SaveChanges();
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            _repository.AddRange(entities);
            await UnitOfWork.SaveChangesAsync();
        }

        public void Remove(Tkey id)
        {
            _repository.Remove(id);
            UnitOfWork.SaveChanges();
        }

        public void Remove(T entity)
        {
            _repository.Remove(entity);
            UnitOfWork.SaveChanges();
        }

        public async Task<Int32> DeleteAsync(T entity)
        {
            _repository.Remove(entity);

            var NoDeleted = await UnitOfWork.SaveChangesAsync();
            return NoDeleted;
        }

        public void Update(Tkey id, T entity)
        {
            _repository.Update(id, entity);
            UnitOfWork.SaveChanges();
        }

        public async Task UpdateAsync(Tkey id, T entity)
        {
            _repository.Update(id, entity);
            await UnitOfWork.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                UnitOfWork.Dispose();
            }

            _disposed = true;
        }
    }
}