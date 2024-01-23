using B1TestTask.UI.Models;
using Microsoft.EntityFrameworkCore;

namespace B1TestTask.UI.Data;
internal sealed class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<RandomRowData> Rows { get; set; }
}
