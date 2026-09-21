using Microsoft.EntityFrameworkCore;
using Wallet.Api.Models;

namespace Wallet.Api.Data;

public class WalletDbContext : DbContext
{
    public WalletDbContext(DbContextOptions<WalletDbContext> options)
        : base(options)
    {
    }

    public DbSet<EmployeeCard> EmployeeCards => Set<EmployeeCard>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmployeeCard>()
            .ToTable("EmployeeCards");

        modelBuilder.Entity<EmployeeCard>()
            .HasKey(x => x.ID);
    }
}