using Microsoft.EntityFrameworkCore.Diagnostics.Internal;

namespace Exercise3.Repositories;

using Exercise3.Data;
using Exercise3.Entities;
using Exercise3.DTOs;
using Exercise3.Enums;

public class AstronautsRepository
{
    private static MysqlDbContext db = new MysqlDbContext();
    private static List<Astronaut> AstronautsList = db.Astronauts.ToList();
    
    // Checking
    public bool exists(string name, string lastname)
    {
        // Converting to lowercase and querying
        if (db.Astronauts.Any(a => a.Name.ToLower() == name.ToLower()) &&
            db.Astronauts.Any(a => a.LastName.ToLower() == lastname.ToLower()))
        {
            return true;
        }

        return false;
    }
    
    // GET
    public List<Astronaut> get()
    {
        return db.Astronauts.ToList();
    }
    public Astronaut? getById(int id)
    {
        return db.Astronauts.FirstOrDefault(astronaut => astronaut.Id == id);
    }
    public List<Astronaut> getByRange(AstronautRange range)
    {
        return db.Astronauts.Where(astronaut => astronaut.Range == range).ToList();
    }
    public List<Astronaut> getByMissions(int missionsQuantity)
    {
        return db.Astronauts.Where(a => a.Missions.Count >= missionsQuantity).ToList();
    }
    
    // POST
    public bool add(AstronautRequest newAstronaut)
    {
        try
        {
            // Adding model to database
            db.Astronauts.Add(new Astronaut
            {
                Name = newAstronaut.Name, LastName = newAstronaut.LastName, Range = newAstronaut.Range,
                ExperienceHours = newAstronaut.ExperienceHours
            });

            // Saving changes
            db.SaveChanges();

            // Updating local list
            AstronautsList = db.Astronauts.ToList();

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
    public bool update(int id, AstronautRequest newAstronaut)
    {
        try
        {
            // Getting actual astronaut
            Astronaut actualAstronaut = getById(id);

            // Updating data
            db.Entry(actualAstronaut).CurrentValues.SetValues(newAstronaut);

            // Saving changes
            db.SaveChanges();

            // Updating Memory List
            AstronautsList = get();
            
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
            // Getting actual astronaut
            Astronaut actualAstronaut = getById(id);

            // Deleting astronaut
            db.Astronauts.Remove(actualAstronaut);

            // Saving changes
            db.SaveChanges();
            
            // Updating Memory List
            AstronautsList = get();

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