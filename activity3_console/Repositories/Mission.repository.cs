using Microsoft.EntityFrameworkCore;

namespace Exercise3.Repositories;

using Exercise3.Data;
using Exercise3.Entities;
using Exercise3.DTOs;
using Exercise3.Enums;

public class MissionsRepository
{
    private static MysqlDbContext db = new MysqlDbContext();
    private static List<Mission> MissionsList = db.Missions.ToList();
    
    // Checking
    public bool exists(string title)
    {
        // Converting to lowercase and querying
        if (db.Missions.Any(m => m.Title.ToLower() == title.ToLower())) { return true; }

        return false;
    }
    
    // GET
    public List<Mission> get()
    {
        // Eager Loading
        return db.Missions.Include(m => m.Astronaut).Include(m => m.Spaceship).ToList();
    }
    public Mission? getById(int id)
    {
        // Eager Loading
        return db.Missions.Include(m => m.Astronaut).Include(m => m.Spaceship).FirstOrDefault(mission => mission.Id == id);
    }
    public List<Mission> getByState(MissionState state)
    {
        return db.Missions.Include(m => m.Astronaut).Include(m => m.Spaceship).Where(mission => mission.State == state).ToList();
    }

    public List<Mission> getByRisk(ExplorationRegisterRiskLevel risk)
    {
        /*
         * Return all the missions that have at least one exploration register with the risk given
         */
        return db.Missions.
            Include(m => m.Astronaut).
            Include(m => m.Spaceship).
            Where(m => m.ExplorationRegister.
                Any(ep => ep.RiskLevel == risk)).
            ToList();
    }
    
    // POST
    public bool add(MissionRequest missionRequest)
    {
        try
        {
            // Adding model to database
            db.Missions.Add(new Mission
            {
                Title = missionRequest.Title, LaunchDate = missionRequest.LaunchDate, State = missionRequest.State,
                AstronautId = missionRequest.AstronautId, SpaceshipId = missionRequest.SpaceshipId
            });
            
            // Saving changes
            db.SaveChanges();

            // Updating local list
            MissionsList = db.Missions.ToList();

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
    public bool update(int id, MissionRequest newMission)
    {
        try
        {
            // Getting actual mission
            Mission actualMission = getById(id);

            // Updating data
            db.Entry(actualMission).CurrentValues.SetValues(newMission);

            // Saving changes
            db.SaveChanges();

            // Updating Memory List
            MissionsList = get();
            
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
            Mission actualMission = getById(id);

            // Deleting astronaut
            db.Missions.Remove(actualMission);

            // Saving changes
            db.SaveChanges();

            // Updating Memory List
            MissionsList = get();
            
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
    public int countByAstronautId(int astronautId)
    {
        return db.Missions.Count(mission => mission.AstronautId == astronautId);
    }
}