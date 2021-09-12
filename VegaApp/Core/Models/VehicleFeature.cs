using System.ComponentModel.DataAnnotations;

namespace Vega.Core.Models
{
    public class VehicleFeature
    {
        public long VehicleId { get; set; }
        [Required]
        public Vehicle Vehicle { get; set; }

        public long FeatureId { get; set; }
        [Required]
        public Feature Feature { get; set; }
    }
}