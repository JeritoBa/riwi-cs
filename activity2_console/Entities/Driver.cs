namespace Exercise2.Entities;

public class Driver
{
    public int id { get; set; }
    public string fullName { get; set; }
    public string license { get; set; }
    public string state { get; set; }

    public override string ToString()
    {
        return $@" Vehicle ID {id}
                   Full Name: {fullName}
                   License: {license}
                   Actual State: {state}
";
    }
}