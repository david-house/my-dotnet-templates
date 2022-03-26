using Microsoft.EntityFrameworkCore;
using MyTemplate.DbContexts.Models;

namespace MyTemplate.DbContexts.SqliteDbContext;

public class MySqliteDbContext : DbContext
{
    //private readonly string _connectionString;
    public DbSet<DemoMessage> DemoMessages { get; set; }

    public MySqliteDbContext(DbContextOptions<MySqliteDbContext> dbContextOptions) : base(dbContextOptions)
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DemoMessage>()
            .Property(d => d.DemoMessageId)
            .ValueGeneratedOnAdd()
            .IsRequired();

        modelBuilder.Entity<DemoMessage>()
            .HasKey(c => c.DemoMessageId);
    }
}

