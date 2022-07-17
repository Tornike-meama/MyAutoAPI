using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyAutoAPI1.Controllers.GetBody.Role;
using MyAutoAPI1.Models;
using MyAutoAPI1.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyAutoAPI1.Services.Role
{
    public class RoleServices : IRoleServices
    {
        private readonly MyDbContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public RoleServices(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, MyDbContext dbContext)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IComonResponse<string>>AddRoleAsync(string roleName, string creatorId)
        {
            try
            {
                if(roleName == null)
                {
                    return new BadRequest<string>("No provide role name");
                }

                IdentityResult roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));

                if(!roleResult.Succeeded)
                {
                    return new BadRequest<string>("can't create role somthing wrong");
                }

                return new ComonResponse<string>("role created!");

            }
            catch (Exception ex)
            {
                return new BadRequest<string>(ex.Message);
            }
        }

        public async Task<IComonResponse<string>>AddUserInRoleAsync(AdduserInRoleModel data)
        {
            try
            {
                var role = _dbContext.Roles.FirstOrDefault(o => o.Id == data.RoleId);
                var user = _dbContext.Users.FirstOrDefault(o => o.Id == data.UserId);

                var res = await _userManager.AddToRoleAsync(user, role.Name);

                if(!res.Succeeded)
                {
                    return new BadRequest<string>(string.Join(", ", res.Errors));
                }

                return new ComonResponse<string>("user added in role!");

            }
            catch (Exception ex)
            {
                return new BadRequest<string>(ex.Message);
            }
        }

        public async Task<IComonResponse<List<IdentityRole>>> GetAllRoleAsync()
        {
            try
            {
                List<IdentityRole> res = await _dbContext.Roles.ToListAsync();
                return new ComonResponse<List<IdentityRole>>(res);
            }
            catch (Exception ex)
            {
                return new BadRequest<List<IdentityRole>>(ex.Message);
            }
        }
    }
}
