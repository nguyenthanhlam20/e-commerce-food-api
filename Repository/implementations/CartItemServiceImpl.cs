using BusinessObject;
using Repository.implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.implementations
{
    public class CartItemServiceImpl : CartItemService
    {
        private static CartItemRepository cartItemRepository = new CartItemRepository();
        public void AddToCart(CartItem cartItem)
        {
            cartItemRepository.AddToCart(cartItem);
        }

        public void CreateCartItem(CartItem cartItem)
        {
            cartItemRepository.CreateCartItem(cartItem);
        }

        public void DeleteItem(int cartId, int productId)
        {
            cartItemRepository.DeleteItem(cartId, productId);
        }

        public CartItem FindByProductIdAndUserId(int productId, int userId)
        {
            return cartItemRepository.FindByProductIdAndUserId(productId, userId);
        }

        public List<CartItem> GetAllByCartId(int cartId)
        {
            return cartItemRepository.FindAllCartItemByCartId(cartId);
        }

        public void UpdateCartItem(CartItem cartItem)
        {
            cartItemRepository.UpdateCartItem(cartItem);
        }
    }
}
