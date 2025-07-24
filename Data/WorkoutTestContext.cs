using System.Web;
using WorkoutTrackerWebsite.Models;

namespace WorkoutTrackerWebsite.Data;

/// <summary>
/// Contains a list that is used to simulate a small database for testing
/// </summary>
public class WorkoutTestContext : IWorkoutDB
{
    private static DateOnly date1 = new DateOnly(2025, 1, 16);
    private static DateOnly date2 = new DateOnly(2025, 1, 28);

    public WorkoutTestContext() { }
    public List<Workout> _testDB { get; private set; } = new List<Workout>
    {
        new Workout(0, "Sit Ups", date1, 0, 3, 12),
        new Workout(1, "Bicep Curl", date1, 20, 3, 12),
        new Workout(2, "Sit Ups", date2, 0, 3, 12)
    };

    public void Add(Workout workout)
    {
        _testDB.Add(workout);
    }

    public void Delete(int id)
    {
        Workout? workoutToDelete = GetById(id);

        if (workoutToDelete is not null)
            _testDB.Remove(workoutToDelete);
    }

    public List<Workout> Get()
    {
        return CopyWorkoutList();
    }

    public List<Workout> GetByName(string name)
    {
        string sanitizedString = HttpUtility.HtmlEncode(name);
        return _testDB.Where(w => w.Name.Contains(sanitizedString)).ToList();
    }

    public Workout? GetById(int id)
    {
        return _testDB.FirstOrDefault(w => w.Id == id);
    }

    public void Update(Workout newWorkout)
    {
        Workout? workoutToUpdate = GetById(newWorkout.Id);

        if (workoutToUpdate is not null)
        {
            int index = _testDB.IndexOf(workoutToUpdate);
            _testDB[index] = newWorkout;
        }
    }

    private List<Workout> CopyWorkoutList()
    {
        List<Workout> copy = new List<Workout>();

        for (int i = 0; i < _testDB.Count; i++)
            copy.Add(_testDB[i]);

        return copy;
    }
}