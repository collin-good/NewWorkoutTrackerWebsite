using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WorkoutTrackerWebsite.Models;

public class Workout
{
    public Workout() { }

    public Workout(int id, string name, DateOnly date, int weight, int sets, int reps)
    {
        Id = id;
        Name = name;
        Date = date;
        Weight = weight;
        Sets = sets;
        Reps = reps;
    }
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = default!;
    [Required]
    public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Today);
    [Required]
    public int Weight { get; set; }
    [Required]
    [Range(1, 15, ErrorMessage = "Sets have a minimum value of 1 and a maximum value of 15")]
    public int Sets { get; set; } = 1;
    [Required]
    [Range(1, 150, ErrorMessage = "Reps have a minimum value of 1 and a maximum value of 150")]
    public int Reps { get; set; } = 1;

    public override bool Equals(object? obj)
    {
        if (obj is Workout)
        {
            var that = obj as Workout;

            return Id == that!.Id &&
                   Name.Equals(that.Name) &&
                   Date == that.Date &&
                   Weight == that.Weight &&
                   Sets == that.Sets &&
                   Reps == that.Reps;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}