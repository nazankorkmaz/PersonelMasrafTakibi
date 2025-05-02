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
   public class CategoryQueryHandler :
        IRequestHandler<GetAllCategoriesQuery, ApiResponse<List<CategoryResponse>>>,
        IRequestHandler<GetCategoryByIdQuery, ApiResponse<CategoryResponse>>
    {
        private readonly MsSqlDbContext context;
        private readonly IMapper mapper;

        public CategoryQueryHandler(MsSqlDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<List<CategoryResponse>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await context.Set<Category>().ToListAsync(cancellationToken);
            var mapped = mapper.Map<List<CategoryResponse>>(categories);
            return new ApiResponse<List<CategoryResponse>>(mapped);
        }

        public async Task<ApiResponse<CategoryResponse>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await context.Set<Category>()
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            var mapped = mapper.Map<CategoryResponse>(category);
            return new ApiResponse<CategoryResponse>(mapped);
        }
    }
}