namespace Application.Common
{
    public class PagedResult<T>
    {
        public IReadOnlyList<T> Items { get; }
        public int CurrentPage { get; }
        public int PageSize { get; }
        public int TotalPages { get; }
        public int TotalCount { get; }
        public bool HasNextPage => CurrentPage < TotalPages;
        public bool HasPreviousPage => CurrentPage > 1;

        public PagedResult(IEnumerable<T> items, int currentPage, int pageSize, int totalCount)
        {
            Items = new List<T>(items);
            PageSize = pageSize <= 0 ? totalCount : pageSize;
            TotalCount = totalCount;
            TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);
            CurrentPage = currentPage <= 0 || currentPage >= TotalPages ? 1 : currentPage;
        }
    }
}
