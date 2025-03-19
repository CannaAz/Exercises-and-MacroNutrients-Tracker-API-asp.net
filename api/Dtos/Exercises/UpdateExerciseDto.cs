using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Exercises;

public record class UpdateExerciseDto
{
    [Required]
    public string? ExerciseName { get; set; }
    [Required]
    [Range(0, 500)]
    public int Weight { get; set; }
    [Required]
    [Range(1, 100)]
    public int Repetitions { get; set; }
    [Required]
    [Range(1, 30)]
    public int Series { get; set; }
}
