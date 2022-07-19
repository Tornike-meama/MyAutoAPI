using Microsoft.AspNetCore.Identity;
using MyAutoAPI1.Controllers.GetQueries;
using MyAutoAPI1.DTO.Identity;
using MyAutoAPI1.Models;
using MyAutoAPI1.Models.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyAutoAPI1.Services.Identity
{
    public interface IIdentityServices
    {
        public Task<IComonResponse<string>> RegisterAsync(string email, string password, string name);
        public Task<IComonResponse<string>> LoginAsync(string email, string password);
        public Task<IComonResponse<List<UserDTO>>> GetAllUsersAsync();
        public Task<IComonResponse<UserDTO>> GetUserByIdAsync(string id);
    }
}
