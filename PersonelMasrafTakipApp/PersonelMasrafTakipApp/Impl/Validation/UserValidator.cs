using FluentValidation;
using MasrafTakip.Base;
using MasrafTakip.Schema;

namespace PersonelMasrafTakipApp.Impl.Validation;

public class UserValidator : AbstractValidator<UserRequest>
{
    public UserValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("Ad soyad boş bırakılamaz.");
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.IBAN).NotEmpty().Length(26).WithMessage("IBAN 26 karakter olmalıdır.");
        RuleFor(x => x.Role).IsInEnum().WithMessage("Geçersiz rol.");
    }
}
