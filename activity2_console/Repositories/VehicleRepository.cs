namespace Exercise2.Repositories;

using Exercise2.Data;
using Exercise2.Entities;
using Exercise2.DTOs;

public class VehicleRepository
{
    private static MysqlDbContext db = new MysqlDbContext();
    private List<Vehicle> vehiclesList = db.vehicles.ToList();
    
    // Util Functions
    public Vehicle? checkVehicle(string plate)
    {
        // Getting vehicle
        Vehicle vehicleFound = vehiclesList.FirstOrDefault(vehicle => vehicle.plate == plate); // If there isn't any match, it will return null

        return vehicleFound;
    }
    
    // GET
    public List<Vehicle> get()
    {
        return vehiclesList;
    }
    public Vehicle? findById(int id)
    {
        return db.vehicles.FirstOrDefault(vehicle => vehicle.id == id);
    }
    public List<Vehicle>? findAvailable()
    {
        List<Vehicle> availableVehicles = db.vehicles.Where(vehicle => vehicle.state == "available").ToList();

        // Checking if there isn't any available vehicle
        if (availableVehicles.Count == 0)
        {
            return null;
        }

        return availableVehicles;
    }
    public List<Vehicle>? findBussy()
    {
        return db.vehicles.Where(vehicle => vehicle.state == "bussy").ToList();
    }
    
    // POST
    public bool add(VehicleRequest newVehicle)
    {
        try
        {
            // Checking that the vehicle's plate isn't registered
            Vehicle vehicleFound = checkVehicle(newVehicle.plate);

            if (vehicleFound != null)
            {
                Console.WriteLine("This vehicle is already registered");
                return false;
            }

            // Inserting vehicle into list
            db.vehicles.Add(new Vehicle
            {
                plate = newVehicle.plate, type = newVehicle.type, capacity = newVehicle.capacity,
                state = newVehicle.state
            });

            // Saving changes in db
            db.SaveChanges();

            // Updating memory list
            vehiclesList = db.vehicles.ToList();

            // Returning value
            return true;
        }
        catch (Exception err)
        {
            Console.WriteLine(err.Message);
            return false;
        }
    }
    
    // PATCH
    public bool changeState(int id, string state)
    {
        try
        {
            // Checking that that driver license exists
            Vehicle vehicleFound = findById(id);

            if (vehicleFound == null)
            {
                Console.WriteLine("This vehicle doesn't exists.");

                return false;
            }
            
            // Changing state
            vehicleFound.state = state;
            
            // Saving changes
            db.SaveChanges();
            
            // Returning response
            return true;
        }
        catch (Exception err)
        {
            Console.WriteLine(err.Message);
            return false;
        }
    }
}