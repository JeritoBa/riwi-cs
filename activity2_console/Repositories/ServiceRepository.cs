namespace Exercise2.Repositories;

using Exercise2.Data;
using Exercise2.DTOs;
using Exercise2.Entities;

public class ServiceRepository
{
    private static MysqlDbContext db = new MysqlDbContext();
    private List<Service> servicesList = db.transport_service.ToList();
    
    // Util Functions
    
    // GET
    public List<Service> get()
    {
        return servicesList;
    }
    public Service? findById(int id)
    {
        return db.transport_service.FirstOrDefault(service => service.id == id);
    }
    public double getTotalIncomes()
    {
        return db.transport_service.Sum(service => service.total_cost);
    }
    public List<Service> getServicesInCourse()
    {
        return db.transport_service.Where(service => service.state == "progressing").ToList();
    }
    
    // POST
    public bool add(ServiceRequest newService)
    {
        try
        {
            // Inserting service into list
            db.transport_service.Add(new Service { origin = newService.origin, destination = newService.destination, distance = newService.distance, state = newService.state, total_cost = newService.total_cost, driver_id = newService.driver_id, vehicle_id = newService.vehicle_id });

            // Saving changes in db
            db.SaveChanges();

            // Updating list
            servicesList = db.transport_service.ToList();

            // Returning result
            return true;
        }
        catch (Exception err)
        {
            Console.WriteLine(err.Message);

            if (err.InnerException != null)
            {
                Console.WriteLine(err.InnerException.Message);
            }
            
            return false;
        }
    }
    
    // PATCH
    public bool assign(int id, int driver_id, int vehicle_id)
    {
        try
        {
            // Checking that that service id exists
            Service serviceFound = findById(id);

            if (serviceFound == null)
            {
                Console.WriteLine("This service doesn't exists.");

                return false;
            }
            
            // Assigning vehicle and driver ids
            serviceFound.driver_id = driver_id;
            serviceFound.vehicle_id = vehicle_id;
            
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
    public bool changeState(int id, string state)
    {
        try
        {
            // Checking that that driver license exists
            Service serviceFound = findById(id);

            if (serviceFound == null)
            {
                Console.WriteLine("This service doesn't exists.");

                return false;
            }
            
            // Changing state
            serviceFound.state = state;
            
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
    public bool changeCost(int id, double cost)
    {
        try
        {
            // Checking that that driver license exists
            Service serviceFound = findById(id);

            if (serviceFound == null)
            {
                Console.WriteLine("This service doesn't exists.");

                return false;
            }
            
            // Changing total cost
            serviceFound.total_cost = cost;
            
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