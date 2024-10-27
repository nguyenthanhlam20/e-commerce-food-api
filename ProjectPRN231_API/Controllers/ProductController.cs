using BusinessObject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Service;
using Service.implementations;

namespace ProjectPRN231_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private static ProductService productService = new ProductServiceImpl();

        [HttpGet("available-products")]
        public IActionResult GetAllAvailableProduct()
        {
            List<Product> productList = productService.GetAllAvailableProduct();
            return Ok(productList);
        }

        [HttpGet("new-arrival-products")]
        public IActionResult GetNewArrivalProduct()
        {
            List<Product> productList = productService.GetNewArrivalProduct();
            return Ok(productList);
        }

        [HttpGet("available-products-pagination")]
        public IActionResult GetAllAvailableProductPagination([FromQuery] int pageNumber,
            [FromQuery] int pageSize,
            [FromQuery] int sortOrder,
            [FromQuery] int? categoryId,
            [FromQuery] string? productName,
            [FromQuery] double? minPrice,
            [FromQuery] double? maxPrice)
        {
            List<Product> productList = productService.GetAllAvailableProductPagination(pageNumber, pageSize, sortOrder, categoryId, productName, minPrice, maxPrice);
            return Ok(productList);
        }

        [HttpGet("available-products-by-category")]
        public IActionResult GetAllAvailableProductByCategory([FromQuery] int? categoryId)
        {
            List<Product> productList = productService.GetAllAvailableProductByCategory(categoryId);
            return Ok(productList);
        }

        [HttpGet("available-products-by-name")]
        public IActionResult GetAllAvailableProductByName([FromQuery] string? productName)
        {
            List<Product> productList = productService.GetAllAvailableProductByName(productName);
            return Ok(productList);
        }

        [HttpGet("available-products-by-price")]
        public IActionResult GetAllAvailableProductByPrice([FromQuery] double? minPrice, double? maxPrice)
        {
            List<Product> productList = productService.GetAllAvailableProductByPrice(minPrice, maxPrice);
            return Ok(productList);
        }

        [HttpGet("product-by-id")]
        public IActionResult GetAllAvailableProductById([FromQuery] int? productId)
        {
            Product productList = productService.GetAllAvailableProductById(productId);
            return Ok(productList);
        }

        [HttpGet("product-related-by-id")]
        public IActionResult GetAllAvailableProductRelatedById([FromQuery] int? productId)
        {
            List<Product> productList = productService.GetAllAvailableProductRelatedById(productId);
            return Ok(productList);
        }
    }
}
