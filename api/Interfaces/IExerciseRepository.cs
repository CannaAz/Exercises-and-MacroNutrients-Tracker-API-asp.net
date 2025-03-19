using api.Dtos.Exercises;
using api.Models;

namespace api.Interfaces;

public interface IExerciseRepository
{
    public Task<List<Exercise>> GetAllUserExercisesAsync(string email);
    public Task<Exercise> GetByIdAsync(int id, string email);
    public Task<Exercise> GetByNameAsync(string name);
    public Task<Exercise> CreateExerciseAsync(Exercise exerciseModel);
    public Task<Exercise> UpdateAsync(int id, string email, UpdateExerciseDto ExerciseDto);
    public Task<Exercise> DeleteAsync(int id, string email);

    public Task DeleteAllAsync(string email);

}
