using MasrafTakip.Base;
using MasrafTakip.Base.Enum;
using MasrafTakip.Schema;

namespace MasrafTakip.Base;

public class UserRequest : BaseRequest
{
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string IBAN { get; set; }
    public string PasswordHash { get; set; }
    public UserRole Role { get; set; }
}

public class UserResponse : BaseResponse
{
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string IBAN { get; set; }   
    public UserRole Role { get; set; }
    public List<ExpenseResponse>? Expenses { get; set; } 
}