using FluentValidation;
using MyAutoAPI1.Controllers.GetBody.Currency;
using MyAutoAPI1.Models;

public class AddCurrencyValidator : AbstractValidator<AddCurrencyModel>
{
    public AddCurrencyValidator()
    {
        RuleFor(currency => currency.Name).NotNull().NotEmpty();
        RuleFor(currency => currency.ShortName).NotNull().Length(0,5);
        RuleFor(currency => currency.Symbol).NotNull().NotEmpty();
    }
}
