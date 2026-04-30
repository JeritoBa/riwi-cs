namespace Exercise2.Entities;

public class Service
{
    public int id { get; set; }
    public string origin { get; set; }
    public string destination { get; set; }
    public double distance { get; set; }
    public string state { get; set; }
    public double total_cost { get; set; }
    public int vehicle_id { get; set; }
    public int driver_id { get; set; }

    public override string ToString()
    {
        return $@" Service ID {id}
            Origin: {origin}
            Destination: {destination}
            Distance: {distance}
            State: {state}
            Total Cost: {total_cost}
            Vehicle ID: {vehicle_id}
            Driver ID: {driver_id}
        ";
    }
}