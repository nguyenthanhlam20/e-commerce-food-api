using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.implementations
{
    public class OrderRepository
    {
        private static List<Order> orders = new List<Order>();
        public void CreateOrder(Order order)
        {
            try
            {
                using (var context = new DBContext())
                {
                    context.Orders.Add(order);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Order> FindAllByUserId(int userId)
        {
            try
            {
                using (var context = new DBContext())
                {
                    orders = context.Orders.Include(o => o.OrderDetails)
                        .ThenInclude(od => od.Product)
                        .Where(o => o.CustomerID == userId)
                        .OrderByDescending(o => o.OrderDate)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return orders;
        }
    }
}
