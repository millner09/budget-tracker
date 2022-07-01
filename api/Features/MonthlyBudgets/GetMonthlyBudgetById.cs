﻿using api.Data;
using api.Features.Core;
using api.Models;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.Features.MonthlyBudgets
{
    public class GetMonthlyBudgetById
    {
        public class Command : IRequest<Result<GetMonthlyBudgetByIdResponse>>
        {
            public Command(Guid id)
            {
                Id = id;
            }
            public Guid Id { get; set; }
        }

        public class GetMonthlyBudgetByIdResponse
        {
            public Guid Id { get; set; }
            public decimal StartingBalance { get; set; }
            public string YearMonth { get; set; }
            public List<ExpenseResponse> Expenses { get; set; }
            public List<IncomeResponse> Incomes { get; set; }
        }

        public class ExpenseResponse
        {
            public Guid Id { get; set; }
            public Guid CategoryId { get; set; }
            public decimal PlannedAmount { get; set; }
        }

        public class IncomeResponse
        {
            public Guid Id { get; set; }
            public Guid CategoryId { get; set; }
            public decimal PlannedAmount { get; set; }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<MonthlyBudget, GetMonthlyBudgetByIdResponse>()
                    .ForMember(dest => dest.Incomes, opt => opt.MapFrom(src => src.PlannedIncomes))
                    .ForMember(dest => dest.Expenses, opt => opt.MapFrom(src => src.PlannedExpenses));
                CreateMap<PlannedExpense, ExpenseResponse>();
                CreateMap<PlannedIncome, IncomeResponse>();
            }
        }

        public class Handler : IRequestHandler<Command, Result<GetMonthlyBudgetByIdResponse>>
        {
            private readonly IMapper mapper;
            private readonly BudgetTrackerContext context;

            public Handler(IMapper mapper, BudgetTrackerContext context)
            {
                this.mapper = mapper;
                this.context = context;
            }
            public async Task<Result<GetMonthlyBudgetByIdResponse>> Handle(Command request, CancellationToken cancellationToken)
            {
                var monthlyBudget = await context.MonthlyBudgets
                    .Include(x => x.PlannedExpenses)
                    .Include(x => x.PlannedIncomes)
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                if (monthlyBudget == null)
                    return null;

                var response = mapper.Map<GetMonthlyBudgetByIdResponse>(monthlyBudget);
                return Result<GetMonthlyBudgetByIdResponse>.Success(response);
            }
        }
    }
}