namespace Exercise3.DTOs;
using Exercise3.Enums;

public class EngineerRequest
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public EngineerSpeciality Speciality { get; set; }
    public int ExperienceYears { get; set; }
}