using AutoMapper;
using MediatR;
using PersonelMasrafTakipApp.Domain;
using PersonelMasrafTakipApp.Impl.Cqrs;
using MasrafTakip.Base;
using MasrafTakip.Schema;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using PersonelMasrafTakipApp;

namespace MasrafTakip.Api.Impl.Command
{
    public class ExpenseCommandHandler :
        IRequestHandler<CreateExpenseCommand, ApiResponse<ExpenseResponse>>,
        IRequestHandler<UpdateExpenseCommand, ApiResponse>,
        IRequestHandler<DeleteExpenseCommand, ApiResponse>
    {
        private readonly MsSqlDbContext dbContext;
        private readonly IMapper mapper;

        public ExpenseCommandHandler(MsSqlDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<ApiResponse> Handle(DeleteExpenseCommand request, CancellationToken cancellationToken)
        {
            var expense = await dbContext.Set<Expense>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (expense == null)
                return new ApiResponse("Expense not found");

            dbContext.Remove(expense);
            await dbContext.SaveChangesAsync(cancellationToken);
            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(UpdateExpenseCommand request, CancellationToken cancellationToken)
        {
            var expense = await dbContext.Set<Expense>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (expense == null)
                return new ApiResponse("Expense not found");

            expense.Amount = request.Expense.Amount;
            expense.Description = request.Expense.Description;
            expense.CategoryId = request.Expense.CategoryId;
            expense.UpdatedDate = DateTime.Now;
            
            expense.InsertedUser = "test";
            expense.UpdatedDate =DateTime.Now;
            expense.UpdatedUser = "test";

            await dbContext.SaveChangesAsync(cancellationToken);
            return new ApiResponse();
        }

        public async Task<ApiResponse<ExpenseResponse>> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
        {
            var mapped = mapper.Map<Expense>(request.Expense);
            mapped.InsertedDate = DateTime.Now;
            mapped.InsertedUser = "test";
            mapped.UpdatedDate =DateTime.Now;
            mapped.UpdatedUser = "test";
            
            var entity = await dbContext.AddAsync(mapped, cancellationToken);

            await dbContext.SaveChangesAsync(cancellationToken);
            var response = mapper.Map<ExpenseResponse>(entity.Entity);
            return new ApiResponse<ExpenseResponse>(response);
        }
    }
}
