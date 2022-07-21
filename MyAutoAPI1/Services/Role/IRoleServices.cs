using Microsoft.AspNetCore.Identity;
using MyAutoAPI1.Controllers.GetBody.Role;
using MyAutoAPI1.Models.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyAutoAPI1.Services.Role
{
    public interface IRoleServices
    {
        public Task<IComonResponse<string>>AddRoleAsync(string roleName, string creatorId);
        public Task<IComonResponse<List<IdentityRole>>>GetAllRoleAsync();
        public Task<IComonResponse<string>>AddUserInRoleAsync(AdduserInRoleModel data);
        public Task CheckRolesAsyncInBG();
    }
}
