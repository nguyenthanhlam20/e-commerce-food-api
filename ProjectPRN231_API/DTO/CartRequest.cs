namespace ProjectPRN231_API.DTO
{
    public class CartRequest
    {
        public string userId {  get; set; }
        public string productId { get; set; }
        public string quantity { get; set; }
        public string unitPrice { get; set; }

        public CartRequest()
        {

        }

        public CartRequest(string userId, string productId, string quantity, string unitPrice)
        {
            this.userId = userId;
            this.productId = productId;
            this.quantity = quantity;
            this.unitPrice = unitPrice;
        }
    }
}
