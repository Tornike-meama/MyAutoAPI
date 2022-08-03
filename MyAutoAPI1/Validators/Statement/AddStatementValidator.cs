using FluentValidation;
using MyAutoAPI1.Controllers.GetBody.Statement;

public class AddStatementValidator : AbstractValidator<AddStatementModel>
{
    public AddStatementValidator()
    {
        RuleFor(statement => statement.Title).NotNull().NotEmpty();
        RuleFor(statement => statement.Price).NotNull().NotEmpty();
        RuleFor(statement => statement.CurrencyId).NotNull().NotEmpty();
    }
}
