using application.Categories;
using domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static application.Categories.CreateCategory;
using static application.Categories.DeleteCategory;
using static application.Categories.GetAllCategories;
using static application.Categories.GetCategoryById;

namespace api.Controllers
{

    public class CategoryController : BaseApiController
    {
        private readonly IMediator mediator;

        public CategoryController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpGet]
        public async Task<IEnumerable<GetAllCategoriesResponse>> GetCategory()
        {
            var result = await mediator.Send(new GetAllCategories.GetAllCategoriesCommand());
            return result.Value;
        }

        [HttpGet("{id}")]
        public async Task<GetCategoryByIdResult> GetCategory(Guid id)
        {
            var result = await mediator.Send(new GetCategoryById.GetCategoryByIdCommand(id));
            return result.Value;
        }

        [HttpPost]
        public async Task<CreateCategoryResponse> CreateCategory(CreateCategory.CreateCategoryCommand command)
        {
            var result = (await mediator.Send(command)).Value;
            return result;
        }

        [HttpDelete("{id}")]
        public async Task DeleteCategory(Guid id)
        {
            var result = await (mediator.Send(new DeleteCategoryCommand(id)));
        }
    }
}
