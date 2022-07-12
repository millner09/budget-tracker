using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using persistance;
using application.Core;
using AutoMapper;
using domain;
using MediatR;

namespace application.Categories
{
    public class GetCategoryById
    {
        public class GetCategoryByIdResult
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }

        public class GetCategoryByIdCommand : IRequest<Result<GetCategoryByIdResult>>
        {
            public GetCategoryByIdCommand(Guid id) => Id = id;
            public Guid Id { get; set; }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile() => CreateMap<Category, GetCategoryByIdResult>();
        }

        public class Handler : IRequestHandler<GetCategoryByIdCommand, Result<GetCategoryByIdResult>>
        {
            private readonly IMapper mapper;
            private readonly BudgetTrackerContext context;

            public Handler(IMapper mapper, BudgetTrackerContext context)
            {
                this.mapper = mapper;
                this.context = context;
            }

            public async Task<Result<GetCategoryByIdResult>> Handle(GetCategoryByIdCommand request, CancellationToken cancellationToken)
            {
                var category = await context.Categories.FindAsync(request.Id);

                return Result<GetCategoryByIdResult>.Success(mapper.Map<GetCategoryByIdResult>(category));
            }
        }
    }
}