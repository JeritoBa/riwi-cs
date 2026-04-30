using System.Threading;

using Exercise2.DTOs;
using Exercise2.Repositories;
using Exercise2.Entities;

// Important Vars
double kmPrice = 2.0; // Price in usd for each km rided

// Repositories
DriverRepository dbDriver = new DriverRepository();
VehicleRepository dbVehicle = new VehicleRepository();
ServiceRepository dbService = new ServiceRepository();

// Util Functions
void Separator()
{
    Console.WriteLine("---------------------------");
}
void Sleep(int ms)
{
    Thread.Sleep(ms);
}

// Services

// 1. Drivers Register
void RegisterDriver()
{
    Separator();
    Console.WriteLine("--- Add a new driver ---");
    
    // Requesting data
    Console.Write("Type the full name of the driver: ");
    string name = Console.ReadLine();
    
    Console.Write("Type the license: ");
    string license = Console.ReadLine();
    
    // Creating DTO (model base)
    DriverRequest newDriver = new DriverRequest { fullname = name, license = license };
    
    // Post Request
    bool response = dbDriver.add(newDriver);
    
    Sleep(1000); // Sleeping time
    
    // Checking response
    if (response)
    {
        Console.WriteLine("User added succesfully");
        return;
    }
    
    Console.WriteLine("Failed Post");
}

// 2. Vehicles Register
void RegisterVehicle()
{
    Separator();
    Console.WriteLine("--- Add a new vehicle ---");
    
    // Requesting data
    Console.Write("Type the plate serial: ");
    string plate = Console.ReadLine();
    
    Console.WriteLine("--Type of car--");
    Console.WriteLine("1. Car");
    Console.WriteLine("2. Motocycle");
    Console.WriteLine("3. Truck");

    Console.Write("Type the number of the vehicle type: ");
    int typeChoosed = int.Parse(Console.ReadLine());
    string type;
    
    switch (typeChoosed)
    {
        case 1:
            type = "car";
            break;
        case 2:
            type = "motocycle";
            break;
        case 3:
            type = "truck";
            break;
        default:
            Console.WriteLine("Bad option. Try again");
            return;
    }
    
    Console.Write("Type the capacity of the vehicle: ");
    int capacity = int.Parse(Console.ReadLine());

    // Checking that the capacity is minor than zero
    if (capacity < 1)
    {
        Console.WriteLine("Capacity must be minor than zero");
        return;
    }
    
    // Creating DTO (model base)
    VehicleRequest newVehicle = new VehicleRequest { plate = plate, type = type, capacity = capacity };
    
    // Post request
    bool response = dbVehicle.add(newVehicle);
    
    Sleep(1000); // Sleeping time
    
    // Checking response
    if (response)
    {
        Console.WriteLine("Vehicle added succesfully");
        return;
    }
    
    Console.WriteLine("Failed Post");
}

// 3. Services register
void RegisterService()
{
    Separator();
    Console.WriteLine("--- Add a new service ---");
    
    // Requesting data to service
    Console.Write("Insert your origin pick up location: ");
    string origin = Console.ReadLine();
    
    Console.Write("Type your destination: ");
    string destination = Console.ReadLine();
    
    // Checking that origin and destination are not empty
    if (origin.Length == 0 || destination.Length == 0)
    {
        Console.WriteLine("Direction dates cannot be empty");
        return;
    }
    
    Console.Write("Type the distance in km: ");
    double distance = double.Parse(Console.ReadLine());

    // Checking the distance is higher than zero
    if (distance <= 0)
    {
        Console.WriteLine("Distance must be higher than zero");
        return;
    }
    
    // Creating DTO 
    ServiceRequest newService = new ServiceRequest
    {
        origin = origin, destination = destination, distance = distance
    };
    
    // Post request
    bool response = dbService.add(newService);
    
    Sleep(1000); // Sleeeping time
    
    // Checking response
    if (response)
    {
        Console.WriteLine("Service added succesfully");
        return;
    }
    
    Console.WriteLine("Failed Post");
}

// 4. Resources Assignment
void AssignService()
{
    Separator();
    Console.WriteLine("--- Assign service ---");
    
    // Asking for service id
    Console.Write("Write the ID of the service: ");
    int id = int.Parse(Console.ReadLine());
    
    // Checking that exists
    Service mainService = dbService.findById(id);

    if (mainService == null)
    {
        Console.WriteLine("This service doesn't exists");
        return;
    }
    
    // Searching available drivers and vehicles
    List<Driver> driversAvailable = dbDriver.findAvailable();
    List<Vehicle> vehiclesAvailable = dbVehicle.findAvailable();
    
    // Checking that there's any driver available
    if (dbDriver.findAvailable() == null)
    {
        Console.WriteLine("There isn't any driver available");
        return;
    } else if (dbVehicle.findAvailable() == null)
    { // Checking that there's any vehicle available
        Console.WriteLine("There isn't any vehicle available");
        return;
    }
    
    // Assigning driver and vehicle
    int driver_id = driversAvailable[0].id;
    int vehicle_id = vehiclesAvailable[0].id;

    dbService.assign(id, driver_id, vehicle_id);
    
    // Changing state of driver and vehicle
    dbDriver.changeState(driver_id, "service");
    dbVehicle.changeState(vehicle_id, "bussy");
    
    Console.WriteLine("Service Resources Assigned");
    
    // Sleeping
    Sleep(2000);
}

// 5. Service Init
void InitService()
{
    Separator();
    Console.WriteLine("--- Init a service ---");
    
    // Asking for service id
    Console.Write("Write the ID of the service: ");
    int id = int.Parse(Console.ReadLine());
    
    // Checking that exists
    Service mainService = dbService.findById(id);

    if (mainService == null)
    {
        Console.WriteLine("This service doesn't exists");
        return;
    }
    
    // Checking that the service has been assigned
    if (mainService.driver_id == 0 || mainService.vehicle_id == 0)
    {
        Console.WriteLine("You must assign first the service with a vehicle and a driver");
        return;
    }
    
    // Changing service state
    dbService.changeState(id, "progressing");
    
    Console.WriteLine("Service Inited");
    
    // Sleeping
    Sleep(2000);
}

// 6. Service finish
void FinishService()
{
    Separator();
    Console.WriteLine("--- Finish a service ---");
    
    // Asking for service id
    Console.Write("Write the ID of the service: ");
    int id = int.Parse(Console.ReadLine());
    
    // Checking that exists
    Service mainService = dbService.findById(id);

    if (mainService == null)
    {
        Console.WriteLine("This service doesn't exists");
        return;
    }
    
    // Checking that the service is in progress
    if (mainService.state != "progressing")
    {
        Console.WriteLine("This service hasn't been init until");
        return;
    }
    
    // Changing service state
    dbService.changeState(id, "finished");
    
    // Changing state of driver and vehicle
    int driver_id = mainService.driver_id;
    int vehicle_id = mainService.vehicle_id;
    
    // Checking that the driver and vehicle ids exists
    dbDriver.changeState(driver_id, "available");
    dbVehicle.changeState(vehicle_id, "available");
    
    // Calculating charge to client and printing it
    double total_cost = mainService.distance * kmPrice;

    dbService.changeCost(id, total_cost);
    Console.WriteLine($"Total cost: {total_cost}$");
    
    // Sleeping
    Sleep(2000);
}

// 7. Service Query
void ServiceQuery()
{
    Separator();
    Console.WriteLine("--- Getting services ---");
    
    // Getting all services
    List<Service> servicesList = dbService.get();
    
    // Printing one by one
    foreach (Service service in servicesList)
    {
        Console.WriteLine(service.ToString());
    }
    
    // Sleeping
    Sleep(2000);
}

// 8. Resources Query
void ResourcesQuery()
{
    Separator();
    Console.WriteLine("--- Resources Query ---");
    
    // Printing submenu
    

    int subMenu()
    {
        Console.WriteLine("1. List drivers");
        Console.WriteLine("2. List vehicles");
        Console.WriteLine("3. Exit");
        
        Console.Write("Choose the option you want do: ");
        int option = int.Parse(Console.ReadLine());

        return option;
    }

    void ListDrivers()
    {
        List<Driver> driversList = dbDriver.get();

        foreach (Driver driver in driversList)
        {
            Console.WriteLine(driver.ToString());
        }
    }

    void ListVehicles()
    {
        List<Vehicle> vehiclesList = dbVehicle.get();

        foreach (Vehicle vehicle in vehiclesList)
        {
            Console.WriteLine(vehicle.ToString());
        }
    }
    
    while (true)
    {
        int option = subMenu();

        switch (option)
        {
            case 1:
                ListDrivers();
                break;
            case 2:
                ListVehicles();
                break;
            case 3:
                Console.WriteLine("Going to main menu...");
                Sleep(2000);
                return;
        }
        
        Sleep(1000);
    }
}

// 9. Main Reports
void GenerateReports()
{
    Separator();
    Console.WriteLine("--- Main Report ---");
    
    // Declarating vars
    int totalServices = dbService.get().Count;
    double totalIncomes = dbService.getTotalIncomes();
    int servicesInCourse = dbService.getServicesInCourse().Count;
    int driversBussy = dbDriver.findBussy().Count;
    int driversAvailable = dbDriver.findAvailable().Count;
    int vehiclesBussy = dbDriver.findBussy().Count;
    int vehiclesAvailable = dbDriver.findAvailable().Count;
    
    // Printing each report
    Console.WriteLine($"Total of services: {totalServices}");
    Console.WriteLine($"Total Incomes: {totalIncomes}$");
    Console.WriteLine($"Services in Course: {servicesInCourse}");
    Console.WriteLine($"Drivers Bussy: {driversBussy}");
    Console.WriteLine($"Drivers Availables: {driversAvailable}");
    Console.WriteLine($"Vehicles Bussy: {vehiclesBussy}");
    Console.WriteLine($"Vehicles Availables: {vehiclesAvailable}");
    
    // Sleeping
    Sleep(2000);
}

// Menu Function
int ShowMenu()
{
    try
    {
        Separator();

        Console.WriteLine("--- Transport Management System ---");

        Console.WriteLine("1. Register driver");
        Console.WriteLine("2. Register vehicle");
        Console.WriteLine("3. Register transport service");
        Console.WriteLine("4. Assign driver and vehicle to any service");
        Console.WriteLine("5. Init service");
        Console.WriteLine("6. Finish service");
        Console.WriteLine("7. Search services");
        Console.WriteLine("8. Search drivers and vehicles");
        Console.WriteLine("9. Operative reports");
        Console.WriteLine("10. Exit");

        Console.Write("Choose any option: ");
        int option = int.Parse(Console.ReadLine());

        return option;
    }
    catch (FormatException err)
    {
        // Will be managed as an error inside the switch clause.
        return 0;
    }
}

// App initializing - Unique User View
while (true)
{
    // Showing menu options
    int option = ShowMenu();
    
    // Calling service by option elected
    switch (option)
    {
        case 0:
            Console.WriteLine("Option must be an int");
            break;
        case 1:
            RegisterDriver();
            break;
        case 2:
            RegisterVehicle();
            break;
        case 3:
            RegisterService();
            break;
        case 4:
            AssignService();
            break;
        case 5:
            InitService();
            break;
        case 6:
            FinishService();
            break;
        case 7:
            ServiceQuery();
            break;
        case 8:
            ResourcesQuery();
            break;
        case 9:
            GenerateReports();
            break;
        case 10:
            Console.WriteLine("Exiting...");
            Sleep(1000);
            
            return;
        default: 
            Console.WriteLine("That option doesn't exists");
            break;
    }
    
    // Sleeping time
    Sleep(2000);
}