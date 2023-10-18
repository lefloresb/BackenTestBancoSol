using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using TrackFinance.Core.TransactionAgregate.Enum;

namespace TrackFinance.Core.TransactionAgregate.Specifications;
public class ItemsByDateSpec : Specification<Transaction>, ISingleResultSpecification
{
  public ItemsByDateSpec(int userId, TransactionType transactionType, DateTime dateIni, DateTime dateEnd)
  {    
    Query.Where(h => h.ExpenseDate.Date >= dateIni && h.ExpenseDate.Date <= dateEnd)
         .Where(h => (transactionType == TransactionType.All) || h.TransactionType == transactionType)
         .Where(h => h.UserId == userId)
         .OrderBy(g => g.ExpenseDate);
  }
}
