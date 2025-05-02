using AutoMapper;
using LinqKit;
using MediatR;
using PersonelMasrafTakipApp.Domain;
using PersonelMasrafTakipApp.Impl.Cqrs;
using MasrafTakip.Base;
using MasrafTakip.Schema;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PersonelMasrafTakipApp.Impl.Query
{
     public class UserQueryHandler :
        IRequestHandler<GetAllUsersQuery, ApiResponse<List<UserResponse>>>,
        IRequestHandler<GetUserByIdQuery, ApiResponse<UserResponse>>
    {
        private readonly MsSqlDbContext context;
        private readonly IMapper mapper;

        public UserQueryHandler(MsSqlDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<List<UserResponse>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await context.Set<User>().Include(u => u.Expense).ToListAsync(cancellationToken);
            var mapped = mapper.Map<List<UserResponse>>(users);
            return new ApiResponse<List<UserResponse>>(mapped);
        }

        public async Task<ApiResponse<UserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await context.Set<User>().Include(u => u.Expense) // expense listesi gelsin
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            var mapped = mapper.Map<UserResponse>(user);
            return new ApiResponse<UserResponse>(mapped);
        }
    }
}