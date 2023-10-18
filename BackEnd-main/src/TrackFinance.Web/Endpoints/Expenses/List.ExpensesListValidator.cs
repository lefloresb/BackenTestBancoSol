using FluentValidation;
using TrackFinance.Web.Endpoints.Incomes;

namespace TrackFinance.Web.Endpoints.Expenses;

public class ExpensesListValidator : AbstractValidator<ExpensesListRequest>
{
  public ExpensesListValidator()
  {
    RuleFor(expense => expense.UserId).GreaterThan(0);
  }
}
