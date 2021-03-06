using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using persistance;
using application.Core;
using AutoMapper;
using domain;
using MediatR;

namespace application.MonthlyBudgets
{
    public class CreateMonthlyBudget
    {
        public class CreateMonthlyBudgetCommand : IRequest<Result<CreateMonthlyBudgetResponse>>
        {
            public decimal StartingBalance { get; set; }
        }

        public class CreateMonthlyBudgetResponse
        {
            public Guid Id { get; set; }
            public decimal StartingBalance { get; set; }
            public string YearMonth { get; set; }
            public DateTime MonthlyBudgetDate { get; private set; }

        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<MonthlyBudget, CreateMonthlyBudgetResponse>();
                CreateMap<CreateMonthlyBudgetCommand, MonthlyBudget>();
            }
        }

        public class Handler : IRequestHandler<CreateMonthlyBudgetCommand, Result<CreateMonthlyBudgetResponse>>
        {
            private readonly IMapper mapper;
            private readonly BudgetTrackerContext context;

            public Handler(IMapper mapper, BudgetTrackerContext context)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Result<CreateMonthlyBudgetResponse>> Handle(CreateMonthlyBudgetCommand request, CancellationToken cancellationToken)
            {
                var monthlyBudget = new MonthlyBudget(request.StartingBalance, DateTime.Now.Date.ToUniversalTime());
                context.MonthlyBudgets.Add(monthlyBudget);

                var result = await context.SaveChangesAsync() > 0;

                if (!result)
                    return Result<CreateMonthlyBudgetResponse>.Failure("Failed to create monthly budget");

                var response = mapper.Map<CreateMonthlyBudgetResponse>(monthlyBudget);
                return Result<CreateMonthlyBudgetResponse>.Success(response);
            }
        }
    }
}