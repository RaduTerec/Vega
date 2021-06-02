using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Vega.Models.DataTransferObjects
{
    public class VehicleDTO
    {
        public long Id { get; set; }

        public string ContactName { get; set; }

        public string ContactPhone { get; set; }

        public Model Model { get; set; }

        public ICollection<Feature> Features { get; }

        public DateTime LastUpdate { get; set; }

        public VehicleDTO()
        {
            Features = new Collection<Feature>();
        }
    }
}