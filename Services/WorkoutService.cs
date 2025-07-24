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

    public Workout? GetByID(int id) => _context.GetById(id);

    public List<Workout> GetByWorkoutName(string name) => _context.GetByName(HttpUtility.HtmlEncode(name));

    public void Add(Workout workout)
    {
        _context.Add(workout);
    }

    public void Detete(int id)
    {
        _context.Delete(id);
    }

    public void Update(Workout workout)
    {
        _context.Update(workout);
    }

    public void UpdateSortMethod(WorkoutSortMethod method)
    {
        sortMethod = method;
    }

    public List<Workout> GetSortedWorkouts()
    {
        List<Workout> workouts = _context.Get();

        return (sortMethod == WorkoutSortMethod.workoutType) ? _context.Get().OrderBy(w => w.Name).ToList() : _context.Get().OrderBy(w => w.Date).ToList();
    }

}

public enum WorkoutSortMethod
{
    date,
    workoutType
}
