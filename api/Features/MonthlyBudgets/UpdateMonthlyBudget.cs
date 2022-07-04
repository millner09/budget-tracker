using api.Data;
using api.Features.Core;
using api.Models;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.Features.MonthlyBudgets
{
    public class UpdateMonthlyBudget
    {
        public class UpdateMonthlyBudgetRequest
        {
            public decimal StartingBalance { get; set; }
            public DateTime MonthlyBudgetDate { get; set; }
        }

        public class Command : IRequest<Result<UpdateMonthlyBudgetResponse>> {
            public Command(Guid id, UpdateMonthlyBudgetRequest request)
            {
                Request = request;
                Id = id;
            }

            public Guid Id { get; set; }
            public UpdateMonthlyBudgetRequest Request { get; set; }
        }

        public class UpdateMonthlyBudgetResponse
        {
            public decimal StartingBalance { get; set; }
            public DateTime MonthlyBudgetDate { get; set; }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<UpdateMonthlyBudgetRequest, MonthlyBudget>();
                CreateMap<MonthlyBudget, UpdateMonthlyBudgetResponse>();
            }
        }

        public class Handler : IRequestHandler<Command, Result<UpdateMonthlyBudgetResponse>>
        {
            private readonly IMapper mapper;
            private readonly BudgetTrackerContext context;

            public Handler(IMapper mapper, BudgetTrackerContext context)
            {
                this.mapper = mapper;
                this.context = context;
            }

            public async Task<Result<UpdateMonthlyBudgetResponse>> Handle(Command request, CancellationToken cancellationToken)
            {
                var monthlyBudgetToUpdate = await context.MonthlyBudgets.FindAsync(request.Id);
                if (monthlyBudgetToUpdate == null)
                    return null;

                mapper.Map(request.Request, monthlyBudgetToUpdate);
                monthlyBudgetToUpdate.SetMonthlyBudgetDate(request.Request.MonthlyBudgetDate);

                var result = await context.SaveChangesAsync() > 0;

                if (!result)
                    return Result<UpdateMonthlyBudgetResponse>.Failure("Failed to update monthly budget");

                var response = mapper.Map<UpdateMonthlyBudgetResponse>(monthlyBudgetToUpdate);
                return Result<UpdateMonthlyBudgetResponse>.Success(response);
            }
        }
    }
}
