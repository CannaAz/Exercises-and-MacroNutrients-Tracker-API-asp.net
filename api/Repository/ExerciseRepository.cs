using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using api.Dtos.Exercises;

namespace api.Repository;

public class ExerciseRepository : IExerciseRepository
{
    private readonly AppDbContext _context;

    public ExerciseRepository(AppDbContext context)
    {
        _context = context;
    }
    

    public async Task<List<Exercise>> GetAllUserExercisesAsync(string email)
    {
        var userId = await GetUserId(email);
        var exercises = await _context.Exercises.Where(item => item.UserId == userId).ToListAsync();
        return exercises;
    }

    public async Task<Exercise> GetByNameAsync(string name)
    {
        return await _context.Exercises.FirstOrDefaultAsync(item => item.ExerciseName == name);
    }
    public async Task<Exercise> GetByIdAsync(int id, string email)
    {
        var userId = await GetUserId(email);
        var exercise = await _context.Exercises.FirstOrDefaultAsync(item => item.Id == id && item.UserId == userId);
        return exercise;
    }
    public async Task<Exercise> CreateExerciseAsync(Exercise exerciseModel)
    {
        await _context.Exercises.AddAsync(exerciseModel);
        _context.SaveChanges();
        return exerciseModel;
    }
    public async Task<Exercise> UpdateAsync(int id, string email, UpdateExerciseDto ExerciseDto)
    {
        var userId = await GetUserId(email);
        var currentExercise = await _context.Exercises.FirstOrDefaultAsync(item => item.Id == id && item.UserId == userId);
        if(currentExercise == null) return null;

        currentExercise.ExerciseName = ExerciseDto.ExerciseName;
        currentExercise.Weight = ExerciseDto.Weight;
        currentExercise.Repetitions = ExerciseDto.Repetitions;
        currentExercise.Series = ExerciseDto.Series;

        await _context.SaveChangesAsync();
        return currentExercise;
    }
    public async Task<Exercise> DeleteAsync(int id, string email)
    {
        var userId = await GetUserId(email);
        var Exercise = await _context.Exercises.FirstOrDefaultAsync(item => item.Id == id && item.UserId == userId);
        if(Exercise == null) return null;

        _context.Exercises.Remove(Exercise);
        _context.SaveChanges();
        return Exercise;
    }

    public async Task DeleteAllAsync(string email)
    {
        var userId = await GetUserId(email);
        var ExercisesToDelete = await _context.Exercises.Where(item => item.UserId == userId).ToListAsync();
        _context.Exercises.RemoveRange(ExercisesToDelete);
        _context.SaveChanges();
    }

    private async Task<string> GetUserId(string email)
    {
        return (await _context.Users.FirstOrDefaultAsync(item => item.Email == email)).Id;
    }
}
