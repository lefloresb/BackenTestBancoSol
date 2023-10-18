using Ardalis.ApiEndpoints;
using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TrackFinance.Core.Interfaces;
using TrackFinance.Core.Services;
using TrackFinance.Core.TransactionAgregate.Enum;

namespace TrackFinance.Web.Endpoints.Balance;

public class GetListByBalanceDate : EndpointBaseAsync
    .WithRequest<GetListByBalanceDateRequest>
    .WithActionResult<GetListByBalanceDateResponse>
{
  private readonly ITransactionFinanceService _transactionService;
  public GetListByBalanceDate(ITransactionFinanceService transactionService)
  {
    _transactionService = transactionService;
  }

  [HttpGet(GetListByBalanceDateRequest.Route)]
  [Produces("application/json")]
  [SwaggerOperation(
    Summary = "group",
    Description = "group",
    OperationId = "BalanceDate.List",
    Tags = new[] { "BalanceDateEndpoints" })
]
  public override async Task<ActionResult<GetListByBalanceDateResponse>> HandleAsync([FromRoute] GetListByBalanceDateRequest request, CancellationToken cancellationToken = default)
  {
    var transactions = await _transactionService.GetTransactionItemsByDateAsync(request.UserId, request.TransactionType, request.DateIni, request.DateEnd, cancellationToken);

    if (transactions.Status != ResultStatus.Ok) return BadRequest(transactions.ValidationErrors);

    return Ok(new BalanceListResponse
    {
      ExpensesTransaction = GetTransactionsRecords(transactions.Value, TransactionType.Expense),
      IncomesTransaction = GetTransactionsRecords(transactions.Value, TransactionType.Income)
    });
  }

  private static List<TransactionRecord>? GetTransactionsRecords(List<TransactionDataDto> transactions, TransactionType transactionType)
  {
    return new List<TransactionRecord>(transactions.Where(x => x.TransactionType == transactionType)
                       .Select(item => new TransactionRecord(
                        item.Date,
                        item.DayOfWeek,
                        item.Day,
                        item.TotalAmount,
                        item.TransactionDescriptionType,
                        item.Week,
                        item.Year,
                        item.Month
                       )));
  }
}
