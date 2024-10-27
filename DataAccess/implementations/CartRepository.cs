using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.implementations
{
    public class CartRepository
    {
        private static Cart carts = new Cart();

        public void CreateCart(Cart cart)
        {
            try
            {
                using (var context = new DBContext())
                {
                    context.Carts.Add(cart);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteCart(int cartId)
        {
            Cart cart = null;
            try
            {
                using (var context = new DBContext())
                {
                    cart = context.Carts.SingleOrDefault(c => c.CartId == cartId);
                    context.Carts.Remove(cart);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Cart FindAllCartByUserId(int userId)
        {
            try
            {
                using (var context = new DBContext())
                {
                    carts = context.Carts.Include(c => c.CartItems).ThenInclude(c => c.Products).SingleOrDefault(c => c.UserId == userId);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return carts;
        }

        public void UpdateCart(Cart cart)
        {
            try
            {
                using (var context = new DBContext())
                {
                    context.Entry<Cart>(cart).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
