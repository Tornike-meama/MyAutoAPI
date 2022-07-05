using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAutoAPI1.Models;
using MyAutoAPI1.Services;
using MyAutoAPI1.Services.Currency;
using System.Collections.Generic;

namespace MyAutoAPI1.Controllers
{
    [Route("api/currency")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyServices _currencyServices;

        public CurrencyController(ICurrencyServices currencyServices)
        {
            _currencyServices = currencyServices;
        }

        [HttpGet]
        [Route("getAll")]
        public IActionResult GetAllCurrency()
        {
            ComonResponse<List<Currency>> comonResponse = new ComonResponse<List<Currency>>();

            var res = _currencyServices.GetAllCurrency();

            if (res == null)
            {
                comonResponse.isError = true;
                comonResponse.data = default;
                return BadRequest(comonResponse);
            }
            comonResponse.isError = false;
            comonResponse.data = res;
            return Ok(comonResponse);
        }

        [HttpPost]
        [Route("add")]
        public IActionResult AddCurrency(Currency currency)
        {
            ComonResponse<Currency> comonResponse = new ComonResponse<Currency>();

            var res = _currencyServices.AddCurrency(currency);

            if(res == null)
            {
                comonResponse.isError = true;
                comonResponse.data = null;
                return BadRequest(comonResponse);
            }
            return Ok(comonResponse);
        }
    }
}
