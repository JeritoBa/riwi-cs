namespace Exercise3.Entities;
using Exercise3.Enums;

public class ExplorationRegisterRequest
{
    public string PlanetDestination { get; set; }
    public string Description { get; set; }
    public ExplorationRegisterRiskLevel RiskLevel { get; set; }
    public int MissionId { get; set; }
}