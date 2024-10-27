using System.Text.Json.Serialization;

namespace ProjectPRN231_API.DTO
{
    public class ResetPasswordRequest
    {
        public string email {  get; set; }
        public string phone { get; set; } = "";
        public string username { get; set; } = "";
        public string password { get; set; }
        public string confirmPassword { get; set; }
        public string resetCode { get; set; } = "";
    }
}
