using MediatR;
using MasrafTakip.Base;
using MasrafTakip.Schema;

namespace PersonelMasrafTakipApp.Impl.Cqrs;


public record GetAllExpensesQuery() : IRequest<ApiResponse<List<ExpenseResponse>>>;
public record GetExpenseByIdQuery(long Id) : IRequest<ApiResponse<ExpenseResponse>>;
public record CreateExpenseCommand(ExpenseRequest Expense) : IRequest<ApiResponse<ExpenseResponse>>;
public record UpdateExpenseCommand(long Id, ExpenseRequest Expense) : IRequest<ApiResponse>;
public record DeleteExpenseCommand(long Id) : IRequest<ApiResponse>;

public record GetActiveExpensesQuery() : IRequest<ApiResponse<List<ExpenseResponse>>>;
public record GetPastExpensesQuery() : IRequest<ApiResponse<List<ExpenseResponse>>>;

