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
    public class UpdateCategory
    {
        public class UpdateCategoryResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }

        public class UpdateCategoryRequest
        {
            public string Name { get; set; }
        }

        public class Command : IRequest<Result<UpdateCategoryResponse>>
        {
            public Command(Guid id, UpdateCategoryRequest request)
            {
                Id = id;
                Request = request;
            }

            public Guid Id { get; }
            public UpdateCategoryRequest Request { get; set; }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Category, UpdateCategoryResponse>();
                CreateMap<UpdateCategoryRequest, Category>();
            }
        }

        public class Handler : IRequestHandler<Command, Result<UpdateCategoryResponse>>
        {
            private readonly IMapper mapper;
            private readonly BudgetTrackerContext context;

            public Handler(IMapper mapper, BudgetTrackerContext context)
            {
                this.mapper = mapper;
                this.context = context;
            }

            public async Task<Result<UpdateCategoryResponse>> Handle(Command request, CancellationToken cancellationToken)
            {
                var category = await context.Categories.FindAsync(request.Id);
                if (category == null)
                    return null;

                mapper.Map(request.Request, category);

                var result = await context.SaveChangesAsync() > 0;

                if (!result)
                    return Result<UpdateCategoryResponse>.Failure("Failed to update record");

                return Result<UpdateCategoryResponse>.Success(mapper.Map<UpdateCategoryResponse>(category));
            }
        }
    }
}