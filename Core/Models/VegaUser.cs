using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Vega.Core.Models
{
    public class VegaUser : IdentityUser
    {
        [StringLength(255)]
        public string Name { get; set; }
    }
}