using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.implementations;
using Service;
using BusinessObject;
using ProjectPRN231_API.DTO;

namespace ProjectPRN231_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private static UserService userService = new UserServiceImpl();

        [HttpGet("find-by-account-id/{accountId}")]
        public IActionResult FindUserByAccountId(int accountId)
        {
            User user = userService.FindByAccountId(accountId);
            if (user == null)
            {
                return BadRequest("Cannot find the user with this account!");
            }
            return Ok(user);
        }

        [HttpPut]
        public IActionResult UpdateAccount(UpdateUserRequest request)
        {
            User user = userService.FindByAccountId(request.AccountId);
            if (user == null)
            {
                return BadRequest("Cannot find the user with this account!");
            }

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Address = request.Address;
            user.Email = request.Email;
            user.Phone = request.Phone;
            user.Gender = request.Gender;
            user.BirthDate = request.BirthDate;
            userService.Update(user);
            return Ok("Update account successfully");
        }
    }
}
