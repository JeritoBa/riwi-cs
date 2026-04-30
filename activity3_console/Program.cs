using System.Threading;
using Exercise3;

// Util Functions
Utils utils = new Utils();

// Menu Printing Function
int ShowMenu()
{
    utils.Separator();
    
    Console.WriteLine("-----AstroNova Mission Control-----");
    Console.WriteLine("1. Astronauts Section");
    Console.WriteLine("2. Engineers Section");
    Console.WriteLine("3. Spaceships Section");
    Console.WriteLine("4. Missions Section");
    Console.WriteLine("5. Exploration Registers Section");
    Console.WriteLine("6. Exit system");
    
    Console.Write("Choose the option number of the section you want to enter: ");
    return int.Parse(Console.ReadLine());
}

// Sub-Menus
void CrudPrints(string entitie)
{
    Console.WriteLine($"-----{entitie}s Section-----");
    Console.WriteLine($"1. Add a new {entitie}");
    Console.WriteLine($"2. Get all {entitie}s");
    Console.WriteLine($"3. Update {entitie}");
    Console.WriteLine($"4. Delete {entitie}");
}

int ShowAstronautMenu()
{
    CrudPrints("Astronaut");
    
    Console.WriteLine("5. Filter by range");
    Console.WriteLine("6. Search astronauts with +3 missions");
    Console.WriteLine("7. Count missions by astronaut");
    
    Console.Write("Choose the option number you want do: ");
    
    return int.Parse(Console.ReadLine());
}
int ShowEngineerMenu()
{
    CrudPrints("Engineer");
    
    Console.Write("Choose the option number you want do: ");
    
    return int.Parse(Console.ReadLine());
}
int ShowSpaceshipMenu()
{
    CrudPrints("Spaceship");
    
    Console.WriteLine("5. Find operative spaceships");
    Console.WriteLine("6. Find non-used spaceships");
    
    Console.Write("Choose the option number you want do: ");
    
    return int.Parse(Console.ReadLine());
}
int ShowMissionMenu()
{
    CrudPrints("Mission");
    
    Console.WriteLine("5. Filter missions by state");
    Console.WriteLine("6. Get high risk missions");
    Console.WriteLine("7. Get in-course missions");
    
    Console.Write("Choose the option number you want do: ");
    
    return int.Parse(Console.ReadLine());
}
int ShowExplorationRegisterMenu()
{
    CrudPrints("Exploration Register");
    
    Console.WriteLine("5. Get registers by mission");
    
    Console.Write("Choose the option number you want do: ");
    
    return int.Parse(Console.ReadLine());
}

// Application Initializing
AstroNovaSystem AstroNova = new AstroNovaSystem();

while (true)
{
    int option = ShowMenu();

    utils.Sleep(1000);
    
    switch (option)
    {
        case 1:
            AstroNova.Astronauts(ShowAstronautMenu());
            break;
        case 2:
            AstroNova.Engineers(ShowEngineerMenu());
            break;
        case 3:
            AstroNova.Spaceships(ShowSpaceshipMenu());
            break;
        case 4:
            AstroNova.Missions(ShowMissionMenu());
            break;
        case 5:
            AstroNova.ExplorationRegisters(ShowExplorationRegisterMenu());
            break;
        case 6:
            Console.WriteLine("Exiting...");
            utils.Sleep(1000);
            return;
        default:
            Console.WriteLine("That option doesn't exists");
            break;
    }
}













