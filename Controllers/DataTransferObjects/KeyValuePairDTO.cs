using System.ComponentModel.DataAnnotations;

namespace Vega.Controllers.DataTransferObjects
{
    public class KeyValuePairDTO
    {
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
