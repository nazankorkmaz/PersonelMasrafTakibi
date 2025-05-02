using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using MasrafTakip.Base;
using MasrafTakip.Base.Enum;

namespace PersonelMasrafTakipApp.Domain;

[Table("Expense", Schema = "dbo")]
public class Expense : BaseEntity
{
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }

    public string PaymentMethod { get; set; }
    public string Location { get; set; }

    public RequestStatus Status { get; set; } = RequestStatus.Pending;
    public string? RejectionReason { get; set; }

    public long UserId { get; set; }
    public User User { get; set; }

    public long CategoryId { get; set; }
    public Category Category { get; set; }

    public bool IsApproved { get; set; }
}


public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
{
    public void Configure(EntityTypeBuilder<Expense> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn();

        builder.Property(x=> x.InsertedDate).IsRequired(true);
        builder.Property(x=> x.UpdatedDate).IsRequired(false);
        builder.Property(x=> x.InsertedUser).IsRequired(true).HasMaxLength(250);
        builder.Property(x=> x.UpdatedUser).IsRequired(false).HasMaxLength(250);
        builder.Property(x=> x.IsActive).IsRequired(true).HasDefaultValue(true);

        builder.Property(x => x.Description).IsRequired().HasMaxLength(300);
        builder.Property(x => x.Amount).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(x => x.Date).IsRequired();

        builder.Property(x => x.PaymentMethod).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Location).IsRequired().HasMaxLength(150);
        builder.Property(x => x.RejectionReason).HasMaxLength(300);
        builder.Property(x => x.IsApproved).HasDefaultValue(false);
        builder.Property(x => x.Status).HasConversion<string>();


        builder.HasOne(x => x.User)
            .WithMany(x => x.Expense)
            .HasForeignKey(x => x.UserId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Category)
            .WithMany(x => x.Expense)
            .HasForeignKey(x => x.CategoryId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);
    }
}