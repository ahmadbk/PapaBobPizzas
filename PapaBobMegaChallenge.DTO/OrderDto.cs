using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PapaBobMegaChallenge.DTO.Enums;

namespace PapaBobMegaChallenge.DTO
{
    public class OrderDto
    {
        public System.Guid order_id { get; set; }
        public Size size { get; set; }
        public Crust crust { get; set; }
        public bool sausage { get; set; }
        public decimal cost { get; set; }
        public PaymentType payment_type { get; set; }
        public System.Guid customer_id { get; set; }
        public bool pepperoni { get; set; }
        public bool onions { get; set; }
        public bool green_peppers { get; set; }
        public bool completed { get; set; }

        public virtual CustomerDto Customer { get; set; }

    }
}
