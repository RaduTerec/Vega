namespace Vega.Extensions
{
    public interface ISortQuery
    {
        string SortBy { get; set; }
        bool IsAscending { get; set; }
    }
}