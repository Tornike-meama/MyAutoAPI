using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAutoAPI1.Controllers.GetBody.Role;
using MyAutoAPI1.Services.Role;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyAutoAPI1.Controllers
{
    [Route("roles")]
    [ApiController]
    public class RoleController : BaseController.BaseController
    {
        private readonly IRoleServices _roleServices;

        public RoleController(IRoleServices roleServices)
        {
            _roleServices = roleServices;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateRole([FromBody] AddRoleModel addRole)
        {
            var res = await _roleServices.AddRoleAsync(addRole.RoleName, User.FindFirstValue(ClaimTypes.NameIdentifier));
            return DataResponse(res);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAllRole([FromBody] AddRoleModel addRole)
        {
            var res = await _roleServices.GetAllRoleAsync();
            return DataResponse(res);
        }

        [Authorize(Roles = "Editor")]
        [HttpPost]
        [Route("addUserInRole")]
        public async Task<IActionResult> AddUserInRole([FromBody] AdduserInRoleModel data)
        {
            var res = await _roleServices.AddUserInRoleAsync(data);
            return DataResponse(res);
        }
    }
}
