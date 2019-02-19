using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaBobMegaChallenge.Domain.Interfaces
{
    public interface IOrderRepository
    {
        void AddOrder(DTO.OrderDto current_order, DTO.CustomerDto current_customer);
        List<DTO.OrderDto> GetOrdersList();
        void UpdateOrder(string current_order_id);
    }
}
