using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaBobMegaChallenge.Persistence.Repositories
{
    public class PizzaPriceTableRepository : Domain.Interfaces.IPizzaPriceTableRepository
    {
        public DTO.PizzaPriceTableDto GetPizzaPrices()
        {
            PapaBobEntities db = new PapaBobEntities();
            var dbPrices = db.PizzaPriceTables.ToList();
            dbPrices = dbPrices.OrderBy(p => p.Date).ToList();
            var latestPrice = dbPrices[dbPrices.Count - 1];

            return PriceMapper(latestPrice);
        }

        public DTO.PizzaPriceTableDto PriceMapper(Persistence.PizzaPriceTable current_prices)
        {
            DTO.PizzaPriceTableDto prices_current = new DTO.PizzaPriceTableDto();

            prices_current.Id = current_prices.Id;
            prices_current.Date = current_prices.Date;
            prices_current.SmallSizeCost = current_prices.SmallSizeCost;
            prices_current.MediumSizeCost = current_prices.MediumSizeCost;
            prices_current.LargeSizeCost = current_prices.LargeSizeCost;
            prices_current.ThickCrustCost = current_prices.ThickCrustCost;
            prices_current.ThinCrustCost = current_prices.ThinCrustCost;
            prices_current.SausageCost = current_prices.SausageCost;
            prices_current.GreenPeppersCost = current_prices.GreenPeppersCost;
            prices_current.OnionsCost = current_prices.OnionsCost;
            prices_current.PepperoniCost = current_prices.PepperoniCost;

            return prices_current;
        }

    }
}
