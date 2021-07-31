using System.ComponentModel.DataAnnotations;

namespace Vega.Controllers.DataTransferObjects
{
    public class LoginDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}