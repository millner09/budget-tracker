using application.Categories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{

    public class CategoryController : BaseApiController
    {
        private readonly IMediator mediator;

        public CategoryController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategory.CreateCategoryCommand createCommand)
        {
            var result = await mediator.Send(createCommand);
            return HandleResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await mediator.Send(new GetAllCategories.GetAllCategoriesCommand());
            return HandleResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAll(Guid id)
        {
            var result = await mediator.Send(new GetCategoryById.GetCategoryByIdCommand(id));
            return HandleResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await mediator.Send(new DeleteCategory.DeleteCategoryCommand(id));
            return HandleResult(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateCategory.UpdateCategoryRequest request)
        {
            var result = await mediator.Send(new UpdateCategory.Command(id, request));
            return HandleResult(result);
        }
    }
}