using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TrackFinance.Core.TransactionAgregate;
using TrackFinance.Core.TransactionAgregate.Enum;
using TrackFinance.SharedKernel.Interfaces;
using TrackFinance.Web.Endpoints.Incomes;

namespace TrackFinance.Web.Endpoints.Expenses;

public class Update : EndpointBaseAsync
    .WithRequest<UpdateExpensesRequest>
    .WithActionResult<UpdateExpensesResponse>
{
  private readonly IRepository<Transaction> _repository;
  public Update(IRepository<Transaction> repository)
  {
    _repository = repository;
  }
  [HttpPut(UpdateExpensesRequest.Route)]
  [Produces("application/json")]
  [SwaggerOperation(
     Summary = "Updates a expenses",
     Description = "Updates a expenses",
     OperationId = "Expenses.Update",
     Tags = new[] { "ExpensesEndpoints" })
  ]
  public override async Task<ActionResult<UpdateExpensesResponse>> HandleAsync(UpdateExpensesRequest request, CancellationToken cancellationToken = default)
  {
    var existingExpenses = await _repository.GetByIdAsync(request.Id, cancellationToken);

    if (existingExpenses == null)
    {
      return NotFound();
    }

    existingExpenses.UpdateValue(request.Description, request.Amount, request.ExpenseType, request.ExpenseDate, request.UserId, TransactionType.Income);

    await _repository.UpdateAsync(existingExpenses, cancellationToken);

    var response = new UpdateExpensesResponse(
        expenseRecord: new ExpenseRecord(
          existingExpenses.Id,
          existingExpenses.Description,
          existingExpenses.Amount,
          existingExpenses.TransactionDescriptionType,
          existingExpenses.ExpenseDate,
          existingExpenses.UserId,
          existingExpenses.TransactionType));

    return Ok(response);
  }
}
