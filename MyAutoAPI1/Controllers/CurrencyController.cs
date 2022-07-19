using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAutoAPI1.Controllers.GetBody.Currency;
using MyAutoAPI1.Models;
using MyAutoAPI1.Services;
using MyAutoAPI1.Services.Currency;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Results;
using MyAutoAPI1.Validators;
using System.Linq;

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
        public async Task<IActionResult> AddCurrency([FromBody] AddCurrencyModel currency)
        {
            AddCurrencyValidator validaor = new AddCurrencyValidator();
            ValidationResult validationResult = await validaor.ValidateAsync(currency);

            if (!validationResult.IsValid)
            {
                return BadRequest(string.Join(", ", validationResult.Errors.Select(o => o.ErrorMessage)));
            }

            return DataResponse(await _currencyServices.AddCurrencyAsync(currency));
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdateCurrency([FromBody] UpdateCurrencyModel currency)
        {
            UpdateCurrencyvalidator validaor = new UpdateCurrencyvalidator();
            ValidationResult validationResult = await validaor.ValidateAsync(currency);

            if(!validationResult.IsValid)
            {
                return BadRequest(string.Join(", ", validationResult.Errors.Select(o => o.ErrorMessage)));
            }
            
            return DataResponse(await _currencyServices.UpdateCurrencyAsync(currency));

        }
    }
}
