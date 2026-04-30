using Exercise3.Enums;

namespace Exercise3.DTOs;

public class SpaceshipRequest
{
    public string Name { get; set; }
    public string Model { get; set; }
    public int Capacity { get; set; }
    public SpaceshipState State { get; set; } = SpaceshipState.Operative;
}