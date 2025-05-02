using AutoMapper;
using MediatR;
using PersonelMasrafTakipApp.Domain;
using PersonelMasrafTakipApp.Impl.Cqrs;
using MasrafTakip.Base;
using MasrafTakip.Schema;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;

namespace PersonelMasrafTakipApp.Impl.Command
{
    public class UserCommandHandler :
        IRequestHandler<CreateUserCommand, ApiResponse<UserResponse>>,
        IRequestHandler<UpdateUserCommand, ApiResponse>,
        IRequestHandler<DeleteUserCommand, ApiResponse>
    {
        private readonly MsSqlDbContext dbContext;
        private readonly IMapper mapper;

        public UserCommandHandler(MsSqlDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<ApiResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await dbContext.Set<User>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (user == null)
                return new ApiResponse("User not found");

            user.IsActive = false;

            await dbContext.SaveChangesAsync(cancellationToken);
            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await dbContext.Set<User>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (user == null)
                return new ApiResponse("User not found");

            user.FirstName = request.User.FirstName;
            user.MiddleName = request.User.MiddleName;
            user.LastName = request.User.LastName;
            user.Email = request.User.Email;
            user.IBAN = request.User.IBAN;
            user.PasswordHash = request.User.PasswordHash;
            user.Role = request.User.Role;


            user.InsertedUser = "test";
            user.UpdatedDate =DateTime.Now;
            user.UpdatedUser = "test";
            await dbContext.SaveChangesAsync(cancellationToken);
            return new ApiResponse();
        }

        public async Task<ApiResponse<UserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var mapped = mapper.Map<User>(request.User);
            mapped.InsertedDate = DateTime.Now;
            mapped.IsActive = true;
            mapped.InsertedUser = "test";
            mapped.UpdatedDate =DateTime.Now;
            mapped.UpdatedUser = "test";

            var entity = await dbContext.AddAsync(mapped, cancellationToken);

            await dbContext.SaveChangesAsync(cancellationToken);
            var response = mapper.Map<UserResponse>(entity.Entity);
            return new ApiResponse<UserResponse>(response);
        }
    }
}
