using Microsoft.EntityFrameworkCore;
using TemplateDDD.Shared.Models;

namespace TemplateDDD.Shared.Pagination
{
    public class Paginator<T> : IPaginator<T>
    {
        private const int DEFAULT_PAGE_SIZE = 5;

        public List<T> Paginate(IQueryable<T> query, BaseQueryModel model)
        {
            var pageIndex = model.PageIndex < 1 ? 1 : model.PageIndex;
            var pageSize = model.PageSize < 1 ? DEFAULT_PAGE_SIZE : model.PageSize;

            var data = query.Paginate(pageIndex, pageSize).ToList();

            return data;
        }

        public async Task<List<T>> PaginateAsync(IQueryable<T> query, BaseQueryModel model)
        {
            var pageIndex = model.PageIndex < 1 ? 1 : model.PageIndex;
            var pageSize = model.PageSize < 1 ? DEFAULT_PAGE_SIZE : model.PageSize;

            var data = await query.Paginate(pageIndex, pageSize).ToListAsync();

            return data;
        }
    }
}