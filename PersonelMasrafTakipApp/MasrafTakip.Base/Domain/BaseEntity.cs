namespace MasrafTakip.Base;

public abstract class BaseEntity
{
    public long Id { get; set; }
    public bool IsActive { get; set; } = true;
    public string? InsertedUser { get; set; }
    public DateTime InsertedDate { get; set; } = DateTime.Now;
    public string? UpdatedUser { get; set; }
    public DateTime? UpdatedDate { get; set; }
}
