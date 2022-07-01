using api.Features.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace api.Features.MonthlyBudgets
{
    public class MonthlyBudgetController : BaseApiController
    {
        private readonly IMediator mediator;

        public MonthlyBudgetController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMonthlyBudget(CreateMonthlyBudget.Command command)
        {
            var result = await mediator.Send(command);
            return HandleResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMonthlyBudgetById(Guid id)
        {
            var result = await mediator.Send(new GetMonthlyBudgetById.Command(id));
            return HandleResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetMonthlyBudgets()
        {
            var result = await mediator.Send(new GetMonthlyBudgets.Command());
            return HandleResult(result);
        }
    }
}