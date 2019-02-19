using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PapaBobMegaChallengeMVC.Models
{
    public class Customer
    {
        [Required]
        public System.Guid customer_id { get; set; }

        [Display(Name="Name")]
        public string name { get; set; }

        [Display(Name = "Address")]
        public string address { get; set; }

        [Display(Name = "Zip Code")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "Entered Zip Code is not valid.")]
        public string zip_code { get; set; }

        [Display(Name ="Phone Number")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        public string phone_number { get; set; }

        public double amount_owing { get; set; }
    }
}