using FluentValidation;
using MyAutoAPI1.Controllers.GetBody.Currency;
using MyAutoAPI1.Models;

public class StatementValidator : AbstractValidator<Statement>
{
    public StatementValidator()
    {
        RuleFor(currency => currency.Title).NotNull().NotEmpty();
        RuleFor(currency => currency.Price).NotNull().NotEmpty();
        RuleFor(currency => currency.Creator).NotNull().NotEmpty();
        RuleFor(currency => currency.CurrencyId).NotNull().NotEmpty();
    }
}
