using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaBobMegaChallenge.DTO
{
    [Serializable]
    public class CustomerDto
    {
        public System.Guid customer_id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string zip_code { get; set; }
        public string phone_number { get; set; }
        public double amount_owing { get; set; }
    }
}
