using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Vega.Models
{
    public class Vehicle
    {
        public long Id { get; set; }

        [Required]
        [StringLength(255)]
        public string ContactName { get; set; }

        [Required]
        [StringLength(255)]
        public string ContactPhone { get; set; }

        [Required]
        public Model Model { get; set; }

        [Required]
        public ICollection<Feature> Features { get; }

        public DateTime LastUpdate { get; set; }

        public Vehicle()
        {
            Features = new Collection<Feature>();
            LastUpdate = DateTime.Now;
        }
    }
}