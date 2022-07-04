using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAutoAPI1.Models;
using MyAutoAPI1.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        [Route("getById/{id}")]
        public IActionResult GetStatementById([FromRoute]int id)
        {
            ComonResponse<Statement> comonResponse = new ComonResponse<Statement>();
            var res = _statementService.GetStatementById(id);
            if(res == null)
            {
                comonResponse.isError = true;
                comonResponse.data = null;
                return NotFound(comonResponse);
            }
            comonResponse.isError = false;
            comonResponse.data = res;
            return Ok(comonResponse);
        }

        [HttpGet]
        [Route("getAll")]
        public IActionResult GetAllStatements()
        {
            ComonResponse<List<Statement>> comonResponse = new ComonResponse<List<Statement>>();
            var res = _statementService.GetAllStatements();
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
