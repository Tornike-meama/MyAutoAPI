using Microsoft.EntityFrameworkCore;
using MyAutoAPI1.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyAutoAPI1.Services.Currency
{
    public class CurrencyServices : ICurrencyServices
    {
        private readonly MyDbContext _dbContext;

        public CurrencyServices(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Models.Currency>> GetAllCurrency()
        {
            return await _dbContext.Currencies.ToListAsync();
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
