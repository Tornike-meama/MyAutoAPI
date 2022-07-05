using MyAutoAPI1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyAutoAPI1.Services
{
    public interface IStatementServices
    {
        public Statement GetStatementById(int id);
        public Task<List<Statement>> GetAllStatements(int count, int fromIndex);
        public Statement AddStatement(Statement data);
    }
}
