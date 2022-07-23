using MyAutoAPI1.Controllers.GetBody.Statement;
using MyAutoAPI1.Models;
using MyAutoAPI1.Models.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyAutoAPI1.Services
{
    public interface IStatementServices
    {
        public Task<IComonResponse<List<Statement>>> GetAllStatementsAsync(int count, int fromIndex);
        public Task<IComonResponse<Statement>> GetStatementByIdAsync(int id);
        public Task<IComonResponse<List<Statement>>> GetStatementByUserIdAsync(string userId);
        public Task<IComonResponse<Statement>> AddStatementAsync(AddStatementModel data, string creatorId);
        public Task<IComonResponse<Statement>> UpdateStatementAsync(UpdateStatement data, string creatorId);
        public Task<IComonResponse<Statement>> DeleteStatementAsync(int id, string userId);
    }
}
