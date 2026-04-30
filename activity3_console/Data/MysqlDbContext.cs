namespace Exercise3.Data;

using Microsoft.EntityFrameworkCore;

using Exercise3.Entities;
using Exercise3.Enums;

public class MysqlDbContext : DbContext
{
    public DbSet<Astronaut> Astronauts { get; set; }
    public DbSet<Engineer> Engineers { get; set; }
    public DbSet<ExplorationRegister> ExplorationRegisters { get; set; }
    public DbSet<Mission> Missions { get; set; }
    public DbSet<Spaceship> Spaceships { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseMySql(
        "server=204.168.220.65;database=jeronimo_gallego_exercise3;user=root;password=nebula123*",
        ServerVersion.AutoDetect("server=204.168.220.65;database=jeronimo_gallego_exercise3;user=root;password=nebula123*")
    );

    // DB Fields Configurations
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Check's
        modelBuilder.Entity<Astronaut>()
            .ToTable(t => t.HasCheckConstraint(
                "CK_Astronaut_Experience",
                "ExperienceHours > 0"
            ));
        
        modelBuilder.Entity<Engineer>()
            .ToTable(t => t.HasCheckConstraint(
                "CK_Engineer_Experience",
                "ExperienceYears > 0"
            ));

        modelBuilder.Entity<Spaceship>()
            .ToTable(t => t.HasCheckConstraint(
                "CK_Spaceship_Capacity",
                "Capacity > 0"
            ));
        
        // Enum String Conversion
        modelBuilder.Entity<Astronaut>()
            .Property(p => p.Range)
            .HasConversion<string>();
        
        modelBuilder.Entity<Engineer>()
            .Property(p => p.Speciality)
            .HasConversion<string>();
        
        modelBuilder.Entity<Spaceship>()
            .Property(p => p.State)
            .HasConversion<string>();

        modelBuilder.Entity<Mission>()
            .Property(p => p.State)
            .HasConversion<string>();

        modelBuilder.Entity<ExplorationRegister>()
            .Property(p => p.RiskLevel)
            .HasConversion<string>();
        
        // Default Values
        modelBuilder.Entity<Astronaut>()
            .Property(p => p.Range)
            .HasConversion<string>()
            .HasDefaultValue(AstronautRange.Rookie);
        
        modelBuilder.Entity<Spaceship>()
            .Property(p => p.State)
            .HasConversion<string>()
            .HasDefaultValue(SpaceshipState.Operative);
        
        modelBuilder.Entity<Mission>()
            .Property(p => p.LaunchDate)
            .HasDefaultValue(DateTime.Now);
        modelBuilder.Entity<Mission>()
            .Property(p => p.State)
            .HasDefaultValue(MissionState.Planned);
        
        modelBuilder.Entity<ExplorationRegister>()
            .Property(p => p.RiskLevel)
            .HasDefaultValue(ExplorationRegisterRiskLevel.Low);
        
        // Foreign keys
        modelBuilder.Entity<Mission>()
            .HasOne<Astronaut>(mission => mission.Astronaut) // Declaring EF Navegation Object
            .WithMany(astronaut => astronaut.Missions) // Declaring EF Navegation List
            .HasForeignKey(p => p.AstronautId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Mission>()
            .HasOne<Spaceship>(mission => mission.Spaceship) // Declaring EF Navegation Object
            .WithMany(spaceship => spaceship.Missions) // Declaring EF Navegation List
            .HasForeignKey(p => p.SpaceshipId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<ExplorationRegister>()
            .HasOne<Mission>(explorationRegister => explorationRegister.Mission) // Declaring EF Navegation Object
            .WithMany(mission => mission.ExplorationRegister) // Declaring EF Navegation List
            .HasForeignKey(p => p.MissionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}