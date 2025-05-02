using Microsoft.EntityFrameworkCore;
using PersonelMasrafTakipApp.Domain;
using MasrafTakip.Base;

namespace PersonelMasrafTakipApp;

public class MsSqlDbContext : DbContext
{
        public MsSqlDbContext(DbContextOptions<MsSqlDbContext> options) : base(options)
    {

    }

    public DbSet<User> User { get; set; }
    public DbSet<Expense> Expense { get; set; }
    public DbSet<Category> Categorie { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MsSqlDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

}