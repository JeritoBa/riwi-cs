namespace Exercise2.DTOs;

public class VehicleRequest
{
    public string plate { get; set; }
    public string type { get; set; }
    public int capacity { get; set; }
    public string state { get; set; } = "available";
}