using System.ComponentModel.DataAnnotations;

namespace DW.Api.Models.Requests
{
    public class RegRequest
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
