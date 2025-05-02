using FluentValidation;
using MasrafTakip.Schema;

namespace PersonelMasrafTakipApp.Impl.Validation;

public class ExpenseValidator : AbstractValidator<ExpenseRequest>
{
    public ExpenseValidator()
    {
        RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Tutar sıfırdan büyük olmalıdır.");
        RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Kategori seçilmelidir.");
        RuleFor(x => x.PaymentMethod).NotEmpty().WithMessage("Ödeme yöntemi boş bırakılamaz.");
        RuleFor(x => x.Location).NotEmpty().WithMessage("Konum bilgisi zorunludur.");
    }
}
