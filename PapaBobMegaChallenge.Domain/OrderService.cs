using PapaBobMegaChallenge.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaBobMegaChallenge.Domain
{
    public class OrderService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IPizzaPriceTableRepository _pizzaPriceTableRepository;
        private readonly IOrderRepository _orderRepository;

        public OrderService(ICustomerRepository customerRepository, IPizzaPriceTableRepository pizzaPriceTableRepository, IOrderRepository orderRepository)
        {
            _customerRepository = customerRepository;
            _pizzaPriceTableRepository = pizzaPriceTableRepository;
            _orderRepository = orderRepository;
        }

        //calculate total cost given DTO order object return a double
        public double CalculateAmountOwing(DTO.OrderDto current_order)
        {
            var latestPricing = _pizzaPriceTableRepository.GetPizzaPrices();
            decimal cost = 0;

            switch (current_order.size)
            {
                case DTO.Enums.Size.Small:
                    cost += latestPricing.SmallSizeCost;
                    break;
                case DTO.Enums.Size.Medium:
                    cost += latestPricing.MediumSizeCost;
                    break;
                case DTO.Enums.Size.Large:
                    cost += latestPricing.LargeSizeCost;
                    break;
            }

            switch (current_order.crust)
            {
                case DTO.Enums.Crust.Thin:
                    cost += latestPricing.ThinCrustCost;
                    break;
                case DTO.Enums.Crust.Thick:
                    cost += latestPricing.ThickCrustCost;
                    break;
            }

            cost += current_order.green_peppers ? latestPricing.GreenPeppersCost : 0;
            cost += current_order.onions ? latestPricing.OnionsCost : 0;
            cost += current_order.pepperoni ? latestPricing.PepperoniCost : 0;
            cost += current_order.sausage ? latestPricing.SausageCost : 0;

            return double.Parse(cost.ToString());
        }

        //receive order and customer object from presentation and pass onto persistence
        public void AddOrder(DTO.OrderDto current_order)
        {
            _orderRepository.AddOrder(current_order, current_order.Customer);
        }

        public List<DTO.OrderDto> ObtainOrdersList()
        {
            return _orderRepository.GetOrdersList();
        }

        public List<DTO.CustomerDto> ObtainCustomersList()
        {
            return _customerRepository.GetCustomerList();
        }

        public void ChangeOrderStatus(string current_order_id)
        {
            _orderRepository.UpdateOrder(current_order_id);
        }

        public double GetTotalAccountBalance(string phoneNumber)
        {
            return _customerRepository.CustomerBalanceOwing(phoneNumber);
        }
    }
}
