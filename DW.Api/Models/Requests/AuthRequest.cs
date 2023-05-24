using System.ComponentModel.DataAnnotations;

namespace DW.Api.Models.Requests
{
    public class AuthRequest
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
