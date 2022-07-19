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
        public async Task<IActionResult> Register([FromBody] RegistrationRequest request) => DataResponse(await _identityServices.RegisterAsync(request.Email, request.Password, request.Name));

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request) => DataResponse(await _identityServices.LoginAsync(request.Email, request.Password));

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAllUser() => DataResponse(await _identityServices.GetAllUsersAsync());

        [HttpGet]
        [Route("getUserById/{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] string id) => DataResponse(await _identityServices.GetUserByIdAsync(id));

    }
}
