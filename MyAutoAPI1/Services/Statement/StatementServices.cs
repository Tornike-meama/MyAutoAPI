using Microsoft.EntityFrameworkCore;
using MyAutoAPI1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyAutoAPI1.Services
{
    public class StatementServices : IStatementServices
    {
        private readonly MyDbContext _dbContext;

        public StatementServices(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Statement> AddStatement(Statement data)
        {
            try
            {
                var isCurrency = _dbContext.Currencies.Any(o => o.Id == data.CurrencyId);
                if(data.CurrencyId < 1 || !isCurrency) return null;
                var statement = new Statement()
                {
                    Title = data.Title,
                    Description = data.Description,
                    Price = data.Price,
                    CurrencyId = data.CurrencyId,
                };
                _dbContext.Statement.Add(statement);
                await _dbContext.SaveChangesAsync();
                return statement; 
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Statement> UpdateStatement(Statement data)
        {
            try
            {
                var isCurrency = _dbContext.Currencies.Any(o => o.Id == data.CurrencyId);
                if (data.CurrencyId < 1 || !isCurrency)
                {
                    return null;
                }
                var statement = _dbContext.Statement.FirstOrDefault(o => o.Id == data.Id);
                if(statement == null)
                {
                    return null;
                }
                statement.Title = data.Title;
                statement.Description = data.Description;
                statement.Price = data.Price;
                statement.CurrencyId = data.CurrencyId;

                await _dbContext.SaveChangesAsync();
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<Statement>> GetAllStatements(int count, int fromIndex)
        {
            try
            {
                if (count == 0)
                {
                    count = 10;
                }

                return await _dbContext.Statement.Skip(fromIndex).Take(count).ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Statement> GetStatementById(int id)
        {
            try
            {
                return await _dbContext.Statement.FirstOrDefaultAsync(o => o.Id == id);
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}
