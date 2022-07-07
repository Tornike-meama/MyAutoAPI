using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAutoAPI1.Models;
using MyAutoAPI1.Services;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using MyAutoAPI1.Controllers.StatamentController;

namespace MyAutoAPI1.Controllers
{
    [Route("api/statement")]
    [ApiController]
    public class StatementController : ControllerBase
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
            List<Statement> res = await _statementService.GetAllStatements(queries.count, queries.fromIndex);
            return Ok(DataResponse<List<Statement>>.ReturnResponse(res));
        }

        [HttpGet]
        [Route("getById/{id}")]
        public async Task<IActionResult> GetStatementById([FromRoute] int id)
        {
            Statement res = await _statementService.GetStatementById(id);
            return Ok(DataResponse<Statement>.ReturnResponse(res));
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddStatement([FromBody] Statement statement)
        {
            Statement res = await _statementService.AddStatement(statement);
            return Ok(DataResponse<Statement>.ReturnResponse(res));
        }
    }
}
