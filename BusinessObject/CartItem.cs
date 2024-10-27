using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class CartItem
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? UnitPrice { get; set; }
        public double? TotalPrice { get; set; }
        public DateTime AddedAt { get; set; } = DateTime.Now;
        [JsonIgnore]
        public virtual Cart? Cart { get; set; }
        public virtual Product? Products { get; set; }

        public CartItem() { }

        public CartItem(int cartId, int productId, int quantity, decimal? unitPrice, double? totalPrice)
        {
            CartId = cartId;
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
            TotalPrice = totalPrice;
        }
    }
}
