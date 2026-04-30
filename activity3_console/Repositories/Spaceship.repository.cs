using Exercise3.Enums;

namespace Exercise3.Repositories;

using Exercise3.Data;
using Exercise3.Entities;
using Exercise3.DTOs;

public class SpaceshipRepository
{
    private static MysqlDbContext db = new MysqlDbContext();
    private static List<Spaceship> SpaceshipsList = db.Spaceships.ToList();
    
    // Checking
    public bool exists(string name, string model)
    {
        // Converting to lowercase and querying
        if (db.Spaceships.Any(a => a.Name.ToLower() == name.ToLower()) &&
            db.Spaceships.Any(a => a.Model.ToLower() == model.ToLower()))
        {
            return true;
        }

        return false;
    }
    
    // GET
    public List<Spaceship> get()
    {
        return db.Spaceships.ToList();
    }
    public Spaceship? getById(int id)
    {
        return db.Spaceships.FirstOrDefault(spaceship => spaceship.Id == id);
    }
    public List<Spaceship> getByState(SpaceshipState state)
    {
        return db.Spaceships.Where(spaceship => spaceship.State == state).ToList();
    }
    
    // POST
    public bool add(SpaceshipRequest newSpaceship)
    {
        try
        {
            // Adding model to database
            db.Spaceships.Add(new Spaceship
            {
                Name = newSpaceship.Name, Model = newSpaceship.Model, Capacity = newSpaceship.Capacity, State = newSpaceship.State
            });
            
            // Saving changes
            db.SaveChanges();

            // Updating local list
            SpaceshipsList = get();

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
    public bool update(int id, SpaceshipRequest newSpaceship)
    {
        try
        {
            // Getting actual spaceship
            Spaceship actualSpaceship = getById(id);

            // Updating data
            db.Entry(actualSpaceship).CurrentValues.SetValues(newSpaceship);

            // Saving changes
            db.SaveChanges();

            // Updating Memory List
            SpaceshipsList = get();
            
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
            // Getting actual spaceship
            Spaceship actualSpaceship = getById(id);

            // Deleting spaceship
            db.Spaceships.Remove(actualSpaceship);

            // Saving changes
            db.SaveChanges();

            // Updating Memory List
            SpaceshipsList = get();
            
            // Returning response
            return true;
        }
        catch (Exception err)
        {
            Console.WriteLine($"Database Error: {err.Message}");
            return false;
        }
    }
    
    // CUSTOM QUERIES
    public List<Spaceship> filterByNonUsed()
    {
        /*
         * Return the spaceships where not have any mission
         */
        return db.Spaceships.Where(s => !s.Missions.Any()).ToList();
    }
}