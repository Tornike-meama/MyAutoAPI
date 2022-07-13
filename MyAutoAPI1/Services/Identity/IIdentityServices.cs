using MyAutoAPI1.Controllers.GetQueries;
using MyAutoAPI1.Models;
using MyAutoAPI1.Models.Responses;
using System.Threading.Tasks;

namespace MyAutoAPI1.Services.Identity
{
    public interface IIdentityServices
    {
        public Task<IComonResponse<string>> RegisterAsync(string email, string password, string name);
        public Task<IComonResponse<string>> LoginAsync(string email, string password);
    }
}
