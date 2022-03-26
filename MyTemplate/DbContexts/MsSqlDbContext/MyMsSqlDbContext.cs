using Microsoft.EntityFrameworkCore;
using MyTemplate.DbContexts.Models;

namespace MyTemplate.DbContexts.MsSqlDbContext;

public class MyMsSqlDbContext : DbContext
{
    //private readonly string _connectionString;
    public DbSet<DemoMessage> DemoMessages { get; set; }

    public MyMsSqlDbContext(DbContextOptions<MyMsSqlDbContext> dbContextOptions) : base(dbContextOptions)
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