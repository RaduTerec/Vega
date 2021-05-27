using System.ComponentModel.DataAnnotations;

namespace Vega.Models
{
    /// <summary>
    /// Holds the car model. Ex: Vitarra, Corsa, etc
    /// </summary>
    public class Model
    {
        public long Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public Make Make { get; set; }
        
        public long MakeId { get; set; }
    }
}