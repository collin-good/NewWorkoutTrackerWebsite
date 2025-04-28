using WorkoutTrackerWebsite.Models;
namespace WorkoutTrackerWebsite.Data;

public static class DbInitializer
{
    public static void Initialize(WorkoutContext context)
    {
        if (context.Workouts.Any())
            return;     //database is initalized

        var workout = new Workout();
        workout.Name = "test";
        workout.Date = DateOnly.Parse(DateTime.Today.ToShortDateString());
        workout.Weight = 0;
        workout.Reps = 0;
        workout.Sets = 0;

        context.Add(workout);
        context.SaveChanges();
    }
}
    