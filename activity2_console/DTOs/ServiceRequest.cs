namespace Exercise2.DTOs;

public class ServiceRequest
{
    public int id { get; set; }
    public string origin { get; set; }
    public string destination { get; set; }
    public double distance { get; set; }
    public string state { get; set; } = "pending";
    public double total_cost { get; set; } = 0;
    public int driver_id { get; set; }
    public int vehicle_id { get; set; }
}