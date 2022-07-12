using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using persistance;
using application.Core;
using AutoMapper;
using domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace application.Categories
{
    public class GetAllCategories
    {
        public class Command : IRequest<Result<IEnumerable<GetAllCategoriesResponse>>> { }

        public class GetAllCategoriesResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile() => CreateMap<Category, GetAllCategoriesResponse>();
        }

        public class Handler : IRequestHandler<Command, Result<IEnumerable<GetAllCategoriesResponse>>>
        {
            private readonly IMapper mapper;
            private readonly BudgetTrackerContext context;
            public Handler(IMapper mapper, BudgetTrackerContext context)
            {
                this.context = context;
                this.mapper = mapper;

            }
            public async Task<Result<IEnumerable<GetAllCategoriesResponse>>> Handle(Command request, CancellationToken cancellationToken)
            {
                var result = mapper.Map<IEnumerable<GetAllCategoriesResponse>>(await context.Categories.ToListAsync());

                return Result<IEnumerable<GetAllCategoriesResponse>>.Success(result);
            }
        }
    }
}