namespace Exercise3.Entities;

using Exercise3.Enums;

public class ExplorationRegister
{
    public int Id { get; set; }
    public string PlanetDestination { get; set; }
    public string Description { get; set; }
    public ExplorationRegisterRiskLevel RiskLevel { get; set; } = ExplorationRegisterRiskLevel.Low;
    public int MissionId { get; set; }
    public Mission Mission { get; set; } // Navegation Object

    public override string ToString()
    {
        return $@"Exploration Register ID #{Id}
                  Planet Destination: {PlanetDestination}
                  Description: {Description}
                  Risk Level: {RiskLevel}
                  Mission Title: {Mission.Title}
";
    }
}