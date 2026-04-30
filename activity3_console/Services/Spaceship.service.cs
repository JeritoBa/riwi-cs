namespace Exercise3.Services;

using Exercise3.Enums;
using Exercise3.DTOs;
using Exercise3.Repositories;
using Exercise3.Entities;

public class SpaceshipService
{
    private static Utils utils = new Utils();
    private static SpaceshipRepository SpaceshipsRepository = new SpaceshipRepository();

    // Getting values by switchs
    SpaceshipState askState()
    {
        Console.WriteLine("Select the state");
        Console.WriteLine("1. Operative");
        Console.WriteLine("2. In Maintenance");
        Console.WriteLine("3. Retired");

        Console.Write("Type the number of the option: ");
        int stateOption = int.Parse(Console.ReadLine());
        SpaceshipState state;

        switch (stateOption)
        {
            case 1:
                state = SpaceshipState.Operative;
                break;
            case 2:
                state = SpaceshipState.Maintenance;
                break;
            case 3:
                state = SpaceshipState.Retired;
                break;
            default:
                Console.WriteLine("That option doesn't exists. Setting astronaut state as operative by default.");
                state = SpaceshipState.Operative;
                
                break;
        }

        return state;
    }
    
    public void add()
    {
        try
        {
            utils.Separator();
            Console.WriteLine("---ADD SPACESHIP---");

            // Requesting dates
            Console.Write("Type the name: ");
            string name = Console.ReadLine();

            Console.Write("Type the model: ");
            string model = Console.ReadLine();

            SpaceshipState state = askState();

            Console.Write("Type the capacity of persons: ");
            int capacity = int.Parse(Console.ReadLine());

            // Validating dates
            // Checking if name and model are empty
            if (name == "" || model == "") { Console.WriteLine("Name or model cannot be empty"); return; }
            
            // Checking experience hours it's a positive number
            if (!utils.validateNumber(capacity)) { Console.WriteLine("Capacity of persons must be higher than zero"); return; }

            // Checking that that spaceship name and model doesn't exists
            if (SpaceshipsRepository.exists(name, model)) { Console.WriteLine("This spaceship is already registered."); return; }

            // Creating model request
            SpaceshipRequest newSpaceship = new SpaceshipRequest
                { Name = name, Model = model, Capacity = capacity, State = state };

            // Adding into db
            bool response = SpaceshipsRepository.add(newSpaceship);

            // Printing result
            if (response) { Console.WriteLine("Spaceship added succesfully"); }
            else { Console.WriteLine("Database Operation Failed "); }
        }
        catch (Exception err)
        {
            Console.WriteLine($"Error: {err.Message}");
            return;
        }
    }
    public List<Spaceship>? get()
    {
        try
        {
            utils.Separator();
            Console.WriteLine("---GETTING SPACESHIPS---");
            
            // DB Query
            List<Spaceship> spaceshipsList = SpaceshipsRepository.get();

            // Checking if there isn't any match
            if(spaceshipsList.Count == 0) { Console.WriteLine("There isn't any spaceship registered."); return null; }
            
            // Printing result
            foreach (Spaceship spaceship in spaceshipsList) { Console.WriteLine(spaceship.ToString()); }
            
            // Returning response
            return spaceshipsList;
        }
        catch (Exception err)
        {
            Console.WriteLine($"Error: {err.Message}");

            return null;
        }
    }
    public Spaceship? update()
    {
        try
        {
            utils.Separator();
            Console.WriteLine("---UPDATE SPACESHIP---");
            
            // Getting data
            Console.Write("Type the id of the spaceship you want update: ");
            int id = int.Parse(Console.ReadLine());

            // Checking that spaceship exists
            Spaceship spaceshipFound = SpaceshipsRepository.getById(id);

            if (spaceshipFound == null)
            {
                Console.WriteLine("That spaceship doesn't exists");
                return null;
            }

            // Asking data
            Console.Write("Type the new name: ");
            string name = Console.ReadLine();

            Console.Write("Type the new model: ");
            string model = Console.ReadLine();

            // Checking that that name and model doesn't exists
            if (SpaceshipsRepository.exists(name, model))
            {
                Console.WriteLine("That name and model is already registered");
                return null;
            }

            SpaceshipState state = askState();

            Console.Write("Type the capacity of persons: ");
            int capacity = int.Parse(Console.ReadLine());

            // Creating spaceship request
            SpaceshipRequest newSpaceship = new SpaceshipRequest
                { Name = name, State = state, Capacity = capacity };

            // Checking data
            if (newSpaceship.Name == "" || newSpaceship.Model == "") { Console.WriteLine("Name or model cannot be empty"); return null; }
            if (newSpaceship.Capacity <= 0) { Console.WriteLine("Capacity must be higher than zero"); return null; }

            // Updating spaceship
            bool response = SpaceshipsRepository.update(id, newSpaceship);

            // Checking response and printing result
            if (response) { Console.WriteLine("Spaceship updated"); return spaceshipFound; }

            Console.WriteLine("Operation failed");
            return null;
        }
        catch (Exception err)
        {
            Console.WriteLine($"Error: {err.Message}");
            return null;
        }
    }
    public Spaceship? delete()
    {
        try
        {
            utils.Separator();
            Console.WriteLine("---DELETE SPACESHIP---");

            // Asking ID to delete
            Console.Write("Type the ID of the spaceship: ");
            int id = int.Parse(Console.ReadLine());

            // Checking if it exists
            Spaceship spaceshipFound = SpaceshipsRepository.getById(id);

            if (spaceshipFound == null)
            {
                Console.WriteLine("This spaceship doesn't exists.");
                return null;
            }

            // Deleting spaceship
            bool response = SpaceshipsRepository.delete(id);

            // Printing result
            if (response)
            {
                Console.WriteLine("Spaceship deleted succesfully");
                return spaceshipFound;
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
    public List<Spaceship>? filterByState()
    {
        try
        {
            utils.Separator();
            Console.WriteLine("---FILTER BY STATE---");
            
            // Asking state
            SpaceshipState state = askState();
            
            // Making DB Query
            List<Spaceship> response = SpaceshipsRepository.getByState(state);
            
            // Checking if there isn't any match
            if(response.Count == 0) { Console.WriteLine("There isn't any spaceship with that state. "); return null; }
            
            // Printing result
            foreach (Spaceship spaceship in response) { Console.WriteLine(spaceship.ToString()); }

            return response;
        }
        catch (Exception err)
        {
            Console.WriteLine($"Error: {err.Message}");
            return null;
        }
    }

    public List<Spaceship>? filterByNonUsed()
    {
        try
        {
            utils.Separator();
            Console.WriteLine("---NON USED SPACESHIPS---");
            
            // Making DB Query
            List<Spaceship> response = SpaceshipsRepository.filterByNonUsed();

            // Checking if there is any result
            if(response.Count == 0) { Console.WriteLine("There isn't any spaceship non used"); return null; }

            // Printing result
            foreach(Spaceship spaceship in response) { Console.WriteLine(spaceship.ToString()); }

            return response;
        }
        catch (Exception err)
        {
            Console.WriteLine($"Error: {err.Message}");
            return null;
        }
    }
}