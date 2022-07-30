using FluentValidation;
using MyAutoAPI1.Controllers.GetBody.Statement;

public class UpdateStatementValidator : AbstractValidator<UpdateStatement>
{
    public UpdateStatementValidator()
    {
        RuleFor(statement => statement.Id).NotNull().NotEmpty();
        RuleFor(statement => statement.Title).NotNull().NotEmpty();
        RuleFor(statement => statement.Price).NotNull().NotEmpty();
        RuleFor(statement => statement.CurrencyId).NotNull().NotEmpty();
    }
}
