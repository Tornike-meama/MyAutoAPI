using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAutoAPI1.Controllers.GetQueries;
using MyAutoAPI1.Services.Identity;
using System;
using System.Threading.Tasks;

namespace MyAutoAPI1.Controllers
{
    [Route("identity")]
    [ApiController]
    public class IdentityController : BaseController.BaseController
    {
        private readonly IIdentityServices _identityServices;

        public IdentityController(IIdentityServices identityServices)
        {
            _identityServices = identityServices;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequest request)
        {
            var res = await _identityServices.RegisterAsync(request.Email, request.Password, request.Name);

            if(res.IsError)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var res = await _identityServices.LoginAsync(request.Email, request.Password);

            if (res.IsError)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAllUser()
        {
           var res = await _identityServices.GetAllUsersAsync();
           return DataResponse(res);
        }

    }
}
