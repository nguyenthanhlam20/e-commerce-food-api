using BusinessObject;
using ProjectPRN231_API.DTO;

namespace ProjectPRN231_MVC.Models
{
    public class CheckoutViewModel
    {
        public Cart cart { get; set; }
        public CheckoutRequest? request { get; set; }
    }
}
