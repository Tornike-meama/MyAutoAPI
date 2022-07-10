using MyAutoAPI1.Controllers.GetQueries;
using MyAutoAPI1.Models;
using System.Threading.Tasks;

namespace MyAutoAPI1.Services.Identity
{
    public interface IIdentityServices
    {
        public Task<AuthResponse> RegisterAsync(string email, string password, string name);
        public Task<AuthResponse> LoginAsync(string email, string password);
    }
}
