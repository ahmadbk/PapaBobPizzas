using System;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PapaBobMegaChallenge.Domain.Interfaces;

namespace PapaBobMegaChallenge.Domain.Test
{
    [TestClass]
    public class OrderServiceTest
    {
        private IOrderRepository _orderRepository;
        private ICustomerRepository _customerRepository;
        private IPizzaPriceTableRepository _pizzaPriceTableRepository;

        private OrderService _orderManager;

        [TestInitialize]
        public void Initialize()
        {
            _orderRepository = A.Fake<IOrderRepository>();
            _customerRepository = A.Fake<ICustomerRepository>();
            _pizzaPriceTableRepository = A.Fake<IPizzaPriceTableRepository>();
            _orderManager = new OrderService(_customerRepository, _pizzaPriceTableRepository, _orderRepository);
            
        }

        [TestMethod]
        public void ObtainOrdersList_ReturnsOrdersList()
        {

        }
    }
}
