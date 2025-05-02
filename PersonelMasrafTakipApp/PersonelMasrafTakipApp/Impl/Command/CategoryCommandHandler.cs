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
    public class CategoryCommandHandler :
        IRequestHandler<CreateCategoryCommand, ApiResponse<CategoryResponse>>,
        IRequestHandler<UpdateCategoryCommand, ApiResponse>,
        IRequestHandler<DeleteCategoryCommand, ApiResponse>
    {
        private readonly MsSqlDbContext dbContext;
        private readonly IMapper mapper;

        public CategoryCommandHandler(MsSqlDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<ApiResponse> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await dbContext.Set<Category>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (category == null)
                return new ApiResponse("Category not found");

            dbContext.Remove(category);
            await dbContext.SaveChangesAsync(cancellationToken);
            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await dbContext.Set<Category>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (category == null)
                return new ApiResponse("Category not found");
            
            category.Name = request.Category.Name;
            category.InsertedUser = "test";

            category.UpdatedDate =DateTime.Now;
            category.UpdatedUser = "test";
            //category.InsertedUser = currentUserId;  // token ile sonradan

            //category.Description = request.Category.Description;

            await dbContext.SaveChangesAsync(cancellationToken);
            return new ApiResponse();
        }



        public async Task<ApiResponse<CategoryResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var mapped = mapper.Map<Category>(request.Category);

            mapped.InsertedUser = "test";
            mapped.UpdatedDate =DateTime.Now;
            mapped.UpdatedUser = "test";

            var entity = await dbContext.AddAsync(mapped, cancellationToken);

            await dbContext.SaveChangesAsync(cancellationToken);
            var response = mapper.Map<CategoryResponse>(entity.Entity);
            return new ApiResponse<CategoryResponse>(response);
        }
    }
}
