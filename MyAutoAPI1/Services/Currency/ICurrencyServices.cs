using MyAutoAPI1.Models;
using MyAutoAPI1.Models.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyAutoAPI1.Services.Currency
{
    public interface ICurrencyServices
    {
        public Task<IComonResponse<List<Models.Currency>>> GetAllCurrency();
        public Task<IComonResponse<Models.Currency>> GetCurrencyById(int id);
        public Task<IComonResponse<Models.Currency>> AddCurrency(Models.Currency data);
    }
}
