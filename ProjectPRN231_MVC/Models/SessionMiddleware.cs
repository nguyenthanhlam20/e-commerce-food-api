using Microsoft.AspNetCore.Mvc;

namespace ProjectPRN231_MVC.Models
{
    public class SessionMiddleware
    {
        private readonly RequestDelegate _next;

        public SessionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var userId = context.Session.GetInt32("UserId");

            // Ensure that login, register, and API routes are accessible without a session
            var path = context.Request.Path.Value.ToLower();

            if (userId == null && !path.Contains("/authentication") && !path.Contains("/user/home"))
            {
                // Set a message in HttpContext.Items
                // Set a message in Session
                context.Session.SetString("SessionExpiredMessage", "Your session has expired, please log in again.");

                // Redirect to login page if session is missing and request is not for login/register
                context.Response.Redirect("/Authentication/Login");
                return;
            }

            await _next(context);
        }
    }
}
