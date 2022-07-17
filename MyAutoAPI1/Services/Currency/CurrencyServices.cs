using Microsoft.EntityFrameworkCore;
using MyAutoAPI1.Models;
using MyAutoAPI1.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using MyAutoAPI1.Controllers.GetBody.Currency;
using MyAutoAPI1.Validators;

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

        public async Task<IComonResponse<AddCurrencyModel>>AddCurrencyAsync(AddCurrencyModel data)
        {
            try
            {
                if (data == null) return new BadRequest<AddCurrencyModel>("Data is null");

                var currency = new Models.Currency()
                {
                    Name = data.Name,
                    ShortName = data.ShortName,
                    Symbol = data.Symbol,
                };

                AddCurrencyValidator validaor = new AddCurrencyValidator();
                ValidationResult validationResult = validaor.Validate(data);

                if (!validationResult.IsValid) return new BadRequest<AddCurrencyModel>(string.Join(", ", validationResult.Errors.Select(o => o.ErrorMessage)));

                _dbContext.Currencies.Add(currency);
                await _dbContext.SaveChangesAsync();
                return new ComonResponse<AddCurrencyModel>(data);
            }
            catch (Exception ex)
            {
                return new BadRequest<AddCurrencyModel>(ex.Message);
            }
        }

        public async Task<IComonResponse<UpdateCurrencyModel>>UpdateCurrencyAsync(UpdateCurrencyModel data)
        {
            try
            {
                if (data == null) return new BadRequest<UpdateCurrencyModel>("Data is null");

                var currency = _dbContext.Currencies.FirstOrDefault(o => o.Id == data.Id);

                if (currency == null)
                {
                    return new NotFound<UpdateCurrencyModel>("Currency not found");
                }

                UpdateCurrencyvalidator validaor = new UpdateCurrencyvalidator();
                ValidationResult validationResult = validaor.Validate(data);

                if (!validationResult.IsValid)
                {
                    return new BadRequest<UpdateCurrencyModel>(string.Join(", ", validationResult.Errors.Select(o => o.ErrorMessage)));
                }

                currency.Name = data.Name;
                currency.ShortName = data.ShortName;
                currency.Symbol = data.Symbol;

                await _dbContext.SaveChangesAsync();
                return new ComonResponse<UpdateCurrencyModel>(data);
            }
            catch (Exception ex)
            {
                return new BadRequest<UpdateCurrencyModel>(ex.Message);
            }
        }

        //public async Task<IComonResponse<Models.Currency>> DeleteCurrencyAsync(int id)
        //{
        //    try
        //    {
        //        var currency = _dbContext.Currencies.FirstOrDefault(o => o.Id == Id);

        //        if (currency == null)
        //        {
        //            return new NotFound<Models.Currency>("Currency not found");
        //        }

                

        //    }
        //    catch (Exception ex)
        //    {
        //        return new BadRequest<Models.Currency>(ex.Message);
        //    }
        //}
    }
}
