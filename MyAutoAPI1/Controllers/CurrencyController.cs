using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAutoAPI1.Controllers.GetBody.Currency;
using MyAutoAPI1.Models;
using MyAutoAPI1.Services;
using MyAutoAPI1.Services.Currency;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyAutoAPI1.Controllers
{
    [Route("api/currency")]
    [ApiController]
    public class CurrencyController : BaseController.BaseController
    {
        private readonly ICurrencyServices _currencyServices;

        public CurrencyController(ICurrencyServices currencyServices)
        {
            _currencyServices = currencyServices;
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAllCurrency()
        {
            var res = await _currencyServices.GetAllCurrencyAsync();
            return DataResponse(res);
        }

        [HttpGet]
        [Route("getById")]
        public async Task<IActionResult> GetCurrencyByI([FromQuery] int id)
        {
            var res = await _currencyServices.GetCurrencyByIdAsync(id);
            return DataResponse(res);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddCurrency([FromBody] AddCurrencyModel currency)
        {
            var res = await _currencyServices.AddCurrencyAsync(currency);
            return DataResponse(res);
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdateCurrency([FromBody] UpdateCurrencyModel currency)
        {
            var res = await _currencyServices.UpdateCurrencyAsync(currency);
            return DataResponse(res);
        }
    }
}
