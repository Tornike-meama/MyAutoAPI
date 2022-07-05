using MyAutoAPI1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyAutoAPI1.Services.Currency
{
    public interface ICurrencyServices
    {
        public Task<List<Models.Currency>> GetAllCurrency();
        public Models.Currency AddCurrency(Models.Currency data);
    }
}
