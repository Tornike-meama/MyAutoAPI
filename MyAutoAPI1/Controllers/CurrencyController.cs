using Microsoft.AspNetCore.Mvc;
using MyAutoAPI1.Controllers.GetBody.Currency;
using MyAutoAPI1.Services.Currency;
using System.Threading.Tasks;
using FluentValidation.Results;
using System.Linq;
using MyAutoAPI1.Validators.Currency;
using MyAutoAPI1.Validators;

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
        public async Task<IActionResult> GetAllCurrency() => DataResponse(await _currencyServices.GetAllCurrencyAsync());

        [HttpGet]
        [Route("getById")]
        public async Task<IActionResult> GetCurrencyByI([FromQuery] int id) => DataResponse(await _currencyServices.GetCurrencyByIdAsync(id));

        [HttpPost]
        [Route("add")]
        [RequestsValidator(Arguments = new object[] { typeof(AddCurrencyValidator)})]
        public async Task<IActionResult> AddCurrency([FromBody] AddCurrencyModel currency) => DataResponse(await _currencyServices.AddCurrencyAsync(currency));

        [HttpPost]
        [Route("update")]
        [RequestsValidator(Arguments = new object[] {typeof(UpdateCurrencyvalidator)})]
        public async Task<IActionResult> UpdateCurrency([FromBody] UpdateCurrencyModel currency) => DataResponse(await _currencyServices.UpdateCurrencyAsync(currency));

    }
}
