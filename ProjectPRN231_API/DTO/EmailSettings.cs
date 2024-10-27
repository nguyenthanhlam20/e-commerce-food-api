namespace ProjectPRN231_API.DTO
{
    public class EmailSettings
    {
        public string SMTPHost { get; set; }
        public int SMTPPort { get; set; }
        public string SMTPUser { get; set; }
        public string SMTPPass { get; set; }
        public string SenderEmail { get; set; }
        public string SenderName { get; set; }
    }
}
