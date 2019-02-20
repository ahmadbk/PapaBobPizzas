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

        public DTO.PizzaPriceTableDto PriceMapper(Persistence.PizzaPriceTable currentPrices)
        {
            var pricesCurrent = new DTO.PizzaPriceTableDto
            {
                Id = currentPrices.Id,
                Date = currentPrices.Date,
                SmallSizeCost = currentPrices.SmallSizeCost,
                MediumSizeCost = currentPrices.MediumSizeCost,
                LargeSizeCost = currentPrices.LargeSizeCost,
                ThickCrustCost = currentPrices.ThickCrustCost,
                ThinCrustCost = currentPrices.ThinCrustCost,
                SausageCost = currentPrices.SausageCost,
                GreenPeppersCost = currentPrices.GreenPeppersCost,
                OnionsCost = currentPrices.OnionsCost,
                PepperoniCost = currentPrices.PepperoniCost
            };

            return pricesCurrent;
        }

    }
}
