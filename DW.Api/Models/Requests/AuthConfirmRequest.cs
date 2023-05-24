namespace DW.Api.Models.Requests
{
    public class AuthConfirmRequest
    {
        public string Code { get; set; }
        public string EmailCode { get; set; }
    }
}
