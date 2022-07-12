using MyAutoAPI1.Models;
using MyAutoAPI1.Models.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyAutoAPI1.Services
{
    public interface IStatementServices
    {
        public Task<IComonResponse<List<Statement>>> GetAllStatements(int count, int fromIndex);
        public Task<IComonResponse<Statement>> GetStatementById(int id);
        public Task<IComonResponse<List<Statement>>> GetStatementByUserId(string userId);
        public Task<IComonResponse<Statement>> AddStatement(Statement data, string token);
        public Task<IComonResponse<Statement>> UpdateStatement(Statement data);
    }
}
