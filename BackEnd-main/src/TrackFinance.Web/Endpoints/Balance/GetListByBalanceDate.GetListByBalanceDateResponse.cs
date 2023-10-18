namespace TrackFinance.Web.Endpoints.Balance;

public class GetListByBalanceDateResponse
{
  public List<TransactionRecord>? ExpensesTransaction { get; set; }
  public List<TransactionRecord>? IncomesTransaction { get; set; }
}
