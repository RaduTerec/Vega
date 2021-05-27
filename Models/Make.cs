using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Vega.Models
{
    public class Make
    {
        public long Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        
        public ICollection<Model> Models { get; }

        public Make()
        {
            Models = new Collection<Model>();
        }
    }
}
