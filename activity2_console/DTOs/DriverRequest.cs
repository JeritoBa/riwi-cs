namespace Exercise2.DTOs;

public class DriverRequest
{
    public string fullname { get; set; }
    public string license { get; set; }
    public string state { get; set; } = "available";
}