using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PapaBobMegaChallengeMVC.Models
{
    public class Order
    {
        [Required]
        public System.Guid order_id { get; set; }

        [Display(Name ="Size")]
        public Size size { get; set; }

        [Display(Name = "Crust")]
        public Crust crust { get; set; }

        public decimal cost { get; set; }

        [Display(Name = "Payment Type")]
        public PaymentType payment_type { get; set; }

        [Required]
        public System.Guid customer_id { get; set; }

        public bool completed { get; set; }
        public Customer Customer { get; set; }

        [Display(Name = "Sausages")]
        public bool sausage { get; set; }

        [Display(Name = "Pepperoni")]
        public bool pepperoni { get; set; }

        [Display(Name = "Onions")]
        public bool onions { get; set; }

        [Display(Name = "Green Peppers")]
        public bool green_peppers { get; set; }

    }
}