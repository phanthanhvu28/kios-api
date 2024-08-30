using ApplicationCore.Contracts.Common;
using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class MainDbContext : AppDbContextBase
{
    public MainDbContext(DbContextOptions options) : base(options)
    {
        //Database.EnsureCreated();
    }

    public DbSet<Companies> Companies { get; set; }
    public DbSet<Stores> Stores { get; set; }
    public DbSet<Areas> Areas { get; set; }
    public DbSet<Tables> Tables { get; set; }
    public DbSet<Orders> Orders { get; set; }
    public DbSet<OrderDetails> OrderDetails { get; set; }
    public DbSet<TypeSales> TypeSales { get; set; }
    public DbSet<TypeBida> TypeBida { get; set; }
    public DbSet<Staffs> Staffs { get; set; }
    public DbSet<AuthenUser> AuthenUser { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Products> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // TODO: define model later
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MainDbContext).Assembly);
    }
}