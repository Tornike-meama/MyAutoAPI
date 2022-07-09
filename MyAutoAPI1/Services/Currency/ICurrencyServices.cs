using MyAutoAPI1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyAutoAPI1.Services.Currency
{
    public interface ICurrencyServices
    {
        public Task<ComonResponse<List<Models.Currency>>> GetAllCurrency();
        public Task<ComonResponse<Models.Currency>> GetCurrencyById(int id);
        public Task<ComonResponse<Models.Currency>> AddCurrency(Models.Currency data);
    }
}
