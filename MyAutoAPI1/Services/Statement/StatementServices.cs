using Microsoft.EntityFrameworkCore;
using MyAutoAPI1.Models;
using MyAutoAPI1.Services.Currency;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace MyAutoAPI1.Services
{
    public class StatementServices : IStatementServices
    {
        private readonly MyDbContext _dbContext;
        private readonly ICurrencyServices _currencyServices;

        public StatementServices(MyDbContext dbContext, ICurrencyServices currencyServices)
        {
            _dbContext = dbContext;
            _currencyServices = currencyServices;
        }


        public async Task<ComonResponse<List<Statement>>> GetAllStatements(int count, int fromIndex)
        {
            try
            {
                if (count == 0)
                {
                    count = 10;
                }

                var res = await _dbContext.Statement.Skip(fromIndex).Take(count).ToListAsync();

                return new ComonResponse<List<Statement>>(res);
            }
            catch (Exception ex)
            {
                return new ComonResponse<List<Statement>>(ex.Message);
            }
        }

        public async Task<ComonResponse<Statement>>GetStatementById(int id)
        {
            try
            {
                var res = await _dbContext.Statement.FirstOrDefaultAsync(o => o.Id == id);
                return new ComonResponse<Statement>(res);
            }
            catch (Exception ex)
            {
                return new ComonResponse<Statement>(ex.Message);
            }
        }
        public async Task<ComonResponse<List<Statement>>>GetStatementByUserId(string userId)
        {
            try
            {
                if (string.IsNullOrEmpty(userId))
                {
                    return new ComonResponse<List<Statement>>("Cann't provide userId");
                }

                var res = await _dbContext.Statement.Where(o => o.Creator.CompareTo(userId) == 0).ToListAsync();

                return new ComonResponse<List<Statement>>(res);
            }
            catch (Exception ex)
            {
                return new ComonResponse<List<Statement>>(ex.Message);
            }
        }
        public async Task<ComonResponse<Statement>> AddStatement(Statement data, string token)
        {
            try
            {
                var currentCurrency = await _currencyServices.GetCurrencyById(data.CurrencyId);
                if (data.CurrencyId < 1 || currentCurrency.IsError) new ComonResponse<Statement>("Can't find Currency");

                var tokenArr = token.Split(" ");
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(tokenArr[1]);
                var tokenData = jsonToken as JwtSecurityToken;

                var statement = new Statement()
                {
                    Title = data.Title,
                    Description = data.Description,
                    Price = data.Price,
                    CurrencyId = data.CurrencyId,
                    Creator = data.Creator
                };

                _dbContext.Statement.Add(statement);
                await _dbContext.SaveChangesAsync();

                return new ComonResponse<Statement>(data); 
            }
            catch (Exception ex)
            {
                return new ComonResponse<Statement>(ex.Message);
            }
        }

        public async Task<ComonResponse<Statement>> UpdateStatement(Statement data)
        {
            try
            {
                var currentCurrency = await _currencyServices.GetCurrencyById(data.CurrencyId);
                if (data.CurrencyId < 1 || currentCurrency.IsError)
                {
                    return new ComonResponse<Statement>("Currency not found");
                }
                var statement = _dbContext.Statement.FirstOrDefault(o => o.Id == data.Id);
                if(statement == null)
                {
                    return new ComonResponse<Statement>("Statement not found invalid ID");
                }
                statement.Title = data.Title;
                statement.Description = data.Description;
                statement.Price = data.Price;
                statement.CurrencyId = data.CurrencyId;

                await _dbContext.SaveChangesAsync();
                return new ComonResponse<Statement>(data); ;
            }
            catch (Exception ex)
            {
                return new ComonResponse<Statement>(ex.Message);
            }
        }

       
    }
}
