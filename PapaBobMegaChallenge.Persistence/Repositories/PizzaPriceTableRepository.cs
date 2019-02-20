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
            var db = new PapaBobEntities();
            var dbPrices = db.PizzaPriceTables.ToList();
            dbPrices = dbPrices.OrderBy(p => p.Date).ToList();
            var latestPrice = dbPrices[dbPrices.Count - 1];

            return PriceMapper(latestPrice);
        }

        public DTO.PizzaPriceTableDto PriceMapper(Persistence.PizzaPriceTable current_prices)
        {
            var prices_current = new DTO.PizzaPriceTableDto
            {
                Id = current_prices.Id,
                Date = current_prices.Date,
                SmallSizeCost = current_prices.SmallSizeCost,
                MediumSizeCost = current_prices.MediumSizeCost,
                LargeSizeCost = current_prices.LargeSizeCost,
                ThickCrustCost = current_prices.ThickCrustCost,
                ThinCrustCost = current_prices.ThinCrustCost,
                SausageCost = current_prices.SausageCost,
                GreenPeppersCost = current_prices.GreenPeppersCost,
                OnionsCost = current_prices.OnionsCost,
                PepperoniCost = current_prices.PepperoniCost
            };

            return prices_current;
        }

    }
}
