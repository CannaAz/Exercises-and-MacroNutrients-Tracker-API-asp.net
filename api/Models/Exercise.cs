using System.ComponentModel.DataAnnotations;

namespace api.Models;

public class Exercise
{
    public int Id { get; set; }

    public string? UserId{ get; set; }
    
    public string? ExerciseName { get; set; }

    public int Weight { get; set; }

    public int Repetitions { get; set; }

    public int Series { get; set; }

    public int ExerciseBase { get; set; }

    public DateOnly CreationDate { get; set; }
}
