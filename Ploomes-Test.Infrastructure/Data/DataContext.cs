using Microsoft.EntityFrameworkCore;
using Ploomes_Test.Domain;
using Ploomes_Test.Infrastructure.Data.TableDefinition;

namespace Ploomes_Test.Infrastructure.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Ticket> Tickets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ticket>().ToTable("Ticket");
        modelBuilder.ApplyConfiguration(new TicketConfiguration());
    }
}