using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface OrderService
    {
        public void CreateOrder(Order order);
        List<Order> FindAllByUserId(int userId);
    }
}
