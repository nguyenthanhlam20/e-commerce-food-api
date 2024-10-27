using BusinessObject;
using Repository.implementations;

namespace Service.implementations
{
    public class CartServiceImpl : CartService
    {

        private static CartRepository cartRepository = new CartRepository();

        public void CreateCart(Cart cart)
        {
            cartRepository.CreateCart(cart);
        }

        public void DeleteCart(int cartId)
        {
            cartRepository.DeleteCart(cartId);
        }

        public Cart FindAllCartByUserId(int userId)
        {
            return cartRepository.FindAllCartByUserId(userId);
        }

        public void UpdateCart(Cart cart)
        {
            cartRepository.UpdateCart(cart);
        }
    }
}
