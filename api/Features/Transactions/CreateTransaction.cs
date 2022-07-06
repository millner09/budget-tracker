using api.Features.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.Features.Transactions
{
    public class CreateTransaction
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid MonthlyBudgetId { get; set; }
            public DateTime TransactionDate { get; set; }
            public string Description { get; set; }
            public Guid CategoryId { get; set; }
        }
    }
}
