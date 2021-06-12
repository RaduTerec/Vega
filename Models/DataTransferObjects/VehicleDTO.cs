using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Vega.Models.DataTransferObjects
{
    public class VehicleDTO
    {
        public long Id { get; set; }
        public ModelDTO Model { get; set; }
        public MakeDTO Make { get; set; }
        public bool IsRegistered { get; set; }
        public ContactDTO Contact { get; set; }
        public ICollection<FeatureDTO> Features { get; set; }
        public DateTime LastUpdate { get; set; }

        public VehicleDTO()
        {
            Features = new Collection<FeatureDTO>();
        }
    }
}