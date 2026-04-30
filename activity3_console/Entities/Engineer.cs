namespace Exercise3.Entities;
using Exercise3.Enums;

public class Engineer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public EngineerSpeciality Speciality { get; set; }
    public int ExperienceYears { get; set; }

    public override string ToString()
    {
        return $@"Engineer ID #{Id}
                  Name: {Name} {LastName}
                  Speciality: {Speciality}
                  Experience: {ExperienceYears} years
";
    }
}