namespace Exercise3.Services;

using Exercise3.Enums;
using Exercise3.DTOs;
using Exercise3.Repositories;
using Exercise3.Entities;

public class MissionService
{
    private static Utils utils = new Utils();
    private static MissionsRepository MissionsRepository = new MissionsRepository();

    private static AstronautsRepository AstronautsRepository = new AstronautsRepository();
    private static SpaceshipRepository SpaceshipRepository = new SpaceshipRepository();
    
    private static ExplorationRegisterService ExplorationRegisterService = new ExplorationRegisterService();
    
    // Getting values by switchs
    MissionState askState()
    {
        Console.WriteLine("Select the state");
        Console.WriteLine("1. Planned");
        Console.WriteLine("2. In Course");
        Console.WriteLine("3. Completed");
        Console.WriteLine("4. Failed");

        Console.Write("Type the number of the option: ");
        int stateOption = int.Parse(Console.ReadLine());
        MissionState state;

        switch (stateOption)
        {
            case 1:
                state = MissionState.Planned;
                break;
            case 2:
                state = MissionState.Course;
                break;
            case 3:
                state = MissionState.Completed;
                break;
            case 4:
                state = MissionState.Failed;
                break;
            default:
                Console.WriteLine("That option doesn't exists. Setting mission state as planned by default.");
                state = MissionState.Planned;
                
                break;
        }

        return state;
    }
    
    // CRUD Functions
    public void add()
    {
        try
        {
            utils.Separator();
            Console.WriteLine("---ADD MISSION---");

            // Requesting dates
            Console.Write("Type the title: ");
            string title = Console.ReadLine();

            DateTime launchDate = DateTime.Now;

            MissionState state = askState();

            Console.Write("Type the astronaut id: ");
            int astronautId = int.Parse(Console.ReadLine());
            
            Console.Write("Type the spaceship id: ");
            int spaceshipId = int.Parse(Console.ReadLine());

            // Validating dates
            // Checking if the title is empty
            if (title == "") { Console.WriteLine("Title cannot be empty"); return; }
            
            // Checking experience hours it's a positive number
            if (!utils.validateNumber(astronautId) || !utils.validateNumber(spaceshipId)) { Console.WriteLine("Astronaut or Spaceship ID must be a real number"); return; } 

            // Checking that that mission doesn't exists
            if (MissionsRepository.exists(title)) { Console.WriteLine("This mission is already registered."); return; }

            // Checking that the astronaut id exists
            if(AstronautsRepository.getById(astronautId) == null) { Console.WriteLine($"That astronaut with ID {astronautId} doesn't exists"); return; }
            // Checking that the spaceship id exists
            if(SpaceshipRepository.getById(spaceshipId) == null) { Console.WriteLine($"That spaceship with ID {spaceshipId} doesn't exists"); return; }
            
            // Creating model request
            MissionRequest newMission = new MissionRequest
                { Title = title, LaunchDate = launchDate, State = state, AstronautId = astronautId, SpaceshipId = spaceshipId };

            // Adding into db
            bool response = MissionsRepository.add(newMission);

            // Printing result
            if (response) { Console.WriteLine("Mission added succesfully"); }
            else { Console.WriteLine("Database Operation Failed "); }
        }
        catch (Exception err)
        {
            Console.WriteLine($"Error: {err.Message}");
            return;
        }
    }
    public List<Mission>? get()
    {
        try
        {
            utils.Separator();
            Console.WriteLine("---GETTING MISSIONS---");
            
            // DB Query
            List<Mission> missionsList = MissionsRepository.get();

            // Checking if there isn't any match
            if(missionsList.Count == 0) { Console.WriteLine("There isn't any mission registered."); return null; }
            
            // Printing result
            foreach (Mission mission in missionsList) { Console.WriteLine(mission.ToString()); }
            
            // Returning response
            return missionsList;
        }
        catch (Exception err)
        {
            Console.WriteLine($"Error: {err.Message}");

            return null;
        }
    }
    public Mission? update()
    {
        try
        {
            utils.Separator();
            Console.WriteLine("---UPDATE MISSION---");
            
            // Getting data
            Console.Write("Type the id of the mission you want update: ");
            int id = int.Parse(Console.ReadLine());
            
            // Checking that mission exists
            Mission missionFound = MissionsRepository.getById(id);
            if (missionFound == null) { Console.WriteLine("That mission doesn't exists"); return null; }

            // Requesting dates
            Console.Write("Type the new title: ");
            string title = Console.ReadLine();

            DateTime launchDate = DateTime.Now;
            MissionState state = askState();

            Console.Write("Type the astronaut id: ");
            int astronautId = int.Parse(Console.ReadLine());
            
            Console.Write("Type the spaceship id: ");
            int spaceshipId = int.Parse(Console.ReadLine());
            
            // Validating data
            // Checking if the title is empty
            if (title == "") { Console.WriteLine("Title cannot be empty"); return null; }
            
            // Checking experience hours it's a positive number
            if (!utils.validateNumber(astronautId) || !utils.validateNumber(spaceshipId)) { Console.WriteLine("Astronaut or Spaceship ID must be a real number"); return null; } 

            // Checking that the astronaut id exists
            if(AstronautsRepository.getById(astronautId) == null) { Console.WriteLine($"That astronaut with ID {astronautId} doesn't exists"); return null; }
            // Checking that the spaceship id exists
            if(SpaceshipRepository.getById(spaceshipId) == null) { Console.WriteLine($"That spaceship with ID {spaceshipId} doesn't exists"); return null; }

            // Creating mission request
            MissionRequest newMission = new MissionRequest
            {
                Title = title, State = state, LaunchDate = launchDate, AstronautId = astronautId,
                SpaceshipId = spaceshipId
            };

            // Updating mission
            bool response = MissionsRepository.update(id, newMission);

            // Checking response and printing result
            if (response) { Console.WriteLine("Mission updated"); return missionFound; }

            Console.WriteLine("Operation failed");
            return null;
        }
        catch (Exception err)
        {
            Console.WriteLine($"Error: {err.Message}");
            return null;
        }
    }
    public Mission? delete()
    {
        try
        {
            utils.Separator();
            Console.WriteLine("---DELETE MISSION---");
            
            // Asking ID to delete
            Console.Write("Type the ID of the mission: ");
            int id = int.Parse(Console.ReadLine());

            // Checking if it exists
            Mission missionFound = MissionsRepository.getById(id);
            
            if(missionFound == null) { Console.WriteLine("This mission doesn't exists."); return null; }

            // Deleting mission
            bool response = MissionsRepository.delete(id);

            // Printing result
            if (response)
            {
                Console.WriteLine("Mission deleted succesfully");
                return missionFound;
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
    public List<Mission>? filterByState()
    {
        try
        {
            utils.Separator();
            Console.WriteLine("---FILTER BY STATE---");
            
            // Asking state
            MissionState state = askState();
            
            // Making DB Query
            List<Mission> response = MissionsRepository.getByState(state);
            
            // Checking if there isn't any match
            if(response.Count == 0) { Console.WriteLine("There isn't any mission with that state. "); return null; }
            
            // Printing result
            foreach (Mission mission in response) { Console.WriteLine(mission.ToString()); }

            return response;
        }
        catch (Exception err)
        {
            Console.WriteLine($"Error: {err.Message}");
            return null;
        }
    }

    public List<Mission>? filterByRisk()
    {
        try
        {
            utils.Separator();
            Console.WriteLine("---MISSIONS WITH HIGH RISK---");
            
            // Making DB Query
            List<Mission> response = MissionsRepository.getByRisk(ExplorationRegisterRiskLevel.High);
            
            // Checking if there is any result
            if(response.Count == 0) { Console.WriteLine("There isn't any mission with high risk. "); return null; }
            
            // Printing results
            foreach (Mission mission in response) { Console.WriteLine(mission.ToString()); }
            
            return response;
        }
        catch (Exception err)
        {
            Console.WriteLine($"Error: {err.Message}");
            return null;
        }
    }

    public List<Mission>? filterByInCourse()
    {
        try
        {
            utils.Separator();
            Console.WriteLine("---IN COURSE MISSIONS---");
            
            // Making DB Query
            List<Mission> response = MissionsRepository.getByState(MissionState.Course);
            
            // Checking if there is at least one response
            if(response.Count == 0) { Console.WriteLine("There isn't any mission in course." ); return null; }
            
            // Returning result
            foreach (Mission mission in response) { Console.WriteLine(mission.ToString()); }
            
            return response;
        }
        catch(Exception err)
        {
            Console.WriteLine($"Error: {err.Message}");
            return null;
        }
    }
}