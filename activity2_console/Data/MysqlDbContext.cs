namespace Exercise2.Data;

using Microsoft.EntityFrameworkCore;
using Exercise2.Entities;

public class MysqlDbContext : DbContext
{
    public DbSet<Driver> drivers { get; set; }
    public DbSet<Vehicle> vehicles { get; set; }
    public DbSet<Service> transport_service { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseMySql(
        "server=204.168.220.65;database=jeronimo_gallego_exercise2;user=root;password=nebula123*",
        ServerVersion.AutoDetect("server=204.168.220.65;database=jeronimo_gallego_exercise2;user=root;password=nebula123*")
    );
}