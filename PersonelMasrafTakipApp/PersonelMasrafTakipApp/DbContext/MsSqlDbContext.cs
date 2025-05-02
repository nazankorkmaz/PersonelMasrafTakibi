using Microsoft.EntityFrameworkCore;
using PersonelMasrafTakipApp.Domain;
using MasrafTakip.Base;

namespace PersonelMasrafTakipApp;

public class MsSqlDbContext : DbContext
{
        public MsSqlDbContext(DbContextOptions<MsSqlDbContext> options) : base(options)
    {

    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MsSqlDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

}