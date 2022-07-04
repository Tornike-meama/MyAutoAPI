using MyAutoAPI1.Models;
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
            var statement = new Statement()
            {
                Title = data.Title,
                Description = data.Description,
                Price = data.Price,
            };
            _dbContext.Add(statement);
            _dbContext.SaveChanges();
            return statement;
        }

        public List<Statement> GetAllStatements()
        {
            return _dbContext.Statement.ToList();
        }

        public Statement GetStatementById(int id)
        {
            return _dbContext.Statement.FirstOrDefault(o => o.Id == id);
        }
    }
}
