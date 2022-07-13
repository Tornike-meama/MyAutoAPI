using MyAutoAPI1.Models;
using MyAutoAPI1.Models.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyAutoAPI1.Services.Currency
{
    public interface ICurrencyServices
    {
        public Task<IComonResponse<List<Models.Currency>>> GetAllCurrencyAsync();
        public Task<IComonResponse<Models.Currency>> GetCurrencyByIdAsync(int id);
        public Task<IComonResponse<Models.Currency>> AddCurrencyAsync(Models.Currency data);
    }
}
