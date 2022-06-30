using api.Data;
using api.Models;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace api.Features.Categories
{
    public class CreateCategory
    {
        public class Command : IRequest<Category>
        {
            public string Name { get; set; }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile() => CreateMap<Command, Category>();
        }

        public class Handler : IRequestHandler<Command, Category>
        {
            private readonly IMapper mapper;
            private readonly BudgetTrackerContext context;

            public Handler(IMapper mapper, BudgetTrackerContext context)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Category> Handle(Command request, CancellationToken cancellationToken)
            {
                var category = mapper.Map<Category>(request);
                context.Categories.Add(category);

                await context.SaveChangesAsync();
                return category;
            }
        }
    }
}