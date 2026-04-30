namespace Exercise3.Entities;
using Exercise3.Enums;

public class Spaceship
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Model { get; set; }
    public int Capacity { get; set; }
    public SpaceshipState State { get; set; } = SpaceshipState.Operative; // Has default value = Operative
    public List<Mission> Missions { get; set; } = new List<Mission>(); // Navegation List

    public override string ToString()
    {
        return $@"Spaceship ID #{Id}
                  Name: {Name}
                  Model: {Model}
                  Capacity: {Capacity} persons
                  State: {State}
";
    }
}