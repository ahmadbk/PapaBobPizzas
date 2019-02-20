using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaBobMegaChallenge.Persistence.Repositories
{
    public class OrderRepository : Domain.Interfaces.IOrderRepository
    {
        public void AddOrder(DTO.OrderDto current_order, DTO.CustomerDto current_customer)
        {
            var db = new PapaBobEntities();
            var dbOrders = db.Orders;
            var dbCustomers = db.Customers;

            var new_order = new Persistence.Order();
            CreateNewOrder(current_order, out new_order);

            var dbCustomersList = db.Customers.ToList();
            var check_if_customer_exits = CustomerRepository.CustomerExists(dbCustomersList, current_customer);

            if (!check_if_customer_exits)
            {
                var new_customer = new Customer();
                new_customer = CustomerRepository.CreateNewCustomer(current_customer);
                new_order.customer_id = new_customer.customer_id;
                var tempAmountOwing = current_order.payment_type != DTO.Enums.PaymentType.Cash ? current_order.cost : 0;
                new_customer.amount_owing = double.Parse(tempAmountOwing.ToString());
                new_order.Customer = new_customer;
                dbCustomers.Add(new_customer);
            }
            else
            {
                var existing_customer = dbCustomersList?.Find(p => p.phone_number == current_customer.phone_number);
                new_order.customer_id = existing_customer.customer_id;
                var tempAmountOwing = current_order.payment_type != DTO.Enums.PaymentType.Cash ? current_order.cost : 0;
                existing_customer.amount_owing += double.Parse(tempAmountOwing.ToString());
            }

            dbOrders.Add(new_order);
            db.SaveChanges();
        }

        private static void CreateNewOrder(DTO.OrderDto current_order, out Persistence.Order new_order)
        {
            new_order = new Persistence.Order();
            OrderMapper(current_order, out new_order);
            var order_id = Guid.NewGuid();
            new_order.order_id = order_id;
        }

        private static void OrderMapper(Persistence.Order current_order, out DTO.OrderDto new_order)
        {
            new_order = new DTO.OrderDto
            {
                completed = false,
                cost = current_order.cost,
                size = (DTO.Enums.Size)current_order.size,
                crust = (DTO.Enums.Crust)current_order.crust,
                payment_type = (DTO.Enums.PaymentType)current_order.payment_type,
                onions = current_order.onions,
                green_peppers = current_order.green_peppers,
                sausage = current_order.sausage,
                pepperoni = current_order.pepperoni,
                customer_id = current_order.customer_id,
                order_id = current_order.order_id
            };

            DTO.CustomerDto customer = new DTO.CustomerDto();
            CustomerRepository.CustomerMapper(current_order.Customer, out customer);
            new_order.Customer = customer;
        }

        private static void OrderMapper(DTO.OrderDto current_order, out Persistence.Order new_order)
        {
            new_order = new Persistence.Order
            {
                completed = false,
                cost = current_order.cost,
                crust = (Persistence.Crust)current_order.crust,
                payment_type = (Persistence.PaymentType)current_order.payment_type,
                size = (Persistence.Size)current_order.size,
                onions = current_order.onions,
                green_peppers = current_order.green_peppers,
                sausage = current_order.sausage,
                pepperoni = current_order.pepperoni
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
                    var new_order = new DTO.OrderDto();
                    OrderMapper(order, out new_order);
                    dto_orders_list.Add(new_order);
                }
            }
            return dto_orders_list;
        }
        public void UpdateOrder(string current_order_id)
        {
            var db = new PapaBobEntities();
            var dbOrders = db.Orders.ToList();
            var existing_order = dbOrders?.Find(p => p.order_id == new Guid(current_order_id));
            existing_order.completed = true;
            db.SaveChanges();
        }
    }
}
