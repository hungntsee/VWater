using Microsoft.EntityFrameworkCore;
using Repository.Entity;

public class DBContext : DbContext
{
    protected readonly IConfiguration Configuration;
    public DBContext (IConfiguration configuration)
    {
      Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(Configuration.GetConnectionString("VWaterDatabase"));
        
    }
    public DbSet<Account> Account { get; set; }
    public DbSet<Account_Role> Account_Role { get; set; }
    
    public DbSet<Goods> Goods { get; set; }
    public DbSet<Brand> Brand { get; set; }
    public DbSet<Manufacturer> Manufacturer { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>().HasOne(a => a.Role).WithMany(r => r.Accounts).HasForeignKey(a => a.Role_Id);
        modelBuilder.Entity<Goods>().HasOne(a => a.BrandVirtutal).WithMany(r => r.Goodss).HasForeignKey(a => a.Brand_Id);
        modelBuilder.Entity<Brand>().HasOne(a => a.ManufacturerVirtual).WithMany(r => r.Brands).HasForeignKey(a => a.Manufacturer_Id);
    }
}
