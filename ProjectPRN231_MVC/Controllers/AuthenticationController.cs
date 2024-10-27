using BusinessObject;
using Microsoft.AspNetCore.Mvc;
using ProjectPRN231_API.DTO;
using System.Diagnostics.Metrics;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;

namespace ProjectPRN231_MVC.Controllers
{
    public class AuthenticationController : Controller
    {
		private readonly HttpClient client = null;
		private string AccountAPI = "";
		private string UserAPI = "";
		private string EmailAPI = "";

		public AuthenticationController()
		{
			client = new HttpClient();
			var contentType = new MediaTypeWithQualityHeaderValue("application/json");
			client.DefaultRequestHeaders.Accept.Add(contentType);
			AccountAPI = "https://localhost:7115/api/Account";
			UserAPI = "https://localhost:7115/api/User";
			EmailAPI = "https://localhost:7115/api/Email";
		}

		public IActionResult Index()
        {
            return View();
        }

		public ActionResult Login()
		{
            string msg = null;
            // Check if the middleware has set any message
            // Check if session contains the message
            var sessionMessage = HttpContext.Session.GetString("SessionExpiredMessage");

            if (!string.IsNullOrEmpty(sessionMessage))
            {
                // Move the message from session to TempData
                TempData["sessionExpired"] = sessionMessage;

                // Optionally, clear the session message after moving it to TempData
                HttpContext.Session.Remove("SessionExpiredMessage");

                msg = TempData["sessionExpired"] != null ? TempData["sessionExpired"].ToString() : "";
            }
            else
            {
                msg = TempData["successMsg"] != null ? TempData["successMsg"].ToString() : "";
            }
            ModelState.AddModelError("", msg);
            return View();
		}

        public ActionResult Register()
        {
            String msg = TempData["failMsg"] != null ? TempData["failMsg"].ToString() : "";
            ModelState.AddModelError("", msg);
            return View();
        }

        public IActionResult LogOut()
        {
            // Clear session and cookies
            HttpContext.Session.Clear();
            Response.Cookies.Delete(".AspNetCore.Session");

            return RedirectToAction("Login");
        }

        public ActionResult ForgetPassword()
        {
            String msg = TempData["failMsg"] != null ? TempData["failMsg"].ToString() : "";
            ModelState.AddModelError("", msg);
            return View();
        }

        public ActionResult ConfirmEmailCode()
        {
            return View();
        }

        public ActionResult ConfirmEmailCodeRegister()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ConfirmEmailCodeRegister(RegisterRequest request)
        {
            string code = TempData["registCode"] != null ? TempData["registCode"].ToString() : "";
            string email = TempData["email"] != null ? TempData["email"].ToString() : "";
            string phone = TempData["phone"] != null ? TempData["phone"].ToString() : "";
            string username = TempData["username"] != null ? TempData["username"].ToString() : "";
            string password = TempData["confirmPassword"] != null ? TempData["confirmPassword"].ToString() : "";

            if (request.registCode.Equals(code))
            {
                request.email = email;
                request.phone = phone;
                request.username = username;
                request.password = password;
                request.re_password = password;

                HttpResponseMessage response = await client.PostAsJsonAsync(AccountAPI + "/register", request);

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMsg"] = "You have registered successfully! Please log in";

                    return RedirectToAction("Login");
                }
            }
            TempData["failMsg"] = "Register failed! Try again";
            return RedirectToAction("Register");
        }

        [HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> ConfirmEmailCode(ResetPasswordRequest request)
		{
			String code = TempData["resetCode"] != null ? TempData["resetCode"].ToString() : "";
			String email = TempData["email"] != null ? TempData["email"].ToString() : "";
			String password = TempData["resetPassword"] != null ? TempData["resetPassword"].ToString() : "";

            if (request.resetCode.Equals(code))
            {
                request.email = email;
                request.password = password;
                request.confirmPassword = password;

                HttpResponseMessage response = await client.PutAsJsonAsync(AccountAPI + "/forget-password", request);

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMsg"] = "You have reset password successfully! Please log in";

                    return RedirectToAction("Login");
                }
            }
            TempData["failMsg"] = "Reset password failed! Try again";
            return RedirectToAction("ForgetPassword");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> ForgetPassword(ResetPasswordRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            string patternPassword = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\W).{8,}$";
            Regex regex = new Regex(patternPassword);

            if (!regex.IsMatch(request.password) || !regex.IsMatch(request.confirmPassword))
            {
                ModelState.AddModelError("", "Password must be at lease 8 - 20 characters, including 1 uppercase, 1 lowercase and 1 speacial character!");
                return View(request);
            }

            if (!request.password.Equals(request.confirmPassword))
            {
                ModelState.AddModelError("", "Password must be matched!");
                return View(request);
            }

            HttpResponseMessage response = await client.PostAsJsonAsync(EmailAPI + "/send", request);

			string msg = await response.Content.ReadAsStringAsync();
			if (response.IsSuccessStatusCode)
            {
                TempData["resetCode"] = msg;
                TempData["email"] = request.email;
                TempData["resetPassword"] = request.confirmPassword;
                return RedirectToAction("ConfirmEmailCode");
            }
            ModelState.AddModelError("", msg);
            return View(request);
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Login(LoginRequest request)
		{
			if(!ModelState.IsValid)
			{
				return View(request);
			}

			HttpResponseMessage response = await client.PostAsJsonAsync(AccountAPI + "/login", request);

			if (response.IsSuccessStatusCode)
			{
				//Member m = await response.Content.ReadFromJsonAsync<Member>();
				//return RedirectToAction("Home", "User", new { id = m.MemberId });
				Account account = await response.Content.ReadFromJsonAsync<Account>();
				int accountId = account.AccountId;

				response = await client.GetAsync(UserAPI + "/find-by-account-id/" + accountId);

                if (!response.IsSuccessStatusCode)
                {
					return View(request);
                }

                User user = await response.Content.ReadFromJsonAsync<User>();

				// Store user information in Session
				HttpContext.Session.SetInt32("UserId", user.UserId);
				HttpContext.Session.SetString("Username", account.Username);

				return RedirectToAction("Home", "User");
			}
			ModelState.AddModelError("", "Your username or password is incorrect!");
			return View(request);
		}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
			
			string patternEmail = "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$";
			Regex regex = new Regex(patternEmail);

			if (!regex.IsMatch(request.email))
			{
				ModelState.AddModelError("", "Email must be in correct format (Ex: daylaemail@gmail.com, ...)");
				return View(request);
			}
			string patternPhone = "^(03|05|07|08|09|01[2|6|8|9])+([0-9]{8})$";
			regex = new Regex(patternPhone);

			if (!regex.IsMatch(request.phone))
			{
				ModelState.AddModelError("", "Phone number must be in correct format (Ex: 091..., 054...)");
				return View(request);
			}
			string patternUsername = "^[a-z0-9]{8,}$";
            regex = new Regex(patternUsername);

            if (!regex.IsMatch(request.username))
            {
                ModelState.AddModelError("", "Username must be at lease 8 - 20 characters, do not including special character and uppercase character!");
                return View(request);
            }

            string patternPassword = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\W).{8,}$";
            regex = new Regex(patternPassword);

            if (!regex.IsMatch(request.password) || !regex.IsMatch(request.re_password))
            {
                ModelState.AddModelError("", "Password must be at lease 8 - 20 characters, including 1 uppercase, 1 lowercase and 1 speacial character!");
                return View(request);
            }

            if (!request.password.Equals(request.re_password))
            {
                ModelState.AddModelError("", "Password must be matched!");
                return View(request);
            }

            ResetPasswordRequest resetPasswordRequest = new ResetPasswordRequest();
            resetPasswordRequest.email = request.email;
            resetPasswordRequest.password = request.password;
            resetPasswordRequest.confirmPassword = request.re_password;

            HttpResponseMessage response = await client.PostAsJsonAsync(EmailAPI + "/send", resetPasswordRequest);

            string msg = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                TempData["registCode"] = msg;
                TempData["email"] = resetPasswordRequest.email;
                TempData["phone"] = request.phone;
                TempData["username"] = request.username;
                TempData["confirmPassword"] = resetPasswordRequest.confirmPassword;
                return RedirectToAction("ConfirmEmailCodeRegister");
            }
            ModelState.AddModelError("", msg);
            return View(request);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> ForgetPassword(ResetPasswordRequest request)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(request);
        //    }
        //    string patternPassword = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\W).{8,}$";
        //    Regex regex = new Regex(patternPassword);

        //    if (!regex.IsMatch(request.password) && !regex.IsMatch(request.confirmPassword))
        //    {
        //        ModelState.AddModelError("", "Password must be at lease 8 - 20 characters, including 1 uppercase, 1 lowercase and 1 speacial character!");
        //        return View(request);
        //    }

        //    if (!request.password.Equals(request.confirmPassword))
        //    {
        //        ModelState.AddModelError("", "Password must be matched!");
        //        return View(request);
        //    }

        //    HttpResponseMessage response = await client.PostAsJsonAsync(AccountAPI + "/forget-password", request);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        TempData["successMsg"] = "You have reset password successfully! Please log in";

        //        return RedirectToAction("Login");
        //    }
        //    string msg = await response.Content.ReadAsStringAsync();
        //    ModelState.AddModelError("", msg);
        //    return View(request);
        //}
    }
}
