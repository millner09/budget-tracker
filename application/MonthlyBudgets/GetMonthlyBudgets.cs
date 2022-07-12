using persistance;
using application.Core;
using AutoMapper;
using domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace application.MonthlyBudgets
{
    public class GetMonthlyBudgets
    {
        public class GetMonthlyBudgetResponse
        {
            public Guid Id { get; set; }
            public decimal StartingBalance { get; set; }
            public string YearMonth { get; set; }
        }

        

        public class Command : IRequest<Result<IEnumerable<GetMonthlyBudgetResponse>>> { }

        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<MonthlyBudget, GetMonthlyBudgetResponse>();
            }
        }

        public class Handler : IRequestHandler<Command, Result<IEnumerable<GetMonthlyBudgetResponse>>>
        {
            private readonly IMapper mapper;
            private readonly BudgetTrackerContext context;

            public Handler(IMapper mapper, BudgetTrackerContext context)
            {
                this.mapper = mapper;
                this.context = context;
            }

            public async Task<Result<IEnumerable<GetMonthlyBudgetResponse>>> Handle(Command request, CancellationToken cancellationToken)
            {
                var monthlyBudgets = await context.MonthlyBudgets.ToListAsync();
                var response = mapper.Map<IEnumerable<GetMonthlyBudgetResponse>>(monthlyBudgets);
                return Result<IEnumerable<GetMonthlyBudgetResponse>>.Success(response);
            }
        }
    }
}
