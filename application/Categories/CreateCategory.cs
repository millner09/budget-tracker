using persistance;
using AutoMapper;
using domain;
using MediatR;
using application.Core;

namespace application.Categories
{
    public class CreateCategory
    {
        public class CreateCategoryCommand : IRequest<Result<CreateCategoryResponse>>
        {
            public string Name { get; set; }
        }

        public class CreateCategoryResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<CreateCategoryCommand, Category>();
                CreateMap<Category, CreateCategoryResponse>();
            }
        }

        public class Handler : IRequestHandler<CreateCategoryCommand, Result<CreateCategoryResponse>>
        {
            private readonly IMapper mapper;
            private readonly BudgetTrackerContext context;

            public Handler(IMapper mapper, BudgetTrackerContext context)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Result<CreateCategoryResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
            {
                var category = mapper.Map<Category>(request);
                context.Categories.Add(category);

                var result = await context.SaveChangesAsync() > 0;

                if (!result)
                    return Result<CreateCategoryResponse>.Failure("Failed to create category");


                return Result<CreateCategoryResponse>.Success(mapper.Map<CreateCategoryResponse>(category));
            }
        }
    }
}