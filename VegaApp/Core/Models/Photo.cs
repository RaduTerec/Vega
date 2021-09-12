using System.ComponentModel.DataAnnotations;

namespace Vega.Core.Models
{
    public class Photo
    {
        public long Id { get; set; }
        
        [Required]
        [StringLength(255)]
        public string FileName { get; set; }
        public long VehicleId { get; set; }
    }
}