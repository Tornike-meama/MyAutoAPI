using FluentValidation;
using MyAutoAPI1.Controllers.GetBody.Currency;

namespace MyAutoAPI1.Validators.Currency
{
    public class UpdateCurrencyvalidator : AbstractValidator<UpdateCurrencyModel>
    {
        public UpdateCurrencyvalidator()
        {
            RuleFor(currency => currency.Id).NotNull().NotEmpty();
            RuleFor(currency => currency.Name).NotNull().NotEmpty();
            RuleFor(currency => currency.ShortName).NotNull().Length(0, 5);
            RuleFor(currency => currency.Symbol).NotNull().NotEmpty();
        }
    }
}
