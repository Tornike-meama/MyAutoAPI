using MyAutoAPI1.Models;
using System.Collections.Generic;

namespace MyAutoAPI1.Services.Currency
{
    public interface ICurrencyServices
    {
        public List<Models.Currency> GetAllCurrency();
        public Models.Currency AddCurrency(Models.Currency data);
    }
}
