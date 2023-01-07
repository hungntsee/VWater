using Microsoft.EntityFrameworkCore;
using Repository.Entity;
/*using System.Data.Entity;*/

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
    DbSet<Account> Account { get; set; }
    DbSet<Account_Role> Account_Role { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>().HasOne(a => a.Role).WithMany(r => r.Accounts).HasForeignKey(a => a.Role_Id);
    }
}
