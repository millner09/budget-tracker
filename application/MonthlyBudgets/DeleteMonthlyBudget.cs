using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using persistance;
using application.Core;
using MediatR;

namespace application.MonthlyBudgets
{
    public class DeleteMonthlyBudget
    {
        public class DeleteMonthlyBudgetCommand : IRequest<Result<Unit>>
        {
            public DeleteMonthlyBudgetCommand(Guid id) => Id = id;

            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<DeleteMonthlyBudgetCommand, Result<Unit>>
        {
            private readonly BudgetTrackerContext context;
            public Handler(BudgetTrackerContext context)
            {
                this.context = context;

            }

            public async Task<Result<Unit>> Handle(DeleteMonthlyBudgetCommand request, CancellationToken cancellationToken)
            {
                var monthlyBudget = await context.MonthlyBudgets.FindAsync(request.Id);

                if (monthlyBudget == null)
                    return null;

                context.MonthlyBudgets.Remove(monthlyBudget);

                var result = await context.SaveChangesAsync() > 0;

                if (!result)
                    return Result<Unit>.Failure("Failed to delete monthly budget.");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}