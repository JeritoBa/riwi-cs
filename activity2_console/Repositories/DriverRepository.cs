namespace Exercise2.Repositories;

using Exercise2.Data;
using Exercise2.DTOs;
using Exercise2.Entities;

public class DriverRepository
{
    private static MysqlDbContext db = new MysqlDbContext();
    private List<Driver> driversList = db.drivers.ToList();

    // Util Functions
    public Driver? checkUser(string license)
    {
        // Getting driver
        Driver driverFound = driversList.FirstOrDefault(driver => driver.license == license);

        return driverFound;
    }
    
    // GET
    public List<Driver> get()
    {
        return driversList;
    }
    public Driver? findById(int id)
    {
        return db.drivers.FirstOrDefault(driver => driver.id == id);
    }
    public List<Driver>? findAvailable()
    {
        List<Driver> availableDrivers = db.drivers.Where(driver => driver.state == "available").ToList();

        // Checking if there isn't any available driver
        if (availableDrivers.Count == 0)
        {
            return null;
        }

        return availableDrivers;
    }
    public List<Driver>? findBussy()
    {
        return db.drivers.Where(driver => driver.state == "service").ToList();
    }
    
    // POST
    public bool add(DriverRequest newDriver)
    {
        try
        {
            // Checking that that user license isn't registered
            Driver driverFound = checkUser(newDriver.license);

            if (driverFound != null)
            {
                Console.WriteLine("This user is already registered.");

                return false;
            }

            // Inserting driver into list
            db.drivers.Add(new Driver { fullName = newDriver.fullname, license = newDriver.license, state = newDriver.state });

            // Saving changes in db
            db.SaveChanges();

            // Updating list
            driversList = db.drivers.ToList();

            // Returning result
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
            Driver driverFound = findById(id);

            if (driverFound == null)
            {
                Console.WriteLine("This driver doesn't exists.");

                return false;
            }
            
            // Changing state
            driverFound.state = state;
            
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