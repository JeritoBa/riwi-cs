namespace Exercise3.Services;

using Exercise3.Enums;
using Exercise3.DTOs;
using Exercise3.Repositories;
using Exercise3.Entities;

public class ExplorationRegisterService
{
    private static Utils utils = new Utils();
    private static ExplorationRegisterRepository ExplorationRegisterRepository = new ExplorationRegisterRepository();

    private static MissionsRepository MissionsRepository = new MissionsRepository();
    
    // Getting values by switchs
    public ExplorationRegisterRiskLevel askRisk()
    {
        Console.WriteLine("Select the risk");
        Console.WriteLine("1. Low");
        Console.WriteLine("2. Medium");
        Console.WriteLine("3. High");

        Console.Write("Type the number of the option: ");
        int riskOption = int.Parse(Console.ReadLine());
        ExplorationRegisterRiskLevel risk;

        switch (riskOption)
        {
            case 1:
                risk = ExplorationRegisterRiskLevel.Low;
                break;
            case 2:
                risk = ExplorationRegisterRiskLevel.Medium;
                break;
            case 3:
                risk = ExplorationRegisterRiskLevel.High;
                break;
            default:
                Console.WriteLine("That option doesn't exists. Setting exploration register risk as low by default.");
                risk = ExplorationRegisterRiskLevel.Low;
                
                break;
        }

        return risk;
    }
    
    // CRUD Functions
    public void add()
    {
        try
        {
            utils.Separator();
            Console.WriteLine("---ADD EXPLORATION REGISTER---");

            // Requesting dates
            Console.Write("Type the planet destination: ");
            string planetDestination = Console.ReadLine();
            
            Console.Write("Type description of register: ");
            string description = Console.ReadLine();

            ExplorationRegisterRiskLevel risk = askRisk();

            Console.Write("Type the mission id: ");
            int missionId = int.Parse(Console.ReadLine());

            // Validating dates
            
            // Checking that planet destination and description aren't empty
            if(planetDestination == "" || description == "") { Console.WriteLine("Planet destination or description cannot be empty."); return; }
            
            // Checking that mission id is higher than zero
            if(!utils.validateNumber(missionId)) { Console.WriteLine("Mission ID must be valid"); return; }
            
            // Checking the mission id exists
            if (MissionsRepository.getById(missionId) == null) { Console.WriteLine($"Mission with ID {missionId} doesn't exist"); return; }
            
            // Creating model request
            ExplorationRegisterRequest newExplorationRegister = new ExplorationRegisterRequest { PlanetDestination = planetDestination, Description = description, RiskLevel = risk, MissionId = missionId };

            // Adding into db
            bool response = ExplorationRegisterRepository.add(newExplorationRegister);

            // Printing result
            if(response) { Console.WriteLine("Exploration register has been added"); return; }
            else { Console.WriteLine("Failed Operation"); return; }
        }
        catch (Exception err)
        {
            Console.WriteLine($"Error: {err.Message}");
            return;
        }
    }
    public List<ExplorationRegister>? get()
    {
        try
        {
            utils.Separator();
            Console.WriteLine("---GETTING EXPLORATION REGISTERS---");
            
            // DB Query
            List<ExplorationRegister> explorationRegistersList = ExplorationRegisterRepository.get();

            // Checking if there isn't any match
            if(explorationRegistersList.Count == 0) { Console.WriteLine("There isn't any mission registered."); return null; }
            
            // Printing result
            foreach (ExplorationRegister explorationRegister in explorationRegistersList) { Console.WriteLine(explorationRegister.ToString()); }
            
            // Returning response
            return explorationRegistersList;
        }
        catch (Exception err)
        {
            Console.WriteLine($"Error: {err.Message}");

            return null;
        }
    }

    public ExplorationRegister? update()
    {
        try
        {
            utils.Separator();
            Console.WriteLine("---UPDATE EXPLORATION REGISTER---");

            // Getting exploration register id
            Console.WriteLine("Type the id of the exploration register you want update: ");
            int id = int.Parse(Console.ReadLine());

            // Checking that that exploration register exists
            ExplorationRegister explorationRegisterFound = ExplorationRegisterRepository.getById(id);
            if(explorationRegisterFound == null) { Console.WriteLine("That exploration register doesn't exists"); return null; }
            
            // Requesting new dates
            Console.Write("Type the new planet destination: ");
            string planetDestination = Console.ReadLine();
            
            Console.Write("Type the new description of register: ");
            string description = Console.ReadLine();

            ExplorationRegisterRiskLevel risk = askRisk();

            Console.Write("Type the new mission id: ");
            int missionId = int.Parse(Console.ReadLine());
            
            // Validating new dates
            
            // Checking that planet destination and description aren't empty
            if(planetDestination == "" || description == "") { Console.WriteLine("Planet destination or description cannot be empty."); return null; }
            
            // Checking that mission id is higher than zero
            if(!utils.validateNumber(missionId)) { Console.WriteLine("Mission ID must be valid"); return null; }

            // Creating exploration request
            ExplorationRegisterRequest newExplorationRegister = new ExplorationRegisterRequest { PlanetDestination = planetDestination, Description = description, RiskLevel = risk, MissionId = missionId };
            // Updating exploration register
            bool response = ExplorationRegisterRepository.update(id, newExplorationRegister);

            // Returning response
            if(response) { Console.WriteLine("Exploration Register updated succesfully"); return explorationRegisterFound; }
            
            Console.WriteLine("Operation failed");
            return null;
        }
        catch (Exception err)
        {
            Console.WriteLine($"Error: {err.Message}");
            return null;
        }
    }
    public ExplorationRegister? delete()
    {
        try
        {
            utils.Separator();
            Console.WriteLine("---DELETE EXPLORATION REGISTER---");
            
            // Asking ID to delete
            Console.Write("Type the ID of the exploration register: ");
            int id = int.Parse(Console.ReadLine());

            // Checking if it exists
            ExplorationRegister explorationRegister = ExplorationRegisterRepository.getById(id);
            
            if(explorationRegister == null) { Console.WriteLine("This exploration register doesn't exists."); return null; }

            // Deleting exploration register
            bool response = ExplorationRegisterRepository.delete(id);

            // Printing result
            if (response)
            {
                Console.WriteLine("ExplorationRegister deleted succesfully");
                return explorationRegister;
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
    public List<ExplorationRegister> getByMissionId()
    {
        try
        {
            utils.Separator();
            Console.WriteLine("---GET EXPLORATION REGISTERS---");
            
            // Asking for ID
            Console.Write("Type the id of the mission you want get registers: ");
            int missionId = int.Parse(Console.ReadLine());
            
            // Checking that the id is valid
            if(!utils.validateNumber(missionId)) { Console.WriteLine("Mission ID must be valid"); return null; }
            
            // Checking that mission id exists
            Mission missionFound = MissionsRepository.getById(missionId);
            
            if(missionFound == null) { Console.WriteLine("Mission doesn't exists."); return null; }
            
            // Making DB Query
            List<ExplorationRegister> response = ExplorationRegisterRepository.getByMissionId(missionId);

            // Checking if there is any response
            if(response.Count == 0) { Console.WriteLine("There are no registers about this mission."); return null; }

            // Printing result
            Console.WriteLine(missionFound.ToString());
            Console.WriteLine("Found Registers: ");
            foreach(ExplorationRegister explorationRegister in response) { Console.WriteLine(explorationRegister.ToString()); }
            
            return response;
        }
        catch (Exception err)
        {
            Console.WriteLine($"Error: {err.Message}");
            return null;
        }
    }
}