using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAutoAPI1.Models;
using System;
using MyAutoAPI1.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyAutoAPI1.Controllers.StatamentController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net.Http;
using System.Security.Claims;
using MyAutoAPI1.Controllers.GetBody.Statement;

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
        public async Task<IActionResult> GetAllStatements([FromQuery] StatementsQuery queries)
        {
            var res = await _statementService.GetAllStatements(queries.Count, queries.FromIndex);
            return DataResponse(res);
        }

        [HttpGet]
        [Route("getById/{id}")]
        public async Task<IActionResult> GetStatementById([FromRoute] int id)
        {
            var res = await _statementService.GetStatementById(id);
            return DataResponse(res);
        }

        [HttpGet]
        [Route("getByUserId")]
        public async Task<IActionResult> GetStatementByUserId([FromQuery] string userId)
        {
            var res = await _statementService.GetStatementByUserId(userId);
            return DataResponse(res);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddStatement([FromBody] AddStatementModel statement)
        {
            var userId =  User.FindFirstValue(ClaimTypes.NameIdentifier);
            var res = await _statementService.AddStatement(statement, userId);
            return DataResponse(res);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdateStatement([FromBody] Statement statement)
        {
            var res = await _statementService.UpdateStatement(statement);
            return DataResponse(res);
        }
    }
}
