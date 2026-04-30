namespace Exercise3.Repositories;

using Exercise3.Data;
using Exercise3.Entities;
using Exercise3.DTOs;

public class EngineerRepository
{
    private static MysqlDbContext db = new MysqlDbContext();
    private static List<Engineer> EngineersList = db.Engineers.ToList();
    
    // Checking
    public bool exists(string name, string lastname)
    {
        // Converting to lowercase and querying
        if (db.Engineers.Any(a => a.Name.ToLower() == name.ToLower()) &&
            db.Engineers.Any(a => a.LastName.ToLower() == lastname.ToLower()))
        {
            return true;
        }

        return false;
    }
    
    // GET
    public List<Engineer> get()
    {
        return db.Engineers.ToList();
    }
    public Engineer? getById(int id)
    {
        return db.Engineers.FirstOrDefault(engineer => engineer.Id == id);
    }
    
    // POST
    public bool add(EngineerRequest newEngineer)
    {
        try
        {
            // Adding model to database
            db.Engineers.Add(new Engineer
            {
                Name = newEngineer.Name, LastName = newEngineer.LastName, Speciality = newEngineer.Speciality, ExperienceYears = newEngineer.ExperienceYears
            });

            // Saving changes
            db.SaveChanges();

            // Updating local list
            EngineersList = db.Engineers.ToList();

            // Returning result
            return true;
        }
        catch (Exception err)
        {
            Console.WriteLine($"Database Error: {err.Message}");
            return false;
        }
    }
    
    // PATCH
    public bool update(int id, EngineerRequest newEngineer)
    {
        try
        {
            // Getting actual engineer
            Engineer actualEngineer = getById(id);

            // Updating data
            db.Entry(actualEngineer).CurrentValues.SetValues(newEngineer);

            // Saving changes
            db.SaveChanges();

            // Updating Memory List
            EngineersList = get();
            
            // Returning response
            return true;
        }
        catch (Exception err)
        {
            Console.WriteLine($"Database Error: {err.Message}");
            return false;
        }
    }
    
    // DELETE
    public bool delete(int id)
    {
        try
        {
            // Getting actual engineer
            Engineer actualEngineer = getById(id);

            // Deleting engineer
            db.Engineers.Remove(actualEngineer);

            // Saving changes
            db.SaveChanges();

            // Updating Memory List
            EngineersList = get();
            
            // Returning response
            return true;
        }
        catch (Exception err)
        {
            Console.WriteLine($"Database Error: {err.Message}");
            return false;
        }
    }
}