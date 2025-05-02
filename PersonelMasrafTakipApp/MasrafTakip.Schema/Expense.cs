
using MasrafTakip.Base;
using MasrafTakip.Base.Enum;

namespace MasrafTakip.Schema;

public class ExpenseRequest : BaseRequest
{ 
    public long UserId { get; set; }
    public long CategoryId { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string PaymentMethod { get; set; }
    public string Location { get; set; }
    public string? RejectionReason { get; set; }
    public bool IsApproved { get; set; }
    public RequestStatus Status { get; set; }
}

public class ExpenseResponse : BaseResponse
{
    
    //public UserResponse? User { get; set; }
    //public CategoryResponse? Category { get; set; }
    public long UserId {get; set;}
    public string CategoryName {get; set;}
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string PaymentMethod { get; set; }
    public string Location { get; set; }
    public string? RejectionReason { get; set; }
    public bool IsApproved { get; set; }
    public RequestStatus Status { get; set; }


}

