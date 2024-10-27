namespace ProjectPRN231_API.DTO
{
    public class CheckoutRequest
    {
        public string userId {  get; set; }
        public List<CartRequest> carts { get; set; }
        public string requireDate { get; set; }
        public string shipAddress { get; set; }
        public string shipPrice { get; set; }
    }
}
