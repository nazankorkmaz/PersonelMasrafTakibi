
using MasrafTakip.Base;

namespace MasrafTakip.Schema;

public class CategoryRequest : BaseRequest
{
    public string Name { get; set; }

}

public class CategoryResponse : BaseResponse
{
    public string Name { get; set; }
    public ICollection<ExpenseResponse> Expenses { get; set; }
}
