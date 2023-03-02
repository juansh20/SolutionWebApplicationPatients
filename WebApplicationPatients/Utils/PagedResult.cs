namespace WebApplicationPatients.Utils
{
    public class PagedResult<T>
    {
        public List<T> Results { get; set; }
        public int TotalCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        public PagedResult(PagedList<T> pagedList, int totalCount)
        {
            Results = pagedList;
            TotalCount = totalCount;
            CurrentPage = pagedList.CurrentPage;
            PageSize = pagedList.PageSize;
            TotalPages = pagedList.TotalPages;
        }
    }
}
