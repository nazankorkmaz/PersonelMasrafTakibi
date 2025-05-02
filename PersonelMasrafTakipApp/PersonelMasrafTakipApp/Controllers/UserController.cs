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
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
  //  [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<List<UserResponse>>> GetAll()
    {
        return await _mediator.Send(new GetAllUsersQuery());
    }

    [HttpGet("{id}")]
    //[Authorize(Roles = "Admin,Personel")]
    public async Task<ApiResponse<UserResponse>> GetById(long id)
    {
        return await _mediator.Send(new GetUserByIdQuery(id));
    }

    [HttpPost]
    //[AllowAnonymous]
    public async Task<ApiResponse<UserResponse>> Post([FromBody] UserRequest request)
    {
        return await _mediator.Send(new CreateUserCommand(request));
    }

    [HttpPut("{id}")]
    //[Authorize(Roles = "Admin")]
    public async Task<ApiResponse> Put(long id, [FromBody] UserRequest request)
    {
        return await _mediator.Send(new UpdateUserCommand(id, request));
    }

    [HttpDelete("{id}")]
    //[Authorize(Roles = "Admin")]
    public async Task<ApiResponse> Delete(long id)
    {
        return await _mediator.Send(new DeleteUserCommand(id));
    }
}
