using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Features.Core;
using api.Models;
using AutoMapper;
using MediatR;

namespace api.Features.Categories
{
    public class GetCategoryById
    {
        public class GetCategoryByIdResult
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }

        public class Command : IRequest<Result<GetCategoryByIdResult>>
        {
            public Command(Guid id) => Id = id;
            public Guid Id { get; set; }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile() => CreateMap<Category, GetCategoryByIdResult>();
        }

        public class Handler : IRequestHandler<Command, Result<GetCategoryByIdResult>>
        {
            private readonly IMapper mapper;
            private readonly BudgetTrackerContext context;

            public Handler(IMapper mapper, BudgetTrackerContext context)
            {
                this.mapper = mapper;
                this.context = context;
            }

            public async Task<Result<GetCategoryByIdResult>> Handle(Command request, CancellationToken cancellationToken)
            {
                var category = await context.Categories.FindAsync(request.Id);

                return Result<GetCategoryByIdResult>.Success(mapper.Map<GetCategoryByIdResult>(category));
            }
        }
    }
}