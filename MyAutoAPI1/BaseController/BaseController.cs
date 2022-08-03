using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAutoAPI1.Models.Responses;
using System.Threading.Tasks;

namespace MyAutoAPI1.BaseController
{
    public class BaseController : ControllerBase
    {
        public BaseController() {}

        public IActionResult DataResponse<T>(IComonResponse<T> response)
        {
            if(response is BadRequest<T>)
            {
                return BadRequest(response);
            }
            if(response is NotFound<T>)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

    }
}
