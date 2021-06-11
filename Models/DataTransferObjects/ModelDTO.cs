using System.ComponentModel.DataAnnotations;

namespace Vega.Models.DataTransferObjects
{
    public class ModelDTO
    {
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}