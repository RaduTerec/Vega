using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Vega.Models.DataTransferObjects
{
    public class VehicleDTO
    {
        public long Id { get; set; }
        public long ModelId { get; set; }
        public bool IsRegistered { get; set; }
        public ContactDTO Contact { get; set; }
        public ICollection<long> Features { get; set; }

        public VehicleDTO()
        {
            Features = new Collection<long>();
        }
    }
}