using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Exercises;

public record class CreateExerciseDto
{
    [Required]
    public string? ExerciseName { get; set; }

    public int Weight { get; set; }
    [Required]
    public int Repetitions { get; set; }
    [Required]    
    public int Series { get; set; }
}
