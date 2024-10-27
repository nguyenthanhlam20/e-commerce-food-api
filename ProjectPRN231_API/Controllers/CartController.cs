using BusinessObject;
using Microsoft.AspNetCore.Mvc;
using ProjectPRN231_API.DTO;
using Service;
using Service.implementations;

namespace ProjectPRN231_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private static CartService cartService = new CartServiceImpl();
        private static CartItemService cartItemService = new CartItemServiceImpl();
        private static UserService userService = new UserServiceImpl();
        private static ProductService productService = new ProductServiceImpl();

        [HttpGet("get-cart-of-user/{userId}")]
        public IActionResult GetCartByUserId([FromRoute] int userId)
        {
            Cart cart = cartService.FindAllCartByUserId(userId);
            if (cart == null)
            {
                return NotFound("Cannot find your cart!");
            }
            return Ok(cart);
        }

        [HttpPost("add-to-cart")]
        public IActionResult AddToCart([FromBody]CartRequest request)
        {
            CartItem cartItem = cartItemService.FindByProductIdAndUserId(int.Parse(request.productId), int.Parse(request.userId));

            User user = userService.FindById(int.Parse(request.userId));
            if (user == null)
            {
                return BadRequest("This user is no longer exists");
            }
            Product product = productService.FindById(int.Parse(request.productId));
            if (product == null || product.IsDeleted == true)
            {
                return BadRequest("This product is no longer exist");

            }else if (product.NumberOfStock < int.Parse(request.quantity))
            {
                return BadRequest("The number of products added to the cart exceeds the quantity in stock");
            }else
            {
                int userId = int.Parse(request.userId);
                int productId = int.Parse(request.productId);
                int quantity = int.Parse(request.quantity);
                decimal unitPrice = decimal.Parse(request.unitPrice);
                decimal totalPrice = quantity * unitPrice;

                Cart cart = cartService.FindAllCartByUserId(userId);

                if (cart == null)
                {
                    cart = new Cart(userId);
                    cartService.CreateCart(cart);

                    cartItem = new CartItem(cart.CartId, productId, quantity, unitPrice, (double)totalPrice);
                    cartItemService.CreateCartItem(cartItem);
                }
                else
                {
                    cart.UpdateAt = DateTime.Now;
                    cartService.UpdateCart(cart);

                    if (cartItem == null)
                    {
                        cartItem = new CartItem(cart.CartId, productId, quantity, unitPrice, (double)totalPrice);
                        cartItemService.CreateCartItem(cartItem);
                    }
                    else
                    {
                        cartItem.Quantity += quantity;
                        cartItem.UnitPrice = unitPrice;
                        cartItem.TotalPrice = (double?)(cartItem.Quantity * cartItem.UnitPrice);
                        cartItemService.UpdateCartItem(cartItem);
                    }
                }

                return Ok("Add to cart successfully");
            }
        }

        [HttpPut("{cartId}")]
        public IActionResult UpdateCart(int cartId, [FromBody]CartRequest request)
        {

            List<CartItem> cartItems = cartItemService.GetAllByCartId(cartId);

            foreach (var c in cartItems)
            {
                if(c.ProductId == int.Parse(request.productId))
                {
                    c.Quantity = int.Parse(request.quantity);
                    c.UnitPrice = decimal.Parse(request.unitPrice);
                    c.TotalPrice = (double?)(c.Quantity * c.UnitPrice);
                    cartItemService.UpdateCartItem(c);
                    return Ok();
                }
            }
            return Ok();
        }

        [HttpDelete("{userId}/{productId}")]
        public IActionResult DeleteItemInCart(int userId, int productId)
        {
            CartItem cartItem = cartItemService.FindByProductIdAndUserId(productId, userId);
            if (cartItem == null)
            {
                return BadRequest("This product no longer exists in your cart!");
            }
            int cartId = cartItem.CartId;
            cartItemService.DeleteItem(cartId, productId);
            return Ok("Deleted successfully!");
        }
    }
}
