namespace Vega.Controllers.DataTransferObjects
{
    public class VehicleQueryDTO
    {
        public int? MakeId { get; set; }
        public string SortBy { get; set; }
        public bool IsAscending { get; set; }
    }
}