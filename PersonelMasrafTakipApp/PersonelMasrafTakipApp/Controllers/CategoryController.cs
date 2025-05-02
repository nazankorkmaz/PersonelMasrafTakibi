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
public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;
    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    //[Authorize(Roles = "Admin")]
    public async Task<ApiResponse<List<CategoryResponse>>> GetAll()
    {
        return await _mediator.Send(new GetAllCategoriesQuery());
    }

    [HttpGet("{id}")]
    //[Authorize(Roles = "Admin,Personel")]
    public async Task<ApiResponse<CategoryResponse>> GetById(long id)
    {
        return await _mediator.Send(new GetCategoryByIdQuery(id));
    }

    [HttpPost]
    //[Authorize(Roles = "Admin")]
    public async Task<ApiResponse<CategoryResponse>> Post([FromBody] CategoryRequest request)
    {
        return await _mediator.Send(new CreateCategoryCommand(request));
    }

    [HttpPut("{id}")]
    //[Authorize(Roles = "Admin")]
    public async Task<ApiResponse> Put(long id, [FromBody] CategoryRequest request)
    {
        return await _mediator.Send(new UpdateCategoryCommand(id, request));
    }

    [HttpDelete("{id}")]
    //[Authorize(Roles = "Admin")]
    public async Task<ApiResponse> Delete(long id)
    {
        return await _mediator.Send(new DeleteCategoryCommand(id));
    }
}
