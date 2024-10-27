namespace ProjectPRN231_API.DTO
{
    public class RegisterRequest
    {
        public string username { get; set; }
        public string password { get; set; }
        public string re_password { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string registCode { get; set; } = "";
    }
}
