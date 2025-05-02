using FluentValidation;
using MasrafTakip.Schema;

namespace PersonelMasrafTakipApp.Impl.Validation;

public class CategoryValidator : AbstractValidator<CategoryRequest>
{
    public CategoryValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Kategori adı boş olamaz.");
    }
}
