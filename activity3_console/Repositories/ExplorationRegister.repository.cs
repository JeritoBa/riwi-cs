using Microsoft.EntityFrameworkCore;

namespace Exercise3.Repositories;

using Exercise3.Data;
using Exercise3.Entities;
using Exercise3.DTOs;

public class ExplorationRegisterRepository
{
    private static MysqlDbContext db = new MysqlDbContext();
    private static List<ExplorationRegister> ExplorationRegistersList = db.ExplorationRegisters.ToList();
    
    // GET
    public List<ExplorationRegister> get()
    {
        // .Include() works for Eager Loading, where EF understand that it must load that specific entity data
        return db.ExplorationRegisters.Include(ep => ep.Mission).ToList();
    }
    public ExplorationRegister? getById(int id)
    {
        return db.ExplorationRegisters.Include(ep => ep.Mission).FirstOrDefault(explorationRegister => explorationRegister.Id == id);
    }

    public List<ExplorationRegister> getByMissionId(int missionId)
    {
        return db.ExplorationRegisters.Include(ep => ep.Mission).Where(explorationRegister => explorationRegister.MissionId == missionId).ToList();
    }
    
    // POST
    public bool add(ExplorationRegisterRequest newExplorationRegister)
    {
        try
        {
            // Adding model to database
            db.ExplorationRegisters.Add(new ExplorationRegister
            {
                PlanetDestination = newExplorationRegister.PlanetDestination, Description = newExplorationRegister.Description, RiskLevel = newExplorationRegister.RiskLevel, MissionId = newExplorationRegister.MissionId
            });
            
            // Saving changes
            db.SaveChanges();

            // Updating local list
            ExplorationRegistersList = db.ExplorationRegisters.ToList();

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
    public bool update(int id, ExplorationRegisterRequest newExplorationRegister)
    {
        try
        {
            // Getting actual exploration register
            ExplorationRegister actualExplorationRegister = getById(id);

            // Updating data
            db.Entry(actualExplorationRegister).CurrentValues.SetValues(newExplorationRegister);

            // Saving changes
            db.SaveChanges();

            // Updating Memory List
            ExplorationRegistersList = get();
            
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
            // Getting actual exploration register
            ExplorationRegister actualExplorationRegister = getById(id);

            // Deleting exploration register
            db.ExplorationRegisters.Remove(actualExplorationRegister);

            // Saving changes
            db.SaveChanges();

            // Updating Memory List
            ExplorationRegistersList = get();
            
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