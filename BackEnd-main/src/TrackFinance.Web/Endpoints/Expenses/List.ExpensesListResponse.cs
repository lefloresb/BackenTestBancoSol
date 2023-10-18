using TrackFinance.Web.Endpoints.Incomes;

namespace TrackFinance.Web.Endpoints.Expenses;

public class ExpensesListResponse
{
  public List<ExpenseRecord> Incomes { get; set; } = new();
}
