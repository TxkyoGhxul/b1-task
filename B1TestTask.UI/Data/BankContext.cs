using B1TestTask.UI.Models;
using Microsoft.EntityFrameworkCore;

namespace B1TestTask.UI.Data;

internal sealed class BankContext : DbContext
{
    public BankContext(DbContextOptions<BankContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<InputFile> Files { get; set; }
    public DbSet<BankAccount> Accounts { get; set; }
    public DbSet<BankAccountClass> AccountClasses { get; set; }
    public DbSet<InputBalance> InputBalances { get; set; }
    public DbSet<OutputBalance> OutputBalances { get; set; }
    public DbSet<Turnover> Turnovers { get; set; }
}