namespace Exercise3.DTOs;

using Exercise3.Enums;

public class AstronautRequest
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public AstronautRange Range { get; set; } = AstronautRange.Rookie;
    public int ExperienceHours { get; set; }
}