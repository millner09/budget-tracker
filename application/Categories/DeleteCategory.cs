using application.Core;
using MediatR;
using persistance;

namespace application.Categories
{
    public class DeleteCategory
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
            public Command(Guid id) => Id = id;
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly BudgetTrackerContext context;

            public Handler(BudgetTrackerContext context)
            {
                this.context = context;

            }
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var category = await context.Categories.FindAsync(request.Id);

                if (category == null)
                    return null;

                context.Remove(category);

                var result = await context.SaveChangesAsync() > 0;
                if (!result)
                    return Result<Unit>.Failure("Failed to delete category");

                return Result<Unit>.Success(Unit.Value);

            }
        }
    }
}