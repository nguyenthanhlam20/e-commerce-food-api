using BusinessObject;
using Repository.implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.implementations
{
    public class OrderServiceImpl : OrderService
    {
        private static OrderRepository orderRepository = new OrderRepository();
        public void CreateOrder(Order order)
        {
            orderRepository.CreateOrder(order);
        }

        public List<Order> FindAllByUserId(int userId)
        {
            return orderRepository.FindAllByUserId(userId);
        }
    }
}
