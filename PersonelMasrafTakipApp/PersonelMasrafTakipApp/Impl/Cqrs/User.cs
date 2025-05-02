using MediatR;
using MasrafTakip.Base;
using MasrafTakip.Schema;

namespace PersonelMasrafTakipApp.Impl.Cqrs;


public record GetAllUsersQuery() : IRequest<ApiResponse<List<UserResponse>>>;
public record GetUserByIdQuery(long Id) : IRequest<ApiResponse<UserResponse>>;
public record CreateUserCommand(UserRequest User) : IRequest<ApiResponse<UserResponse>>;
public record UpdateUserCommand(long Id, UserRequest User) : IRequest<ApiResponse>;
public record DeleteUserCommand(long Id) : IRequest<ApiResponse>;
