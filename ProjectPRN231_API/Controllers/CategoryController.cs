using BusinessObject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.implementations;

namespace ProjectPRN231_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService categoryService = new CategoryServiceImpl();

        [HttpGet]
        public IActionResult GetAllCategories()
        {
            List<Category> categories = categoryService.GetAll();
            return Ok(categories);
        }
    }
}
