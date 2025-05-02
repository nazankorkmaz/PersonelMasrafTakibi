using MediatR;
using MasrafTakip.Base;
using MasrafTakip.Schema;

namespace PersonelMasrafTakipApp.Impl.Cqrs;

public record GetAllCategoriesQuery() : IRequest<ApiResponse<List<CategoryResponse>>>;
public record GetCategoryByIdQuery(long Id) : IRequest<ApiResponse<CategoryResponse>>;
public record CreateCategoryCommand(CategoryRequest Category) : IRequest<ApiResponse<CategoryResponse>>;
public record UpdateCategoryCommand(long Id, CategoryRequest Category) : IRequest<ApiResponse>;
public record DeleteCategoryCommand(long Id) : IRequest<ApiResponse>;
