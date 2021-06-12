using System.ComponentModel.DataAnnotations;

namespace Vega.Core.Models
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
    }
}
