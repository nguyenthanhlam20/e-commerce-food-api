using BusinessObject;
using Repository.implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface CartItemService
    {
        public void AddToCart(CartItem cartItem);
        public void UpdateCartItem(CartItem cartItem);
        public List<CartItem> GetAllByCartId(int cartId);
        public CartItem FindByProductIdAndUserId(int productId, int userId);
        void CreateCartItem(CartItem cartItem);
        void DeleteItem(int cartId, int productId);
    }
}
