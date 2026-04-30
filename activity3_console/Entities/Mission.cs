namespace Exercise3.Entities;

using Exercise3.Enums;

public class Mission
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime LaunchDate { get; set; } = DateTime.Now; // Has default value = now()
    public MissionState State { get; set; } = MissionState.Planned; // Has default value = planned
    public int AstronautId { get; set; } // FK
    public int SpaceshipId { get; set; } // FK
    // Entity Framework Needed Properties
    public Astronaut Astronaut { get; set; } // Navegation Object
    public Spaceship Spaceship { get; set; } // Navegation Object
    public List<ExplorationRegister> ExplorationRegister { get; set; } = new List<ExplorationRegister>(); // Navegation List

    public override string ToString()
    {
        return $@"Mission ID #{Id}
                Title: {Title}
                Launch Date: {LaunchDate}
                State: {State}
                Astronaut Name: {Astronaut.Name} {Astronaut.LastName}
                Spaceship Name: {Spaceship.Name}            
";
    }
}