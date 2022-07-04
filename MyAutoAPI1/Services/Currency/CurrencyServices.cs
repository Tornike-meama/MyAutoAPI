using MyAutoAPI1.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyAutoAPI1.Services.Currency
{
    public class CurrencyServices : ICurrencyServices
    {
        private readonly MyDbContext _dbContext;

        public CurrencyServices(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<Models.Currency> GetAllCurrency()
        {
            return _dbContext.Currencies.ToList();
        }

        public Models.Currency AddCurrency(Models.Currency data)
        {
            if(data == null) return null;

            var currency = new Models.Currency()
            {
                Name = data.Name,
                ShortName = data.ShortName,
                Symbol = data.Symbol,
            };
            _dbContext.Currencies.Add(currency);
            _dbContext.SaveChanges();
            return currency;
        }

    }
}
