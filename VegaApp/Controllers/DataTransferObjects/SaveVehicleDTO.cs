using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Vega.Controllers.DataTransferObjects
{
    public class SaveVehicleDTO
    {
        public long Id { get; set; }
        public long ModelId { get; set; }
        public bool IsRegistered { get; set; }

        [Required]
        public ContactDTO Contact { get; set; }
        public ICollection<long> Features { get; set; }

        public SaveVehicleDTO()
        {
            Features = new Collection<long>();
        }
    }
}