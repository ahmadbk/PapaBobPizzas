using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaBobMegaChallenge.Persistence.Repositories
{
    public class CustomerRepository : Domain.Interfaces.ICustomerRepository
    {
        public List<DTO.CustomerDto> GetCustomerList()
        {
            var db = new PapaBobEntities();
            var dbCustomers = db.Customers.ToList();
            var dto_customers_list = new List<DTO.CustomerDto>();

            foreach (var customer in dbCustomers)
            {
                var new_customer = new DTO.CustomerDto();
                CustomerMapper(customer, out new_customer);
                dto_customers_list.Add(new_customer);
            }
            return dto_customers_list;
        }

        public static void CustomerMapper(Persistence.Customer current_customer, out DTO.CustomerDto new_customer)
        {
            new_customer = new DTO.CustomerDto
            {
                name = current_customer.name,
                address = current_customer.address,
                zip_code = current_customer.zip_code,
                phone_number = current_customer.phone_number,
                customer_id = current_customer.customer_id,
                amount_owing = current_customer.amount_owing
            };
        }

        public static Persistence.Customer CreateNewCustomer(DTO.CustomerDto current_customer)
        {
            var new_customer = new Customer();
            CustomerMapper(current_customer, out new_customer);
            var customer_id = Guid.NewGuid();
            new_customer.customer_id = customer_id;
            return new_customer;
        }

        public static void CustomerMapper(DTO.CustomerDto current_customer, out Persistence.Customer new_customer)
        {
            new_customer = new Persistence.Customer
            {
                name = current_customer.name,
                address = current_customer.address,
                zip_code = current_customer.zip_code,
                phone_number = current_customer.phone_number
            };
        }

        public static bool CustomerExists(List<Persistence.Customer> dbCustomersList, DTO.CustomerDto current_customer)
        {
            var count = dbCustomersList?.Where(p => p.phone_number == current_customer.phone_number).Count();

            if (count > 0)
                return true;
            else
                return false;
        }

    }
}
