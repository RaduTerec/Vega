using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Vega.Models
{
    /// <summary>
    /// Car feature like airbag, ABS, etc
    /// </summary>
    public class Feature
    {
        public long Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public ICollection<Vehicle> Vehicles { get; }

        public Feature()
        {
            Vehicles = new Collection<Vehicle>();
        }
    }
}
