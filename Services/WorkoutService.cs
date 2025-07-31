using System.Web;
using WorkoutTrackerWebsite.Data;
using WorkoutTrackerWebsite.Models;

namespace WorkoutTrackerWebsite.Services;

public class WorkoutService
{
    private readonly IWorkoutDB _context;
    private WorkoutSortMethod sortMethod = WorkoutSortMethod.date;
    public WorkoutService(WorkoutContext context)
    {
        _context = context;
    }
    public WorkoutService(IWorkoutDB context)
    {
        _context = context;
    }

    /// <summary>
    /// Gets the workout with the given ID
    /// </summary>
    /// <param name="id">The ID to search for</param>
    /// <returns>A workout with the matching ID or null</returns>
    public Workout? GetByID(int id) => _context.GetById(id);

    /// <summary>
    /// returns a list of workouts with that have a name that contains the one provided
    /// </summary>
    /// <param name="name">The phrase to search for</param>
    /// <returns>A list of workouts that have the given name</returns>
    public List<Workout> GetByWorkoutName(string name) => _context.GetByName(HttpUtility.HtmlEncode(name));

    /// <summary>
    /// Adds a workout to the database
    /// </summary>
    /// <param name="workout">The workout to add</param>
    public void Add(Workout workout)
    {
        _context.Add(workout);
    }

    /// <summary>
    /// Deletes the workout with the corresponding ID in the database
    /// </summary>
    /// <param name="id">The ID of the workout to delete</param>
    public void Detete(int id)
    {
        _context.Delete(id);
    }

    /// <summary>
    /// Updates the given workout in the database
    /// </summary>
    /// <param name="workout">The workout to update</param>
    public void Update(Workout workout)
    {
        _context.Update(workout);
    }

    /// <summary>
    /// Updates the sorting method of the list
    /// </summary>
    /// <param name="method">The sorting method to use</param>
    public void UpdateSortMethod(WorkoutSortMethod method)
    {
        sortMethod = method;
    }

    /// <summary>
    /// Sorts the list of workouts by the set sorting method
    /// </summary>
    /// <returns>The sorted list of workouts</returns>
    public List<Workout> GetSortedWorkouts() => (sortMethod == WorkoutSortMethod.workoutType) ?
                                                _context.Get().OrderBy(w => w.Name).ToList() :
                                                _context.Get().OrderBy(w => w.Date).ToList();
}

public enum WorkoutSortMethod
{
    date,
    workoutType
}
