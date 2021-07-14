using Vega.Extensions;

namespace Vega.Core.Models
{
    public class VehicleQuery : ISortQuery
    {
        public int? MakeId { get; set; }
        public string SortBy { get; set; }
        public bool IsAscending { get; set; }
    }
}