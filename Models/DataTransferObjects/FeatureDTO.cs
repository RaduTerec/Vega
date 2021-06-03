using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Vega.Models.DataTransferObjects
{
    public class FeatureDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ICollection<VehicleDTO> Vehicles { get; }

        public FeatureDTO()
        {
            Vehicles = new Collection<VehicleDTO>();
        }
    }
}
