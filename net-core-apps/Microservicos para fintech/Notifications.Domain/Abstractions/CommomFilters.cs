namespace Notifications.Domain.Abstractions
{
    public class CommonFilters
    {
        public string SortBy { get; set; }
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
        public bool IsValid { get; set; }
        public string Search { get; set; }
        public bool Trackable { get; set; } = false;

        public CommonFilters() { }
        public CommonFilters(string sortBy, int pageSize, int pageNumber, bool isValid, string search, bool trackable)
        {
            SortBy = sortBy;
            PageSize = pageSize;
            PageNumber = pageNumber;
            IsValid = isValid;
            Search = search;
            Trackable = trackable;
        }
    }
}
