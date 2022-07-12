using Microsoft.EntityFrameworkCore;
using MyAutoAPI1.Models;
using MyAutoAPI1.Models.Responses;
using System;
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

        public async Task<IComonResponse<List<Models.Currency>>> GetAllCurrency()
        {
            try
            {
                var res = await _dbContext.Currencies.ToListAsync();
                return new ComonResponse<List<Models.Currency>>(res);
            }
            catch (Exception ex)
            {
                return new BadRequest<List<Models.Currency>>(ex.Message);
            }
        }

        public async Task<IComonResponse<Models.Currency>> GetCurrencyById(int id)
        {
            try
            {
                var res = await _dbContext.Currencies.FirstOrDefaultAsync(o => o.Id == id);
                if(res == null)
                {
                    return new NotFound<Models.Currency>("Not Found");
                }
                return new ComonResponse<Models.Currency>(res);
            }
            catch (Exception ex)
            {
                return new BadRequest<Models.Currency>(ex.Message);
            }
        }

        public async Task<IComonResponse<Models.Currency>> AddCurrency(Models.Currency data)
        {
            try
            {
                if (data == null) return null;

                var currency = new Models.Currency()
                {
                    Name = data.Name,
                    ShortName = data.ShortName,
                    Symbol = data.Symbol,
                };
                _dbContext.Currencies.Add(currency);
                await _dbContext.SaveChangesAsync();
                return new ComonResponse<Models.Currency>(currency);
            }
            catch (Exception ex)
            {
                return new BadRequest<Models.Currency>(ex.Message);
            }
        }

    }
}
