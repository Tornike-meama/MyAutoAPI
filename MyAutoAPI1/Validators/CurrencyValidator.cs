using FluentValidation;
using MyAutoAPI1.Controllers.GetBody.Currency;
using MyAutoAPI1.Models;

public class CurrencyValidator : AbstractValidator<Currency>
{
    public CurrencyValidator()
    {
        RuleFor(currency => currency.Name).NotNull().NotEmpty();
        RuleFor(currency => currency.ShortName).NotNull().Length(0,5);
        RuleFor(currency => currency.Symbol).NotNull().NotEmpty();
    }
    public string Name { get; set; }
    public string ShortName { get; set; }
    public string Symbol { get; set; }
}
