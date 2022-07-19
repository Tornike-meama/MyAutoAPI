using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyAutoAPI1.DTO.Identity;
using MyAutoAPI1.Models;
using MyAutoAPI1.Models.Responses;
using MyAutoAPI1.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyAutoAPI1.Services.Identity
{
    public class IdentityServices : IIdentityServices
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly MyDbContext _dbContext;

        public IdentityServices(
                UserManager<IdentityUser> userManager, 
                JwtSettings jwtSettings, 
                MyDbContext dbContext
               )
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings;
            _dbContext = dbContext;
        }

        public async Task<IComonResponse<string>> LoginAsync(string email, string password)
        {
            try
            {
                var authUser = await _userManager.FindByEmailAsync(email);

                if(authUser == null)
                {
                    return new NotFound<string>("user is not register!");
                }

                var isValidPassword = await _userManager.CheckPasswordAsync(authUser, password);

                if(!isValidPassword)
                {
                    return new BadRequest<string>("user or password is ivalid!");
                }

                return await GenerateAuthResultForUser(authUser);
            }
            catch (Exception ex)
            {
                return new BadRequest<string>(ex.Message);
            }
        }

        public async Task<IComonResponse<string>> RegisterAsync(string email, string password, string name)
        {
            try
            {
                var isUser = await _userManager.FindByEmailAsync(email);

                if(isUser != null)
                {
                    return new BadRequest<string>("user alrady registered!");
                }

                var newUser = new IdentityUser()
                {
                    Email = email,
                    UserName = name,
                };

                var createdUser = await _userManager.CreateAsync(newUser, password);

                if(!createdUser.Succeeded)
                {
                    return new BadRequest<string>("user can't register!");
                }

                return await GenerateAuthResultForUser(newUser);
            }
            catch (Exception ex)
            {
                return new BadRequest<string>(ex.Message);
            }
        }

        public async Task<IComonResponse<List<UserDTO>>>GetAllUsersAsync()
        {
            try
            {
                var res = await _dbContext.Users.ToListAsync();

                var usersList = res.Select(o => new UserDTO
                {
                        Id = o.Id,
                        UserName = o.UserName,
                        Email = o.Email,
                        EmailConfirmed = o.EmailConfirmed,
                        PhoneNumber = o.PhoneNumber,
                        PhoneNumberConfirmed = o.PhoneNumberConfirmed,
                        UserRoles = _dbContext.UserRoles.Where(i => i.UserId == o.Id).Select(i => i.RoleId).ToList()
                        
                }).ToList();

                return new ComonResponse<List<UserDTO>>(usersList);
            }
            catch (Exception ex)
            {
                return new BadRequest<List<UserDTO>>(ex.Message);
            }
        }
        
        public async Task<IComonResponse<UserDTO>>GetUserByIdAsync(string id)
        {
            try
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(o => o.Id == id);

                if(user == null)
                {
                    return new BadRequest<UserDTO>("Can't find user");
                }

                List<string> userRoleIds = await _dbContext.UserRoles.Where(o => o.UserId == id).Select(o => o.RoleId).ToListAsync();

                var res = new UserDTO
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed,
                    PhoneNumber = user.PhoneNumber,
                    PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                    UserRoles = userRoleIds
                };

                return new ComonResponse<UserDTO>(res);
            }
            catch (Exception ex)
            {
                return new BadRequest<UserDTO>(ex.Message);
            }
        }

        private async Task<IComonResponse<string>>GenerateAuthResultForUser(IdentityUser user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var logedUserRoles = from userRole in _dbContext.UserRoles
                                 join role in _dbContext.Roles
                                 on userRole.UserId equals user.Id
                                 select role;
            var userClaims = await _userManager.GetRolesAsync(user);

            if(userClaims.Any())
            {
                foreach (var role in userClaims)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthResponse<string>(tokenHandler.WriteToken(token), "Success Loged In");
        }

    }
   
}
