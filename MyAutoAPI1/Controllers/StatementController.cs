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

        //TODO: add for test authorize attirbiute remove in the future
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddStatement([FromBody] Statement statement)
        {
            var res = await _statementService.AddStatement(statement);
            return DataResponse(res);
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdateStatement([FromBody] Statement statement)
        {
            var res = await _statementService.UpdateStatement(statement);
            return DataResponse(res);
        }
    }
}
