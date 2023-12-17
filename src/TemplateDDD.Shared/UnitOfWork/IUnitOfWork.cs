using TemplateDDD.Shared.Repository;

namespace TemplateDDD.Shared.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T, Tkey> GetRepository<T, Tkey>() where T : class;

        IQueryable<T> FromSql<T>(FormattableString sql) where T : class;

        IQueryable<T> FromSqlRaw<T>(string sql, params object[] parameters) where T : class;

        int ExecuteSqlCommand(string sql, params object[] parameters);

        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}