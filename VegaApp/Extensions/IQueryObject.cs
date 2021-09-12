namespace Vega.Extensions
{
    public interface IQueryObject
    {
        string SortBy { get; set; }
        bool IsAscending { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}