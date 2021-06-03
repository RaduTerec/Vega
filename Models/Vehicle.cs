using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Vega.Models
{
    public class Vehicle
    {
        public long Id { get; set; }

        public long ModelId { get; set; }
        [Required]
        public Model Model { get; set; }

        [Required]
        public bool IsRegistered { get; set; }

        [Required]
        public ICollection<Feature> Features { get; }

        [Required]
        [StringLength(255)]
        public string ContactName { get; set; }

        [StringLength(255)]
        public string ContactEmail { get; set; }

        [Required]
        [StringLength(255)]
        public string ContactPhone { get; set; }

        public DateTime LastUpdate { get; set; }

        public Vehicle()
        {
            Features = new Collection<Feature>();
            LastUpdate = DateTime.Now;
        }
    }
}