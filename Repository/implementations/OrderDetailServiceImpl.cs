using BusinessObject;
using Repository.implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.implementations
{
    public class OrderDetailServiceImpl : OrderDetailService
    {
        private static OrderDetailRepository orderDetailRepository = new OrderDetailRepository();
        public void CreateOrderDetail(OrderDetail orderDetail)
        {
            orderDetailRepository.CreateOrderDetail(orderDetail);
        }
    }
}
