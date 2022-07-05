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

        public Statement AddStatement(Statement data)
        {
            try
            {
                var statement = new Statement()
                {
                    Title = data.Title,
                    Description = data.Description,
                    Price = data.Price,
                    CurrencyId = data.CurrencyId,
                };
                _dbContext.Statement.Add(statement);
                _dbContext.SaveChanges();

                return statement;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<List<Statement>> GetAllStatements(int count, int fromIndex)
        {
            if(count == 0)
            {
                count = 10;
            }

            return await _dbContext.Statement.Skip(fromIndex).Take(count).ToListAsync();
        }

        public Statement GetStatementById(int id)
        {
            return  _dbContext.Statement.FirstOrDefault(o => o.Id == id);
        }
    }
}
