namespace TemplateDDD.Shared.Pagination
{
    public class PaginatedList<T>
    {
        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public int TotalPageCount { get; private set; }
        public List<T> Items { get; set; }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPageCount);
            }
        }

        public PaginatedList(List<T> items, int pageIndex, int pageSize, int totalCount)
        {
            if (items == null)
            {
                throw new ArgumentNullException("source");
            }

            Items = items;
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = totalCount;
            TotalPageCount = (int)Math.Ceiling(totalCount / (double)pageSize);
        }
    }
}