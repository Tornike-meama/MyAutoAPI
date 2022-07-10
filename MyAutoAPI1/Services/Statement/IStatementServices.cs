using MyAutoAPI1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyAutoAPI1.Services
{
    public interface IStatementServices
    {
        public Task<ComonResponse<List<Statement>>> GetAllStatements(int count, int fromIndex);
        public Task<ComonResponse<Statement>> GetStatementById(int id);
        public Task<ComonResponse<List<Statement>>> GetStatementByUserId(string userId);
        public Task<ComonResponse<Statement>> AddStatement(Statement data, string token);
        public Task<ComonResponse<Statement>> UpdateStatement(Statement data);
    }
}
