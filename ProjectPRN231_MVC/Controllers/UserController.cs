using BusinessObject;
using Microsoft.AspNetCore.Mvc;
using ProjectPRN231_API.DTO;
using ProjectPRN231_MVC.Models;
using System.Diagnostics.Metrics;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ProjectPRN231_MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly HttpClient client = null;
        private string ProductAPI = "";
        private string CategoryAPI = "";
        private string AccountAPI = "";
        private string UserAPI = "";
        private string EmailAPI = "";
        private string CartAPI = "";
        private string OrderAPI = "";

        public UserController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductAPI = "https://localhost:7115/api/Product";
            AccountAPI = "https://localhost:7115/api/Account";
            UserAPI = "https://localhost:7115/api/User";
            EmailAPI = "https://localhost:7115/api/Email";
            CategoryAPI = "https://localhost:7115/api/Category";
            CartAPI = "https://localhost:7115/api/Cart";
            OrderAPI = "https://localhost:7115/api/Order";
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Home()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            ViewBag.UserId = userId;

            Cart userCart = null;

            HttpResponseMessage responseUserCart = await client.GetAsync(CartAPI + "/get-cart-of-user/" + userId);
            if (responseUserCart.IsSuccessStatusCode)
            {
                string relatedData = await responseUserCart.Content.ReadAsStringAsync();
                var relatedOptions = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                userCart = JsonSerializer.Deserialize<Cart>(relatedData, relatedOptions);
            }
            double? totalPrice = 0;
            int quantityInCart = 0;
            if (userCart != null)
            {
                foreach (var cart in userCart.CartItems)
                {
                    quantityInCart += 1;
                    totalPrice += cart.TotalPrice;
                }
            }
            ViewBag.TotalPrice = totalPrice;
            ViewBag.Quantity = quantityInCart;

            List<Product> newArrivalProducts = new List<Product>();

            HttpResponseMessage response = await client.GetAsync(ProductAPI + "/new-arrival-products");
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                newArrivalProducts = JsonSerializer.Deserialize<List<Product>>(data, options);
                return View(newArrivalProducts);
            }
            return View(new List<Product>());
        }

        public async Task<IActionResult> Shop(int pageNumber = 1, int pageSize = 3, int sortOrder = 1, int? categoryId = null, string? productName = null, double? minPrice = null, double? maxPrice = null)
		{
            int? userId = HttpContext.Session.GetInt32("UserId");
            ViewBag.UserId = userId;

            Cart userCart = null;

            HttpResponseMessage responseUserCart = await client.GetAsync(CartAPI + "/get-cart-of-user/" + userId);
            if (responseUserCart.IsSuccessStatusCode)
            {
                string relatedData = await responseUserCart.Content.ReadAsStringAsync();
                var relatedOptions = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                userCart = JsonSerializer.Deserialize<Cart>(relatedData, relatedOptions);
            }
            double? totalPrice = 0;
            int quantityInCart = 0;
            if (userCart != null)
            {
                foreach (var cart in userCart.CartItems)
                {
                    quantityInCart += 1;
                    totalPrice += cart.TotalPrice;
                }
            }
            ViewBag.TotalPrice = totalPrice;
            ViewBag.Quantity = quantityInCart;

            List<Product> availableProducts = new List<Product>();
            List<Product> paginatedProducts = new List<Product>();
            List<Category> categories = new List<Category>();

            HttpResponseMessage responseAllProduct = await client.GetAsync(ProductAPI + "/available-products");
            if (responseAllProduct.IsSuccessStatusCode)
            {
                string data = await responseAllProduct.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                availableProducts = JsonSerializer.Deserialize<List<Product>>(data, options);
            }
            else
            {
                availableProducts = new List<Product>();
            }

            HttpResponseMessage responseAllCategory = await client.GetAsync(CategoryAPI);

            if (responseAllCategory.IsSuccessStatusCode)
            {
                string data = await responseAllCategory.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                categories = JsonSerializer.Deserialize<List<Category>>(data, options);
                if (categories == null || categories.Count() == 0)
                {
                    return View(new List<Category>());
                }
            }

                HttpResponseMessage response = await client.GetAsync(ProductAPI + "/available-products-pagination?" 
                    + "pageNumber=" + pageNumber 
                    + "&pageSize=" + pageSize 
                    + "&sortOrder=" + sortOrder
                    + "&categoryId=" + categoryId
                    + "&productName=" + productName
                    + "&minPrice=" + minPrice
                    + "&maxPrice=" + maxPrice);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                paginatedProducts = JsonSerializer.Deserialize<List<Product>>(data, options);
                if (paginatedProducts == null || paginatedProducts.Count() == 0)
                {
                    ViewBag.Categories = categories;
                    return View(new List<Product>());
                }

                // Get the total number of products available
                int totalProductsAvailable = availableProducts.Count();

                // Get the total number of products on the current paginated page
                int totalProducts = paginatedProducts.Count();

                // Calculate total number of pages
                int totalPages = (int)Math.Ceiling(totalProductsAvailable / (double)pageSize);

                if (categoryId != null && productName == null)
                {
                    HttpResponseMessage responseProductWithCategory = await client.GetAsync(ProductAPI + "/available-products-by-category?categoryId=" + categoryId);
                    if (responseProductWithCategory.IsSuccessStatusCode)
                    {
                        string dataProduct = await responseProductWithCategory.Content.ReadAsStringAsync();
                        var optionsProduct = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        List<Product> productsWithCategory = JsonSerializer.Deserialize<List<Product>>(dataProduct, optionsProduct);
                        if (productsWithCategory == null || productsWithCategory.Count() == 0)
                        {
                            ViewBag.Categories = categories;
                            return View(new List<Product>());
                        }
                        totalProductsAvailable = productsWithCategory.Count();
                        totalPages = (int)Math.Ceiling(totalProductsAvailable / (double)pageSize);
                    }
                }
                else if (categoryId == null && productName != null)
                {
                    HttpResponseMessage responseProductWithName = await client.GetAsync(ProductAPI + "/available-products-by-name?productName=" + productName);
                    if (responseProductWithName.IsSuccessStatusCode)
                    {
                        string dataProduct = await responseProductWithName.Content.ReadAsStringAsync();
                        var optionsProduct = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        List<Product> productsWithName = JsonSerializer.Deserialize<List<Product>>(dataProduct, optionsProduct);
                        if (productsWithName == null || productsWithName.Count() == 0)
                        {
                            ViewBag.Categories = categories;
                            return View(new List<Product>());
                        }
                        totalProductsAvailable = productsWithName.Count();
                        totalPages = (int)Math.Ceiling(totalProductsAvailable / (double)pageSize);
                    }
                }
                else if (categoryId != null && productName != null)
                {
                    HttpResponseMessage responseProductWithCategory = await client.GetAsync(ProductAPI + "/available-products-by-category?categoryId=" + categoryId);
                    if (responseProductWithCategory.IsSuccessStatusCode)
                    {
                        string dataProduct = await responseProductWithCategory.Content.ReadAsStringAsync();
                        var optionsProduct = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        List<Product> productsWithCategory = JsonSerializer.Deserialize<List<Product>>(dataProduct, optionsProduct);
                        if (productsWithCategory == null || productsWithCategory.Count() == 0)
                        {
                            ViewBag.Categories = categories;
                            return View(new List<Product>());
                        }

                        List<Product> productsWithCategoryAndName = productsWithCategory.Where(p => p.ProductName.Contains(productName)).ToList();
                        totalProductsAvailable = productsWithCategoryAndName.Count();
                        totalPages = (int)Math.Ceiling(totalProductsAvailable / (double)pageSize);
                    }
                }
                else
                {
                    // do nothing
                }
                // Filter by price
                if (minPrice != null && maxPrice != null)
                {
                    HttpResponseMessage responseProductWithName = await client.GetAsync(ProductAPI + "/available-products-by-price?minPrice=" + minPrice +"&maxPrice=" + maxPrice);
                    if (responseProductWithName.IsSuccessStatusCode)
                    {
                        string dataProduct = await responseProductWithName.Content.ReadAsStringAsync();
                        var optionsProduct = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        List<Product> productsWithPrice = JsonSerializer.Deserialize<List<Product>>(dataProduct, optionsProduct);
                        if (productsWithPrice == null || productsWithPrice.Count() == 0)
                        {
                            ViewBag.Categories = categories;
                            return View(new List<Product>());
                        }
                        totalProductsAvailable = productsWithPrice.Count();
                        totalPages = (int)Math.Ceiling(totalProductsAvailable / (double)pageSize);
                    }
                }
                

                // Determine if pagination is needed
                bool showPagination = totalProductsAvailable > pageSize;

                // Pass data to the view
                ViewBag.PageSize = pageSize;
                ViewBag.CurrentPage = pageNumber;
                ViewBag.TotalPages = totalPages;
                ViewBag.TotalProducts = totalProductsAvailable;
                ViewBag.SortOrder = sortOrder;
                ViewBag.ShowPagination = showPagination;  // Pass the boolean flag to the view
                ViewBag.CategoryId = categoryId;
                ViewBag.MinPrice = minPrice;
                ViewBag.MaxPrice = maxPrice;
            }
            ViewBag.Categories = categories;
            return View(paginatedProducts);
		}

        public async Task<IActionResult> AddToCart(int productId)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            ViewBag.UserId = userId;

            Cart userCart = null;

            HttpResponseMessage responseUserCart = await client.GetAsync(CartAPI + "/get-cart-of-user/" + userId);
            if (responseUserCart.IsSuccessStatusCode)
            {
                string relatedData = await responseUserCart.Content.ReadAsStringAsync();
                var relatedOptions = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                userCart = JsonSerializer.Deserialize<Cart>(relatedData, relatedOptions);
            }
            double? totalPrice = 0;
            int quantityInCart = 0;
            if (userCart != null)
            {
                foreach (var cart in userCart.CartItems)
                {
                    quantityInCart += 1;
                    totalPrice += cart.TotalPrice;
                }
            }
            ViewBag.TotalPrice = totalPrice;
            ViewBag.Quantity = quantityInCart;

            Product productById = new Product();
            List<Product> productRelatedList = new List<Product>();

            String msg = TempData["successMsg"] != null ? TempData["successMsg"].ToString() : "";
            ModelState.AddModelError("", msg);

            HttpResponseMessage responseProductById = await client.GetAsync(ProductAPI + "/product-by-id?productId=" + productId);
            if (responseProductById.IsSuccessStatusCode)
            {
                string data = await responseProductById.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                productById = JsonSerializer.Deserialize<Product>(data, options);
            }

            HttpResponseMessage responseProductRelated = await client.GetAsync(ProductAPI + "/product-related-by-id?productId=" + productId);
            if (responseProductRelated.IsSuccessStatusCode)
            {
                string relatedData = await responseProductRelated.Content.ReadAsStringAsync();
                var relatedOptions = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                productRelatedList = JsonSerializer.Deserialize<List<Product>>(relatedData, relatedOptions);
            }

            ViewBag.RelatedProducts = productRelatedList;
            return View(productById);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int quantity, int productId, double unitPrice)
        {
            // Retrieve the user ID from the session
            int? userId = HttpContext.Session.GetInt32("UserId");

            // Set userId for CartRequest
            CartRequest cartRequest = new CartRequest();
            cartRequest.userId = userId.ToString();
            cartRequest.productId = productId.ToString();
            cartRequest.quantity = quantity.ToString();
            cartRequest.unitPrice = unitPrice.ToString();

            HttpResponseMessage response = await client.PostAsJsonAsync(CartAPI + "/add-to-cart", cartRequest);

            string msg = await response.Content.ReadAsStringAsync();

            TempData["successMsg"] = msg;
            return RedirectToAction("AddToCart", "User", new { productId = productId});
        }

        public async Task<IActionResult> MyCart()
        {
            // Retrieve the user ID from the session
            int? userId = HttpContext.Session.GetInt32("UserId");
            ViewBag.UserId = userId;

            Cart userCart = null;

            HttpResponseMessage responseUserCart = await client.GetAsync(CartAPI + "/get-cart-of-user/" + userId);
            if (responseUserCart.IsSuccessStatusCode)
            {
                string relatedData = await responseUserCart.Content.ReadAsStringAsync();
                var relatedOptions = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                userCart = JsonSerializer.Deserialize<Cart>(relatedData, relatedOptions);
            }
            if(userCart == null)
            {
                return View(new Cart());
            }
            double? totalPrice = 0;
            int quantity = 0;
            foreach (var cart in userCart.CartItems)
            {
                quantity += 1;
                totalPrice += cart.TotalPrice;
            }

            ViewBag.TotalPrice = totalPrice;
            ViewBag.Quantity = quantity;
            return View(userCart);
        }

        public async Task<IActionResult> Checkout()
        {
            // Retrieve the user ID from the session
            int? userId = HttpContext.Session.GetInt32("UserId");

            Cart userCart = new Cart();

            HttpResponseMessage responseUserCart = await client.GetAsync(CartAPI + "/get-cart-of-user/" + userId);
            if (responseUserCart.IsSuccessStatusCode)
            {
                string relatedData = await responseUserCart.Content.ReadAsStringAsync();
                var relatedOptions = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                userCart = JsonSerializer.Deserialize<Cart>(relatedData, relatedOptions);
            }
            double? totalPrice = 0;
            int quantity = 0;
            foreach (var cart in userCart.CartItems)
            {
                quantity += 1;
                totalPrice += cart.TotalPrice;
            }

            ViewBag.TotalPrice = totalPrice;
            ViewBag.Quantity = quantity;

            var checkoutViewModel = new CheckoutViewModel
            {
                cart = userCart,
                request = new CheckoutRequest()
            };

            return View(checkoutViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(CheckoutViewModel model)
        {
            // Retrieve the user ID from the session
            int? userId = HttpContext.Session.GetInt32("UserId");

            Cart userCart = new Cart();

            HttpResponseMessage responseUserCart = await client.GetAsync(CartAPI + "/get-cart-of-user/" + userId);
            if (responseUserCart.IsSuccessStatusCode)
            {
                string relatedData = await responseUserCart.Content.ReadAsStringAsync();
                var relatedOptions = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                userCart = JsonSerializer.Deserialize<Cart>(relatedData, relatedOptions);
            }
            model.cart = userCart;
            double? totalPrice = userCart.CartItems.Sum(c => c.TotalPrice) + 100;
            ViewBag.TotalPrice = totalPrice;

            if (model.request.shipAddress == null || model.request.requireDate == null)
            {
                ModelState.AddModelError("", "Please fill all the required fields!");
                return View(model);
            }
            if (DateTime.Parse(model.request.requireDate) < DateTime.Now)
            {
                ModelState.AddModelError("", "Require date is invalid!");
                return View(model);
            }

            model.request.userId = userId.ToString();
            model.request.shipPrice = "100";

            List<CartRequest> cartRequests = new List<CartRequest>();
            foreach (CartItem cartItem in userCart.CartItems)
            {
                cartRequests.Add(new CartRequest(userId.ToString(), cartItem.ProductId.ToString(), cartItem.Quantity.ToString(), cartItem.UnitPrice.ToString()));
            }
            model.request.carts = cartRequests;
            HttpResponseMessage response = await client.PostAsJsonAsync(OrderAPI + "/checkout", model.request);

            string msg = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("CheckoutSuccess");
            }
            ModelState.AddModelError("", msg);
            return View(model);
        }

        public IActionResult CheckoutSuccess()
        {
            return View();
        }

        public async Task<IActionResult> MyCheckOutAsync()
        {
            // Retrieve the user ID from the session
            int? userId = HttpContext.Session.GetInt32("UserId");

            Cart userCart = new Cart();

            HttpResponseMessage responseUserCart = await client.GetAsync(CartAPI + "/get-cart-of-user/" + userId);
            if (responseUserCart.IsSuccessStatusCode)
            {
                string relatedData = await responseUserCart.Content.ReadAsStringAsync();
                var relatedOptions = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                userCart = JsonSerializer.Deserialize<Cart>(relatedData, relatedOptions);
            }
            double? totalPrice = 0;
            int quantity = 0;
            foreach (var cart in userCart.CartItems)
            {
                quantity += 1;
                totalPrice += cart.TotalPrice;
            }

            List<Order> orders = new List<Order>();
            HttpResponseMessage responseOrderHistory = await client.GetAsync(OrderAPI + "/order-history/" + userId);
            if (responseOrderHistory.IsSuccessStatusCode)
            {
                string data = await responseOrderHistory.Content.ReadAsStringAsync();
                var relatedOptions = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                orders = JsonSerializer.Deserialize<List<Order>>(data, relatedOptions);
            }

            ViewBag.TotalPrice = totalPrice;
            ViewBag.Quantity = quantity;

            return View(orders);
        }

        public async Task<IActionResult> ViewDetailOrderAsync(int orderId)
        {
            // Retrieve the user ID from the session
            int? userId = HttpContext.Session.GetInt32("UserId");

            Cart userCart = new Cart();

            HttpResponseMessage responseUserCart = await client.GetAsync(CartAPI + "/get-cart-of-user/" + userId);
            if (responseUserCart.IsSuccessStatusCode)
            {
                string relatedData = await responseUserCart.Content.ReadAsStringAsync();
                var relatedOptions = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                userCart = JsonSerializer.Deserialize<Cart>(relatedData, relatedOptions);
            }
            double? totalPrice = 0;
            int quantity = 0;
            foreach (var cart in userCart.CartItems)
            {
                quantity += 1;
                totalPrice += cart.TotalPrice;
            }

            List<Order> orders = new List<Order>();
            HttpResponseMessage responseOrderHistory = await client.GetAsync(OrderAPI + "/order-history/" + userId);
            if (responseOrderHistory.IsSuccessStatusCode)
            {
                string data = await responseOrderHistory.Content.ReadAsStringAsync();
                var relatedOptions = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                orders = JsonSerializer.Deserialize<List<Order>>(data, relatedOptions);
            }

            List<OrderDetail> orderDetails = orders.SingleOrDefault(o => o.OrderID == orderId).OrderDetails.ToList();

            ViewBag.TotalPrice = totalPrice;
            ViewBag.Quantity = quantity;

            return View(orderDetails);
        }
    }
}
