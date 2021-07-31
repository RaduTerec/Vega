using System.ComponentModel.DataAnnotations;

namespace Vega.Controllers.DataTransferObjects
{
    public class UserDTO
    {
        [StringLength(255)]
        public string Name { get; set; }
        
        [StringLength(255)]
        public string Username { get; set; }
        
        [EmailAddress]
        [StringLength(255)]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}