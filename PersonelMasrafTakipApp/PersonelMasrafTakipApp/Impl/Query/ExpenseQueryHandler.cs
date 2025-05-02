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
    public class ExpenseQueryHandler :
        IRequestHandler<GetAllExpensesQuery, ApiResponse<List<ExpenseResponse>>>,
        IRequestHandler<GetExpenseByIdQuery, ApiResponse<ExpenseResponse>>
    {
        private readonly MsSqlDbContext context;
        private readonly IMapper mapper;

        public ExpenseQueryHandler(MsSqlDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<List<ExpenseResponse>>> Handle(GetAllExpensesQuery request, CancellationToken cancellationToken)
        {
            var expenses = await context.Set<Expense>()
                .Include(x => x.Category)
                .ToListAsync(cancellationToken);
            var mapped = mapper.Map<List<ExpenseResponse>>(expenses);
            return new ApiResponse<List<ExpenseResponse>>(mapped);
        }

        public async Task<ApiResponse<ExpenseResponse>> Handle(GetExpenseByIdQuery request, CancellationToken cancellationToken)
        {
            var expense = await context.Set<Expense>()
                .Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            var mapped = mapper.Map<ExpenseResponse>(expense);
            return new ApiResponse<ExpenseResponse>(mapped);
        }
    }
}