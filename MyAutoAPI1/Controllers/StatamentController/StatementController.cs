using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAutoAPI1.Models;
using MyAutoAPI1.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyAutoAPI1.Controllers.StatamentController
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
        [Route("getById/{id}")]
        public async Task<IActionResult> GetStatementById([FromRoute] int id)
        {
            ComonResponse<Statement> comonResponse = new ComonResponse<Statement>();
            var res = await _statementService.GetStatementById(id);
            if (res == null)
            {
                comonResponse.isError = true;
                comonResponse.data = null;
                return NotFound(comonResponse);
            }
            comonResponse.isError = false;
            comonResponse.data = res;
            return Ok(comonResponse);
        }

        class StatementModel
        {
            public int count { get; set; }
            public int fromIndex { get; set; }
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAllStatements([FromQuery] GetStatementsQuery queries)
        {
            ComonResponse<List<Statement>> comonResponse = new ComonResponse<List<Statement>>();
            var res = await _statementService.GetAllStatements(queries.count, queries.fromIndex);
            if (res == null)
            {
                comonResponse.isError = true;
                comonResponse.data = null;
                return NotFound(comonResponse);
            }
            comonResponse.isError = false;
            comonResponse.data = res;
            return Ok(comonResponse);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddStatement([FromBody] Statement statement)
        {
            ComonResponse<Statement> comonResponse = new ComonResponse<Statement>();

            var res = await _statementService.AddStatement(statement);
            if (res == null)
            {
                comonResponse.isError = true;
                comonResponse.data = null;
                return NotFound(comonResponse);
            }
            comonResponse.isError = false;
            comonResponse.data = res;
            return Ok(comonResponse);
        }
    }
}
