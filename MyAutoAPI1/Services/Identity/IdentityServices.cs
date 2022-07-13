using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MyAutoAPI1.Controllers.GetQueries;
using MyAutoAPI1.Models;
using MyAutoAPI1.Models.Responses;
using MyAutoAPI1.Options;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyAutoAPI1.Services.Identity
{
    public class IdentityServices : IIdentityServices
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtSettings _jwtSettings;

        public IdentityServices(UserManager<IdentityUser> userManager, JwtSettings jwtSettings)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings;
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

                return GenerateAuthResultForUser(authUser);
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

                return GenerateAuthResultForUser(newUser);
            }
            catch (Exception ex)
            {
                return new BadRequest<string>(ex.Message);
            }
        }

      
        private IComonResponse<string> GenerateAuthResultForUser(IdentityUser newUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                        new Claim(ClaimTypes.NameIdentifier, newUser.Id),
                        new Claim(JwtRegisteredClaimNames.Sub, newUser.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Sub, newUser.Email),
                    }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthResponse<string>(tokenHandler.WriteToken(token), "Success Loged In");
        }

    }
   
}
