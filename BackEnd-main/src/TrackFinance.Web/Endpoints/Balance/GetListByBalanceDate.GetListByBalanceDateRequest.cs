using TrackFinance.Core.TransactionAgregate.Enum;
using TrackFinance.Core.TransactionAgregate;

namespace TrackFinance.Web.Endpoints.Balance;

public class GetListByBalanceDateRequest
{
  public const string Route = "/Balance/{DateType}/{UserId}/{TransactionType}/{DateIni}/{DateEnd}";
  public DateTime DateIni { get; set; }
  public DateTime DateEnd { get; set; }
  public int UserId { get; set; }
  public TransactionType TransactionType { get; set; }
}
