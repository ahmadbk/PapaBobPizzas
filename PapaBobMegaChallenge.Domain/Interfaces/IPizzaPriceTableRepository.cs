using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaBobMegaChallenge.Domain.Interfaces
{
    public interface IPizzaPriceTableRepository
    {
        DTO.PizzaPriceTableDto GetPizzaPrices();
    }
}
