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
    public class OrderController : ControllerBase
    {
        private static OrderService orderService = new OrderServiceImpl();
        private static OrderDetailService orderDetailService = new OrderDetailServiceImpl();
        private static UserService userService = new UserServiceImpl();
        private static ProductService productService = new ProductServiceImpl();
        private static CartService cartService = new CartServiceImpl();
        private static CartItemService cartItemService = new CartItemServiceImpl();

        [HttpPost("checkout")]
        public IActionResult checkOut(CheckoutRequest request)
        {
            int userId = int.Parse(request.userId);

            if (userService.FindById(userId) == null)
            {
                return BadRequest("This user is not exists");
            }
            else if (DateTime.Parse(request.requireDate) < DateTime.Now)
            {
                return BadRequest("Requested date cannot be less than current date");
            }
            else if (request.shipAddress == null)
            {
                return BadRequest("Order address cannot be blank");
            }
            else
            {
                // do nothing
            }

            foreach (CartRequest cr in request.carts)
            {
                string productId = cr.productId;
                Product product = productService.FindById(int.Parse(productId));
                if (product == null || product.IsDeleted == true)
                {
                    return BadRequest("Product is no longer exist");
                }
                else if (int.Parse(cr.quantity) > product.NumberOfStock)
                {
                    return BadRequest("The quantity ordered for the product " + product.ProductName + " exceeds the quantity in stock.");
                }
                else
                {
                    // do nothing and continue
                }
            }

            Cart cart = cartService.FindAllCartByUserId(userId);

            Order order = new Order(userId, DateTime.Now, DateTime.Parse(request.requireDate), request.shipAddress, double.Parse(request.shipPrice), false);
            orderService.CreateOrder(order);

            OrderDetail orderDetail;
            foreach (CartRequest cr in request.carts)
            {
                int productId = int.Parse(cr.productId);
                int quantity = int.Parse(cr.quantity);
                double? totalPrice = productService.FindById(productId).UnitPrice * quantity; 

                orderDetail = new OrderDetail(productId, quantity, totalPrice);
                orderDetail.OrderId = order.OrderID;
                orderDetailService.CreateOrderDetail(orderDetail);

                Product product = productService.FindById(productId);
                product.NumberOfStock -= quantity;
                productService.Update(product);

                cartItemService.DeleteItem(cart.CartId, productId);
            }
            cartService.DeleteCart(cart.CartId);

            return Ok(order);
        }

        [HttpGet("order-history/{userId}")]
        public IActionResult orderHistory(int userId)
        {
            List<Order> orderHistory = new List<Order>();

            orderHistory = orderService.FindAllByUserId(userId);

            return Ok(orderHistory);
        }
    }
}
