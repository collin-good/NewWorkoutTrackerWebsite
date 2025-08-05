using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WorkoutTrackerWebsite.Models;

public class WorkoutDIT
{
    public WorkoutDIT() { }

    public WorkoutDIT(int id, string name, DateOnly date, int weight, int sets, int reps)
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
    [Range(1, 15, ErrorMessage = "Sets havea minimum value of 1 and a maximum value of 15")]
    public int Sets { get; set; } = 1;
    [Required]
    [Range(1, 150, ErrorMessage = "Reps havea minimum value of 1 and a maximum value of 150")]
    public int Reps { get; set; } = 1;

public static implicit operator WorkoutDIT(Workout w) => new WorkoutDIT(w.Id, w.Name, w.Date, w.Weight, w.Sets, w.Reps);

}