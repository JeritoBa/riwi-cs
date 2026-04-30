namespace Exercise3.Services;

using Exercise3.Enums;
using Exercise3.DTOs;
using Exercise3.Repositories;
using Exercise3.Entities;

public class AstronautService
{
    private static Utils utils = new Utils();
    private static AstronautsRepository AstronautsRepository = new AstronautsRepository();

    private static MissionsRepository MissionsRepository = new MissionsRepository();

    // Getting values by switchs
    AstronautRange askRange()
    {
        Console.WriteLine("Select the range");
        Console.WriteLine("1. Rookie");
        Console.WriteLine("2. Pilot");
        Console.WriteLine("3. Commander");

        Console.Write("Type the number of the option: ");
        int rangeOption = int.Parse(Console.ReadLine());
        AstronautRange range;

        switch (rangeOption)
        {
            case 1:
                range = AstronautRange.Rookie;
                break;
            case 2:
                range = AstronautRange.Pilot;
                break;
            case 3:
                range = AstronautRange.Commander;
                break;
            default:
                Console.WriteLine("That option doesn't exists. Setting astronaut range as rookie by default.");
                range = AstronautRange.Rookie;
                
                break;
        }

        return range;
    }

    // CRUD Functions
    public void add()
    {
        try
        {
            utils.Separator();
            Console.WriteLine("---ADD ASTRONAUT---");

            // Requesting dates
            Console.Write("Type the name: ");
            string name = Console.ReadLine();

            Console.Write("Type the lastname: ");
            string lastname = Console.ReadLine();

            AstronautRange range = askRange();

            Console.Write("Type the experience hours: ");
            int experienceHours = int.Parse(Console.ReadLine());

            // Validating dates
            // Checking that that name and lastname doesn't exists
            if (name == "" || lastname == "")
            {
                Console.WriteLine("Name or lastname cannot be empty");
                return;
            }
            
            if (!utils.validateNumber(experienceHours))
            {
                return;
            } // Checking experience hours it's a positive number

            // Checking that that astronaut doesn't exists
            if (AstronautsRepository.exists(name, lastname))
            {
                Console.WriteLine("This astronaut is already registered.");
                return;
            }

            // Creating model request
            AstronautRequest newAstronaut = new AstronautRequest
                { Name = name, LastName = lastname, Range = range, ExperienceHours = experienceHours };

            // Adding into db
            bool response = AstronautsRepository.add(newAstronaut);

            // Printing result
            if (response)
            {
                Console.WriteLine("Astronaut added succesfully");
            }
            else
            {
                Console.WriteLine("Database Operation Failed ");
            }
        }
        catch (Exception err)
        {
            Console.WriteLine($"Error: {err.Message}");
            return;
        }
    }
    public List<Astronaut>? get()
    {
        try
        {
            utils.Separator();
            Console.WriteLine("---GETTING ASTRONAUTS---");
            
            // DB Query
            List<Astronaut> astronautsList = AstronautsRepository.get();

            // Checking if there isn't any match
            if(astronautsList.Count == 0) { Console.WriteLine("There isn't any astronaut registered."); return null; }
            
            // Printing result
            foreach (Astronaut astronaut in astronautsList) { Console.WriteLine(astronaut.ToString()); }
            
            // Returning response
            return astronautsList;
        }
        catch (Exception err)
        {
            Console.WriteLine($"Error: {err.Message}");

            return null;
        }
    }
    public Astronaut? update()
    {
        try
        {
            utils.Separator();
            Console.WriteLine("---UPDATE ASTRONAUT---");
            
            // Getting data
            Console.Write("Type the id of the astronaut you want update: ");
            int id = int.Parse(Console.ReadLine());

            // Checking that astronaut exists
            Astronaut astronautFound = AstronautsRepository.getById(id);

            if (astronautFound == null)
            {
                Console.WriteLine("That astronaut doesn't exists");
                return null;
            }

            // Asking data
            Console.Write("Type the new name: ");
            string name = Console.ReadLine();

            Console.Write("Type the new lastname: ");
            string lastname = Console.ReadLine();

            // Checking that that name and lastname doesn't exists
            if (AstronautsRepository.exists(name, lastname))
            {
                Console.WriteLine("That name and lastname is already registered");
                return null;
            }

            AstronautRange range = askRange();

            Console.Write("Type the hours of experience: ");
            int experienceHours = int.Parse(Console.ReadLine());
            
            if(experienceHours <= 0) { Console.WriteLine("Experience hours must be higher than zero."); return null; }

            // Creating astronaut request
            AstronautRequest newAstronaut = new AstronautRequest
                { Name = name, LastName = lastname, Range = range, ExperienceHours = experienceHours };

            // Checking data
            if (newAstronaut.Name == "" || newAstronaut.LastName == "")
            {
                Console.WriteLine("Name or lastname cannot be empty");
                return null;
            }

            if (newAstronaut.ExperienceHours <= 0)
            {
                Console.WriteLine("Experience hours must be higher than zero");
                return null;
            }

            // Updating astronaut
            bool response = AstronautsRepository.update(id, newAstronaut);

            // Checking response and printing result
            if (response)
            {
                Console.WriteLine("Astronaut updated");
                return astronautFound;
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
    public Astronaut? delete()
    {
        try
        {
            utils.Separator();
            Console.WriteLine("---DELETE ASTRONAUT---");
            
            // Asking ID to delete
            Console.Write("Type the ID of the astronaut: ");
            int id = int.Parse(Console.ReadLine());

            // Checking if it exists
            Astronaut astronautFound = AstronautsRepository.getById(id);
            
            if(astronautFound == null) { Console.WriteLine("This astronaut doesn't exists."); return null; }

            // Deleting astronaut
            bool response = AstronautsRepository.delete(id);

            // Printing result
            if (response)
            {
                Console.WriteLine("Astronaut deleted succesfully");
                return astronautFound;
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

    // Custom Entity Functions
    public List<Astronaut>? filterByRange()
    {
        try
        {
            utils.Separator();
            Console.WriteLine("--FILTER BY RANGE---");
            
            // Asking range
            AstronautRange range = askRange();

            // Making DB Query
            List<Astronaut> response = AstronautsRepository.getByRange(range);

            // Checking if there isn't any match
            if (response.Count == 0)
            {
                Console.WriteLine("There isn't any astronaut with that range.");
                return null;
            }

            // Printing result
            foreach (Astronaut astronaut in response) { Console.WriteLine(astronaut.ToString()); };

            return response;
        }
        catch (Exception err)
        {
            Console.WriteLine($"Error: {err.Message}");
            return null;
        }
    }
    public List<Astronaut>? filterByMissions()
    {
        try
        {
            utils.Separator();
            Console.WriteLine("---ASTRONAUTS WITH +3 MISSIONS---");
            
            // Making DB Query
            List<Astronaut> response = AstronautsRepository.getByMissions(3); // Get since three missions or higher per astronaut

            // Checking if there isn't at least one match
            if(response.Count == 0) { Console.WriteLine("There isn't any astronaut with 3 missions or more."); return null; }
            
            // Printing result
            foreach (Astronaut astronaut in response) { Console.WriteLine(astronaut.ToString()); }

            return response;
        }
        catch (Exception err)
        {
            Console.WriteLine($"Error: {err.Message}");
            return null;
        }
    }

    public int? countMissions()
    {
        try
        {
            utils.Separator();
            Console.WriteLine("---MISSIONS BY ASTRONAUT ID---");

            // Asking for astronaut id
            Console.Write("Type the ID of the astronaut: ");
            int astronautId = int.Parse(Console.ReadLine());

            // Checking it exists
            Astronaut astronautFound = AstronautsRepository.getById(astronautId);

            if(astronautFound == null) { Console.WriteLine("There isn't any astronaut with that id");
                return null;
            }
            
            // Making DB Query
            int response = MissionsRepository.countByAstronautId(astronautId);

            // Checking there is at least one match
            if(response == 0) { Console.WriteLine("This astronaut hasn't any mission"); return null; }

            // Printing result
            Console.WriteLine(astronautFound.ToString());
            Console.WriteLine($"Missions Registered: {response}");

            return response;
        }
        catch (Exception err)
        {
            Console.WriteLine($"Error: {err.Message}");
            return null;
        }
    }
}