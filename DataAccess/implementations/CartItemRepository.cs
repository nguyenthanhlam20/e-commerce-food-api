using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Repository.implementations
{
    public class CartItemRepository
    {
        private static List<CartItem> cartItems = new List<CartItem>();

        public void AddToCart(CartItem cartItem)
        {
            try
            {
                using (var context = new DBContext())
                {
                    context.CartItems.Add(cartItem);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateCartItem(CartItem cartItem)
        {
            try
            {
                using (var context = new DBContext())
                {
                    context.Entry<CartItem>(cartItem).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<CartItem> FindAllCartItemByCartId(int cartId)
        {
            try
            {
                using (var context = new DBContext())
                {
                    cartItems = context.CartItems.Include(c => c.Products).Where(c => c.CartId == cartId).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return cartItems;
        }

        public CartItem FindByProductIdAndUserId(int productId, int userId)
        {
            CartItem cartItem = null;
            try
            {
                using (var context = new DBContext())
                {
                    cartItem = context.CartItems.Include(c => c.Products).Include(c => c.Cart).SingleOrDefault(c => c.ProductId == productId && c.Cart.UserId == userId);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return cartItem;
        }

        public void CreateCartItem(CartItem cartItem)
        {
            try
            {
                using (var context = new DBContext())
                {
                    context.CartItems.Add(cartItem);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteItem(int cartId, int productId)
        {
            CartItem cartItem = new CartItem();
            try
            {
                using (var context = new DBContext())
                {
                    cartItem = context.CartItems.SingleOrDefault(c => c.CartId == cartId && c.ProductId == productId);

                    context.CartItems.Remove(cartItem);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
