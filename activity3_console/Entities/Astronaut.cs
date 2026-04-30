namespace Exercise3.Entities;
using Exercise3.Enums;

public class Astronaut
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public AstronautRange Range { get; set; } = AstronautRange.Rookie;
    public int ExperienceHours { get; set; }
    public List<Mission> Missions { get; set; } = new List<Mission>(); // Navegation List

    public override string ToString()
    {
        return $@"Astronaut ID #{Id}
                  Name: {Name} {LastName}
                  Range: {Range}
                  Experience: {ExperienceHours} hours                
";
    }
}