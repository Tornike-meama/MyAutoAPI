using Microsoft.EntityFrameworkCore;
using MyAutoAPI1.Models;
using MyAutoAPI1.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace MyAutoAPI1.Services.Currency
{
    public class CurrencyServices : ICurrencyServices
    {
        private readonly MyDbContext _dbContext;

        public CurrencyServices(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IComonResponse<List<Models.Currency>>>GetAllCurrencyAsync()
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

        public async Task<IComonResponse<Models.Currency>>GetCurrencyByIdAsync(int id)
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

        public async Task<IComonResponse<Models.Currency>>AddCurrencyAsync(Models.Currency data)
        {
            try
            {
                if (data == null) return new BadRequest<Models.Currency>("Data is null");

                var currency = new Models.Currency()
                {
                    Name = data.Name,
                    ShortName = data.ShortName,
                    Symbol = data.Symbol,
                };

                CurrencyValidator validaor = new CurrencyValidator();
                ValidationResult validationResult = validaor.Validate(currency);

                if (!validationResult.IsValid) return new BadRequest<Models.Currency>(string.Join(", ", validationResult.Errors.Select(o => o.ErrorMessage)));

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
