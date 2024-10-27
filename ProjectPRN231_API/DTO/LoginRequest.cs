using System.ComponentModel.DataAnnotations;

namespace ProjectPRN231_API.DTO
{
    public class LoginRequest
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}
