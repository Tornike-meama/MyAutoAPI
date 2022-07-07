using MyAutoAPI1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyAutoAPI1.Services
{
    public interface IStatementServices
    {
        public Task<Statement> GetStatementById(int id);
        public Task<List<Statement>> GetAllStatements(int count, int fromIndex);
        public Task<Statement> AddStatement(Statement data);
        public Task<Statement> UpdateStatement(Statement data);
    }
}
