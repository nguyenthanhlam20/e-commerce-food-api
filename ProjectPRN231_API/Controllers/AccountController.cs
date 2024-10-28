using BusinessObject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectPRN231_API.DTO;
using Service;
using Service.implementations;

namespace ProjectPRN231_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private static AccountService accountService = new AccountServiceImpl();
        private static UserService userService = new UserServiceImpl();

        [HttpPost("register")]
        public IActionResult register([FromBody] RegisterRequest request)
        {
            Account newAccount= new Account(request.username, request.password, true);
            accountService.Create(newAccount);

            User newUser = new User(request.email, request.phone);

            newUser.AccountId = newAccount.AccountId;
            newUser.RoleId = 2;
            
            
            userService.Create(newUser);

            return Ok(newAccount.AccountId);
        }

        [HttpPost("login")]
        public IActionResult login([FromBody] LoginRequest request)
        {
            Account account = accountService.FindByUsernameAndPassword(request.username, request.password);
            if (account == null)
            {
                return BadRequest("Username or password is incorrect!");
            }

            var response = new LoginResponse();
            response.AccountId = account.AccountId;
            response.UserId = account.User!.UserId;

            return Ok(response);
        }

        [HttpPut("forget-password")]
        public IActionResult forgetPassword([FromBody] ResetPasswordRequest request)
        {
            User user = userService.FindByEmail(request.email);
            if (user == null)
            {
                return BadRequest("User with email " + request.email + " no longer exist!");
            }
            if(request.password == null || request.confirmPassword == null)
            {
                return BadRequest("Information cannot be blank!");
            }
            int accountId = user.AccountId;
            Account account = accountService.FindById(accountId);

            account.Password = request.confirmPassword;
            accountService.Update(account);

            return Ok(account);
        }
    }
}
