using BusinessObject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectPRN231_API.DTO;
using ProjectPRN231_API.Services;
using Service;
using Service.implementations;

namespace ProjectPRN231_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly EmailService _emailService;
        private static AccountService accountService = new AccountServiceImpl();
        private static UserService userService = new UserServiceImpl();

        public EmailController(EmailService emailService)
        {
            _emailService = emailService;

        }

        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromBody] ResetPasswordRequest request)
        {
            if (string.IsNullOrEmpty(request.email))
            {
                return BadRequest("Please provide a valid email address.");
            }
            if (request.email == null || request.phone == null || request.password == null || request.confirmPassword == null)
            {
                return BadRequest("Informations cannot be blank!");
            }
            Account existedUsername = accountService.FindByUsername(request.username);
            if (existedUsername != null)
            {
                return BadRequest("This username is existed!");
            }
            User existedEmail = userService.FindByEmail(request.email);
            if (existedEmail != null)
            {
                return BadRequest("This email is existed!");
            }
            User existedPhone = userService.FindByPhoneNumber(request.phone);
            if (existedPhone != null)
            {
                return BadRequest("This phone number is existed!");
            }
            // Generate reset code
            int code = _emailService.GenerateRandomFourDigits();

            string subject = "Email Confirmation";
            string message = "<h2>Hello!</h2><p>This is a email from Male-Fashion. Your reset code is " + code + ", please enter this code to complete reset your password</p>";

            await _emailService.SendEmailAsync(request.email, subject, message);

            return Ok(code);
        }
    }
}
