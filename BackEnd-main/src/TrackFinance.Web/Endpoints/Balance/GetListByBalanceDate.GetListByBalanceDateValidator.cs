using FluentValidation;

namespace TrackFinance.Web.Endpoints.Balance;

public class GetListByBalanceDateValidator : AbstractValidator<GetListByBalanceDateRequest>
{
  public GetListByBalanceDateValidator()
  {
    RuleFor(expense => expense.UserId).GreaterThan(0);
  }
}
