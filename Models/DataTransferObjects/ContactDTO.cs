using System.ComponentModel.DataAnnotations;

namespace Vega.Models.DataTransferObjects
{
    public class ContactDTO
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        [Phone]
        [StringLength(255)]
        public string Phone { get; set; }
    }
}