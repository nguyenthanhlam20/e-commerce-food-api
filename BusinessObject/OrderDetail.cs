using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class OrderDetail
    {
        public OrderDetail(int? productId, int? quantity, double? totalPrice)
        {
            ProductId = productId;
            Quantity = quantity;
            TotalPrice = totalPrice;
        }

        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public double? TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [JsonIgnore]
        public virtual Order? Order { get; set; }
        public virtual Product? Product { get; set; }
    }
}
