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

        [HttpPost("{budgetId}/test/{transactionId}")]
        public async Task<IActionResult> Test(Guid budgetId, Guid transactionId, CreateMonthlyBudget.Command command)
        {
            var res = $"Hello, world \n{budgetId}\n{transactionId}\n{command.StartingBalance}";
            return Ok(res);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var res = await mediator.Send(new DeleteMonthlyBudget.Command(id));

            return HandleResult(res);
        }
    }
}