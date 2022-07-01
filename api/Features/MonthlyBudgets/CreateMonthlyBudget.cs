using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Features.Core;
using api.Models;
using AutoMapper;
using MediatR;

namespace api.Features.MonthlyBudgets
{
    public class CreateMonthlyBudget
    {
        public class Command : IRequest<Result<CreateMonthlyBudgetResponse>>
        {
            public decimal StartingBalance { get; set; }
        }

        public class CreateMonthlyBudgetResponse
        {
            public Guid Id { get; set; }
            public decimal StartingBalance { get; set; }
            public string YearMonth { get; set; }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<MonthlyBudget, CreateMonthlyBudgetResponse>();
                CreateMap<Command, MonthlyBudget>();
            }
        }

        public class Handler : IRequestHandler<Command, Result<CreateMonthlyBudgetResponse>>
        {
            private readonly IMapper mapper;
            private readonly BudgetTrackerContext context;

            public Handler(IMapper mapper, BudgetTrackerContext context)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Result<CreateMonthlyBudgetResponse>> Handle(Command request, CancellationToken cancellationToken)
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