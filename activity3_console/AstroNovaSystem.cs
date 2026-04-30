namespace Exercise3;
using Exercise3.Services;

public class AstroNovaSystem
{
    // Service Declarations
    private AstronautService astronautService = new AstronautService();
    private EngineerService engineerService = new EngineerService();
    private SpaceshipService spaceshipService = new SpaceshipService();
    private MissionService missionService = new MissionService();
    private ExplorationRegisterService ExplorationRegisterService = new ExplorationRegisterService();
    
    public void Astronauts(int option)
    {
        switch (option)
        {
            case 1:
                astronautService.add();
                break;
            case 2:
                astronautService.get();
                break;
            case 3:
                astronautService.update();
                break;
            case 4:
                astronautService.delete();
                break;
            case 5: // Filter by range
                astronautService.filterByRange();
                break;
            case 6: // Search astronauts with +3 missions
                astronautService.filterByMissions();
                break;
            case 7: // Count missions by astronaut
                astronautService.countMissions();
                break;
            default:
                Console.WriteLine("Opción no valida.");
                return;
        }
    }

    public void Engineers(int option)
    {
        switch (option)
        {
            case 1:
                engineerService.add();
                break;
            case 2:
                engineerService.get();
                break;
            case 3:
                engineerService.update();
                break;
            case 4:
                engineerService.delete();
                break;
            default:
                Console.WriteLine("Opción no valida.");
                return;
        }
    }

    public void Spaceships(int option)
    {
        switch (option)
        {
            case 1:
                spaceshipService.add();
                break;
            case 2:
                spaceshipService.get();
                break;
            case 3:
                spaceshipService.update();
                break;
            case 4:
                spaceshipService.delete();
                break;
            case 5: // Find operative spaceships
                spaceshipService.filterByState();
                break;
            case 6: // Find non-used spaceships
                spaceshipService.filterByNonUsed();
                break;
            default:
                Console.WriteLine("Opción no valida.");
                return;
        }
    }

    public void Missions(int option)
    {
        switch (option)
        {
            case 1:
                missionService.add();
                break;
            case 2:
                missionService.get();
                break;
            case 3:
                missionService.update();
                break;
            case 4:
                missionService.delete();
                break;
            case 5: // Filter missions by state
                missionService.filterByState();
                break;
            case 6: // Get high risk missions
                missionService.filterByRisk();
                break;
            case 7: // Get in-course missions
                missionService.filterByInCourse();
                break;
            default:
                Console.WriteLine("Opción no valida.");
                return;
        }
    }

    public void ExplorationRegisters(int option)
    {
        switch (option)
        {
            case 1:
                ExplorationRegisterService.add();
                break;
            case 2:
                ExplorationRegisterService.get();
                break;
            case 3:
                ExplorationRegisterService.update();
                break;
            case 4:
                ExplorationRegisterService.delete();
                break;
            case 5: // Get registers by mission
                ExplorationRegisterService.getByMissionId();
                break;
            default:
                Console.WriteLine("Opción no valida.");
                return;
        }
    }
}