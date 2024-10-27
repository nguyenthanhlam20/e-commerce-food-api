using BusinessObject;

namespace Service
{
    public interface CartService
    {
        public Cart FindAllCartByUserId(int userId);
        public void CreateCart(Cart cart);
        public void UpdateCart(Cart cart);
        public void DeleteCart(int cartId);
    }
}
