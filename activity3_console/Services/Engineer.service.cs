namespace Exercise3.Services;

using Exercise3.Enums;
using Exercise3.DTOs;
using Exercise3.Repositories;
using Exercise3.Entities;

public class EngineerService
{
    private static Utils utils = new Utils();
    private static EngineerRepository EngineersRepository = new EngineerRepository();
    
    // Getting values by switch
    EngineerSpeciality askSpeciality()
    {
        Console.WriteLine("Select the speciality");
        Console.WriteLine("1. Propulsion");
        Console.WriteLine("2. Systems");
        Console.WriteLine("3. IA");

        Console.Write("Type the number of the option: ");
        int specialityOption = int.Parse(Console.ReadLine());
        EngineerSpeciality speciality;

        switch (specialityOption)
        {
            case 1:
                speciality = EngineerSpeciality.Propulsion;
                break;
            case 2:
                speciality = EngineerSpeciality.Systems;
                break;
            case 3:
                speciality = EngineerSpeciality.IA;
                break;
            default:
                Console.WriteLine("That option doesn't exists. Setting astronaut speciality as propulsion by default.");
                speciality = EngineerSpeciality.Propulsion;
                break;
        }

        return speciality;
    }
    
    // CRUD Functions
    public void add()
    {
        try
        {
            utils.Separator();
            Console.WriteLine("---ADD A NEW ENGINEER---");
            
            // Requesting dates
            Console.Write("Type the name: ");
            string name = Console.ReadLine();

            Console.Write("Type the lastname: ");
            string lastname = Console.ReadLine();

            EngineerSpeciality speciality = askSpeciality();
            
            Console.Write("Type the experience years: ");
            int experienceYears = int.Parse(Console.ReadLine());

            // Validating dates
            if (name == "" || lastname == "")
            {
                Console.WriteLine("Name or lastname cannot be empty");
                return;
            }
            
            if (!utils.validateNumber(experienceYears))
            {
                return;
            } // Checking experience hours it's a positive number

            // Checking that that engineer doesn't exists
            if (EngineersRepository.exists(name, lastname))
            {
                Console.WriteLine("This engineer is already registered.");
                return;
            }

            // Creating model request
            EngineerRequest newEngineer = new EngineerRequest
                { Name = name, LastName = lastname, Speciality = speciality, ExperienceYears = experienceYears };

            // Adding it into db
            bool response = EngineersRepository.add(newEngineer);

            // Printing result
            if(response) { Console.WriteLine("Engineer added succesfully."); return; }
            
            Console.WriteLine("Error adding engineer.");
        }
        catch (Exception err)
        {
            Console.WriteLine($"Error: ${err.Message}");
        }
    }
    public List<Engineer>? get()
    {
        try
        {
            utils.Separator();
            Console.WriteLine("---GETTING ALL ENGINEERS---");
            
            // DB Query
            List<Engineer> engineersList = EngineersRepository.get();

            // Checking if there isn't any match
            if(engineersList.Count == 0) { Console.WriteLine("There isn't any engineer registered."); return null; }
            
            // Printing result
            foreach(Engineer engineer in engineersList) { Console.WriteLine(engineer.ToString()); }

            // Returning response
            return engineersList;
        }
        catch (Exception err)
        {
            Console.WriteLine($"Error: {err.Message}");
            return null;
        }
    }

    public Engineer? update()
    {
        try
        {
            utils.Separator();
            Console.WriteLine("---UPDATING ENGINEER---");
            
            // Getting data
            Console.Write("Type the id of the engineer you want update: ");
            int id = int.Parse(Console.ReadLine());
            
            // Checking if engineer exists
            Engineer engineerFound = EngineersRepository.getById(id);

            if (engineerFound == null)
            {
                Console.WriteLine("That engineer doesn't exists");
                return null;
            }
            
            // Asking new data
            Console.Write("Type the new name: ");
            string name = Console.ReadLine();

            Console.Write("Type the new lastname: ");
            string lastname = Console.ReadLine();

            // Checking that that name and lastname doesn't exists
            if (EngineersRepository.exists(name, lastname))
            {
                Console.WriteLine("That name and lastname is already registered");
                return null;
            }

            EngineerSpeciality speciality = askSpeciality();

            Console.Write("Type the years of experience: ");
            int experienceYears = int.Parse(Console.ReadLine());
            
            // Creating engineer request
            EngineerRequest newEngineer = new EngineerRequest
                { Name = name, LastName = lastname, Speciality = speciality, ExperienceYears = experienceYears };

            // Checking data
            if (newEngineer.Name == "" || newEngineer.LastName == "") { Console.WriteLine("Name or lastname cannot be empty"); return null; }
            if (newEngineer.ExperienceYears <= 0) { Console.WriteLine("Experience years must be higher than zero"); return null; }
            
            // Updating engineer
            bool response = EngineersRepository.update(id, newEngineer);

            // Checking response and printing result
            if(response) { Console.WriteLine("Engineer Updated"); return engineerFound; }
            
            Console.WriteLine("Operation failed");
            return null;
        }
        catch (Exception err)
        {
            Console.WriteLine($"Error: {err.Message}");
            return null;
        }
    }

    public Engineer? delete()
    {
        try
        {
            utils.Separator();
            Console.WriteLine("---DELETING ENGINEER---");
            
            // Asking ID to delete
            Console.Write("Type the ID of the engineer: ");
            int id = int.Parse(Console.ReadLine());
            
            // Checking if it exists
            Engineer engineerFound = EngineersRepository.getById(id);
            
            if(engineerFound == null) { Console.WriteLine("This engineer doesn't exists."); return null; }
            
            // Deleting engineer
            bool response = EngineersRepository.delete(id);

            // Printing result
            if (response)
            {
                Console.WriteLine("Engineer deleted succesfully");
                return engineerFound;
            }

            Console.WriteLine("Operation failed");
            return null;
        }
        catch (Exception err)
        {
            Console.WriteLine($"Error: {err.Message}");
            return null;
        }
    }
}