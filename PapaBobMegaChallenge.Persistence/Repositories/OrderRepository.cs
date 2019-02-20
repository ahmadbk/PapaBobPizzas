using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaBobMegaChallenge.Persistence.Repositories
{
    public class OrderRepository : Domain.Interfaces.IOrderRepository
    {
        public void AddOrder(DTO.OrderDto currentOrder, DTO.CustomerDto currentCustomer)
        {
            var db = new PapaBobEntities();
            var dbOrders = db.Orders;
            var dbCustomers = db.Customers;

            var newOrder = new Persistence.Order();
            CreateNewOrder(currentOrder, out newOrder);

            var dbCustomersList = db.Customers.ToList();
            var check_if_customer_exits = CustomerRepository.CustomerExists(dbCustomersList, currentCustomer);

            if (!check_if_customer_exits)
            {
                var newCustomer = new Customer();
                newCustomer = CustomerRepository.CreateNewCustomer(currentCustomer);
                newOrder.customer_id = newCustomer.customer_id;
                var tempAmountOwing = currentOrder.payment_type != DTO.Enums.PaymentType.Cash ? currentOrder.cost : 0;
                newCustomer.amount_owing = double.Parse(tempAmountOwing.ToString());
                newOrder.Customer = newCustomer;
                dbCustomers.Add(newCustomer);
            }
            else
            {
                var existingCustomer = dbCustomersList?.Find(p => p.phone_number == currentCustomer.phone_number);
                newOrder.customer_id = existingCustomer.customer_id;
                var tempAmountOwing = currentOrder.payment_type != DTO.Enums.PaymentType.Cash ? currentOrder.cost : 0;
                existingCustomer.amount_owing += double.Parse(tempAmountOwing.ToString());
            }

            dbOrders.Add(newOrder);
            db.SaveChanges();
        }

        private static void CreateNewOrder(DTO.OrderDto current_order, out Persistence.Order newOrder)
        {
            newOrder = new Persistence.Order();
            OrderMapper(current_order, out newOrder);
            var order_id = Guid.NewGuid();
            newOrder.order_id = order_id;
        }

        private static void OrderMapper(Persistence.Order currentOrder, out DTO.OrderDto newOrder)
        {
            newOrder = new DTO.OrderDto
            {
                completed = false,
                cost = currentOrder.cost,
                size = (DTO.Enums.Size)currentOrder.size,
                crust = (DTO.Enums.Crust)currentOrder.crust,
                payment_type = (DTO.Enums.PaymentType)currentOrder.payment_type,
                onions = currentOrder.onions,
                green_peppers = currentOrder.green_peppers,
                sausage = currentOrder.sausage,
                pepperoni = currentOrder.pepperoni,
                customer_id = currentOrder.customer_id,
                order_id = currentOrder.order_id
            };

            var customer = new DTO.CustomerDto();
            CustomerRepository.CustomerMapper(currentOrder.Customer, out customer);
            newOrder.Customer = customer;
        }

        private static void OrderMapper(DTO.OrderDto currentOrder, out Persistence.Order newOrder)
        {
            newOrder = new Persistence.Order
            {
                completed = false,
                cost = currentOrder.cost,
                crust = (Persistence.Crust)currentOrder.crust,
                payment_type = (Persistence.PaymentType)currentOrder.payment_type,
                size = (Persistence.Size)currentOrder.size,
                onions = currentOrder.onions,
                green_peppers = currentOrder.green_peppers,
                sausage = currentOrder.sausage,
                pepperoni = currentOrder.pepperoni
            };
        }

        public List<DTO.OrderDto> GetOrdersList()
        {
            var db = new PapaBobEntities();
            var dbOrders = db.Orders.ToList();
            var dto_orders_list = new List<DTO.OrderDto>();

            foreach (var order in dbOrders)
            {
                if (!order.completed)
                {
                    var newOrder = new DTO.OrderDto();
                    OrderMapper(order, out newOrder);
                    dto_orders_list.Add(newOrder);
                }
            }
            return dto_orders_list;
        }
        public void UpdateOrder(string currentOrderId)
        {
            var db = new PapaBobEntities();
            var dbOrders = db.Orders.ToList();
            var existingOrder = dbOrders?.Find(p => p.order_id == new Guid(currentOrderId));
            existingOrder.completed = true;
            db.SaveChanges();
        }
    }
}
