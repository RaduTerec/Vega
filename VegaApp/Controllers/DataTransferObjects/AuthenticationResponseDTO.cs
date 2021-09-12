using System.Collections.Generic;

namespace Vega.Controllers.DataTransferObjects
{
    public class AuthenticationResponseDTO
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public string Token { get; set; }
    }
}