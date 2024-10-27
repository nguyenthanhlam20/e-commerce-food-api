using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class Order
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }
        [ForeignKey("User")]
        public int? CustomerID { get; set; }
        [ForeignKey("User")]
        public int? ApproverId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public DateTime? RequireDate { get; set; }
        public DateTime? ShipDate { get; set; }
        public string? ShipAddress { get; set; }
        public double? ShipPrice { get; set; }
        public bool? Status { get; set; }
        public virtual User? Customer { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public Order()
        {

        }
        public Order(int? customerID, DateTime orderDate, DateTime? requireDate, string? shipAddress, double? shipPrice, bool? status)
        {
            CustomerID = customerID;
            OrderDate = orderDate;
            RequireDate = requireDate;
            ShipAddress = shipAddress;
            ShipPrice = shipPrice;
            Status = status;
        }
    }
}
