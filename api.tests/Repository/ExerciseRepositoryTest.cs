using api.Dtos.Exercises;
using api.Models;
using api.Repository;
using FluentAssertions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.tests;

public class ExerciseRepositoryTest
{
    private List<string> exercisesNames = new List<string>()
    {
        "benchpress", "overpress shoulders",
        "DeadRows", "Deadlifts", "Weighted squats",
        "leg extension", "barbell row", "rear deltrow"
    };

    private async Task<AppDbContext> GetDatabase()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        .Options;
        var databaseCtx = new AppDbContext(options);
        databaseCtx.Database.EnsureCreated();

        Random random = new Random();

        if(await databaseCtx.Exercises.CountAsync() <= 0)
        {
            for (int i = 1; i <= 10; i++)
            {
                databaseCtx.Exercises.Add(
                    new Models.Exercise()
                    {
                        ExerciseName = exercisesNames[random.Next(0, exercisesNames.Count - 1)],
                        Weight = random.Next(10, 140),
                        Repetitions = random.Next(4,12),
                        Series = random.Next(4,12),
                        ExerciseBase = 0
                    }
                );
            }

            await databaseCtx.SaveChangesAsync();
        }

        return databaseCtx;
    }

    [Fact]
    public async void ExerciseRepository_GetByNameAsync_returnsExercise()
    {
        //Arrange
        string name = "bench press";
        AppDbContext databaseCtx = await GetDatabase();
        ExerciseRepository Repository = new ExerciseRepository(databaseCtx);

        //Act
        var result = Repository.GetByNameAsync(name);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<Task<Exercise>>();
    }

    [Fact]
    public async void ExerciseRepository_CreateExerciseAsync_ReturnsExercise()
    {
        //Arrange
        Exercise exercise = new Exercise();
        AppDbContext DatabaseCtx = await GetDatabase();
        ExerciseRepository exerciseRepository = new ExerciseRepository(DatabaseCtx);

        //Act
        var result = exerciseRepository.CreateExerciseAsync(exercise);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<Task<Exercise>>();
    }

    [Fact]
    public async void ExerciseRepository_UpdateAsync_ReturnsExercise()
    {
        //Arrange
        int id = 0;
        string email = "email";
        UpdateExerciseDto exerciseDto = new UpdateExerciseDto();
        AppDbContext DatabaseCtx = await GetDatabase();
        ExerciseRepository exerciseRepository = new ExerciseRepository(DatabaseCtx);

        //Act
        var result = exerciseRepository.UpdateAsync(id, email, exerciseDto);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<Task<Exercise>>();
    }

    [Fact]
    public async void ExerciseRepository_DeleteAsync_ReturnsExercise()
    {
        //Arrange
        int id = 0;
        string email = "email";
        AppDbContext DatabaseCtx = await GetDatabase();
        ExerciseRepository exerciseRepository = new ExerciseRepository(DatabaseCtx);

        //Act
        var result = exerciseRepository.DeleteAsync(id, email);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<Task<Exercise>>();
    }
}
