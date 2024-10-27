using Microsoft.AspNetCore.Mvc;

namespace ProjectPRN231_MVC.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
