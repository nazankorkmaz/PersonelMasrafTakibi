using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MasrafTakip.Base;
using MasrafTakip.Schema;
using System.Threading.Tasks;
using PersonelMasrafTakipApp.Impl.Cqrs;

namespace PersonelMasrafTakipApp.Controllers;

[ApiController]
[Route("api/[controller]")]
//[Authorize]
public class ExpenseController : ControllerBase
{
    private readonly IMediator _mediator;
    public ExpenseController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
  //  [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<List<ExpenseResponse>>> GetAll()
    {
        return await _mediator.Send(new GetAllExpensesQuery());
    }

    [HttpGet("{id}")]
    //[Authorize(Roles = "Admin,Personel")]
    public async Task<ApiResponse<ExpenseResponse>> GetById(long id)
    {
        return await _mediator.Send(new GetExpenseByIdQuery(id));
    }

    [HttpPost]
    //[Authorize(Roles = "Personel")]
    public async Task<ApiResponse<ExpenseResponse>> Post([FromBody] ExpenseRequest request)
    {
        return await _mediator.Send(new CreateExpenseCommand(request));
    }

    [HttpPut("{id}")]
    //[Authorize(Roles = "Admin")]
    public async Task<ApiResponse> Put(long id, [FromBody] ExpenseRequest request)
    {
        return await _mediator.Send(new UpdateExpenseCommand(id, request));
    }

    [HttpDelete("{id}")]
    //[Authorize(Roles = "Admin")]
    public async Task<ApiResponse> Delete(long id)
    {
        return await _mediator.Send(new DeleteExpenseCommand(id));
    }

    [HttpGet("active")]
    //[Authorize(Roles = "Personel")]
    public async Task<ApiResponse<List<ExpenseResponse>>> GetActiveExpenses()
    {
        return await _mediator.Send(new GetActiveExpensesQuery());
    }

    [HttpGet("history")]
    //[Authorize(Roles = "Personel")]
    public async Task<ApiResponse<List<ExpenseResponse>>> GetPastExpenses()
    {
        return await _mediator.Send(new GetPastExpensesQuery());
    }
}
