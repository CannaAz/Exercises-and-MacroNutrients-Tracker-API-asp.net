namespace api.Dtos.Exercises;

public record class ExerciseDto
{
    public int Id { get; set; }

    public string? ExerciseName { get; set; }

    public int Weight { get; set; }

    public int Repetitions { get; set; }

    public int Series { get; set; }

    public int ExerciseBase { get; set; }

    public DateOnly CreationDate { get; set; }
}
