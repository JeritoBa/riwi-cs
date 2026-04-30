namespace Exercise2.Entities;

public class Vehicle
{
    public int id { get; set; }
    public string plate { get; set; }
    public string type { get; set; }
    public int capacity { get; set; }
    public string state { get; set; }

    public override string ToString()
    {
        return $@" Vehicle ID {id}
                   Plate: {plate}
                   Type: {type}
                   Capacity: {capacity}
                   Actual State: {state}
";
    }
}