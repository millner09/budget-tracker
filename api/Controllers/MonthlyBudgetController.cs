using application.MonthlyBudgets;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    public class MonthlyBudgetController : BaseApiController
    {
        private readonly IMediator mediator;

        public MonthlyBudgetController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMonthlyBudget(CreateMonthlyBudget.CreateMonthlyBudgetCommand command)
        {
            var result = await mediator.Send(command);
            return HandleResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMonthlyBudgetById(Guid id)
        {
            var result = await mediator.Send(new GetMonthlyBudgetById.GetMonthlyBudgetCommand(id));
            return HandleResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetMonthlyBudgets()
        {
            var result = await mediator.Send(new GetMonthlyBudgets.GetMonthlyBudgetsCommand());
            return HandleResult(result);
        }

        [HttpPost("{budgetId}/test/{transactionId}")]
        [Authorize]
        public async Task<IActionResult> Test(Guid budgetId, Guid transactionId, CreateMonthlyBudget.CreateMonthlyBudgetCommand command)
        {
            var res = $"Hello, world \n{budgetId}\n{transactionId}\n{command.StartingBalance}";
            return Ok(res);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var res = await mediator.Send(new DeleteMonthlyBudget.DeleteMonthlyBudgetCommand(id));

            return HandleResult(res);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateMonthlyBudget.UpdateMonthlyBudgetRequest request)
        {
            var result = await mediator.Send(new UpdateMonthlyBudget.Command(id, request));
            return HandleResult(result);
        }
    }
}