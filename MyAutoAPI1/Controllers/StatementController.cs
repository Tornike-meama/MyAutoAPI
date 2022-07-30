using Microsoft.AspNetCore.Mvc;
using MyAutoAPI1.Services;
using System.Threading.Tasks;
using MyAutoAPI1.Controllers.StatamentController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;
using MyAutoAPI1.Controllers.GetBody.Statement;
using FluentValidation.Results;
using System.Linq;

namespace MyAutoAPI1.Controllers
{
    [Route("api/statement")]
    [ApiController]
    public class StatementController : BaseController.BaseController
    {
        private readonly IStatementServices _statementService;

        public StatementController(IStatementServices statementServices)
        {
            _statementService = statementServices;
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAllStatements([FromQuery] StatementsQuery queries) =>  DataResponse(await _statementService.GetAllStatementsAsync(queries.Count, queries.FromIndex));

        [HttpGet]
        [Route("getById/{id}")]
        public async Task<IActionResult> GetStatementById([FromRoute] int id) => DataResponse(await _statementService.GetStatementByIdAsync(id));

        [HttpGet]
        [Route("getByUserId")]
        public async Task<IActionResult> GetStatementByUserId([FromQuery] string userId) => DataResponse(await _statementService.GetStatementByUserIdAsync(userId));

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddStatement([FromBody] AddStatementModel statement)
        {

            AddStatementValidator validaor = new AddStatementValidator();
            ValidationResult validationResult = await validaor.ValidateAsync(statement);

            if (!validationResult.IsValid)
            {
                return BadRequest(string.Join(", ", validationResult.Errors.Select(o => o.ErrorMessage)));
            }

            var userId =  User.FindFirstValue(ClaimTypes.NameIdentifier);
            statement.Creator = userId;
            var res = await _statementService.AddStatementAsync(statement, userId);
            return DataResponse(res);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdateStatement([FromBody] UpdateStatement statement)
        {
            UpdateStatementValidator validaor = new UpdateStatementValidator();
            ValidationResult validationResult = await validaor.ValidateAsync(statement);

            if (!validationResult.IsValid)
            {
                return BadRequest(string.Join(", ", validationResult.Errors.Select(o => o.ErrorMessage)));
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var res = await _statementService.UpdateStatementAsync(statement, userId);
            return DataResponse(res);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteStatement([FromRoute] int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var res = await _statementService.DeleteStatementAsync(id, userId);
            return DataResponse(res);
        }
    }
}
