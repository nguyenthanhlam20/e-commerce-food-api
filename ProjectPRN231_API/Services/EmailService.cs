using Microsoft.Extensions.Options;
using ProjectPRN231_API.DTO;
using System.Net.Mail;
using System.Net;

namespace ProjectPRN231_API.Services
{
    public class EmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            var smtpClient = new SmtpClient(_emailSettings.SMTPHost)
            {
                Port = _emailSettings.SMTPPort,
                Credentials = new NetworkCredential(_emailSettings.SMTPUser, _emailSettings.SMTPPass),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailSettings.SenderEmail, _emailSettings.SenderName),
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };

            mailMessage.To.Add(toEmail);

            try
            {
                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (SmtpException ex)
            {
                // Handle the exception as necessary
                throw new InvalidOperationException($"Could not send email: {ex.Message}", ex);
            }
        }

        public int GenerateRandomFourDigits()
        {
            Random random = new Random();
            return random.Next(1000, 10000); // Generates a number between 1000 and 9999
        }

    }
}
