using Microsoft.EntityFrameworkCore;
using TemplateDDD.Shared.Repository;

namespace TemplateDDD.Shared.UnitOfWork
{
    public class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        private readonly TContext _context;
        private bool disposed = false;
        private Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

        public UnitOfWork(TContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public TContext DbContext => _context;

        public IRepository<T, Tkey> GetRepository<T, Tkey>() where T : class
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<Type, object>();
            }

            var type = typeof(T);
            if (!_repositories.ContainsKey(type))
            {
                _repositories[type] = new Repository.Repository<T, Tkey, TContext>(_context);
            }

            return (IRepository<T, Tkey>)_repositories[type];
        }

        public IQueryable<T> FromSql<T>(FormattableString sql) where T : class => _context.Set<T>().FromSqlInterpolated<T>(sql);

        public IQueryable<T> FromSqlRaw<T>(string sql, params object[] parameters) where T : class => _context.Set<T>().FromSqlRaw<T>(sql, parameters);

        public int ExecuteSqlCommand(string sql, params object[] parameters) => _context.Database.ExecuteSqlRaw(sql, parameters);

        public int SaveChanges() => _context.SaveChanges();

        public Task<int> SaveChangesAsync() => _context.SaveChangesAsync();

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (_repositories != null)
                    {
                        _repositories.Clear();
                    }

                    _context.Dispose();
                }
            }

            disposed = true;
        }
    }
}