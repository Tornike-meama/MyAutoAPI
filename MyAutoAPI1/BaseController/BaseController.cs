using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAutoAPI1.Models;
using System.Threading.Tasks;

namespace MyAutoAPI1.BaseController
{
    public class BaseController : ControllerBase
    {
        public BaseController() {}

        public IActionResult DataResponse<T>(ComonResponse<T> response)
        {
            if(response.IsError)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
