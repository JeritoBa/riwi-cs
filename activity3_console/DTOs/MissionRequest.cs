namespace Exercise3.DTOs;

using Exercise3.Enums;

public class MissionRequest
{
    public string Title { get; set; }
    public DateTime LaunchDate { get; set; } = DateTime.Now;
    public MissionState State { get; set; } = MissionState.Planned;
    public int AstronautId { get; set; }
    public int SpaceshipId { get; set; }
}