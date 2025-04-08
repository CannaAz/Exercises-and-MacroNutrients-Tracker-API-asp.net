using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory.Storage.Internal;
using api.Repository;
using FluentAssertions;
using api.Models;
using api.Dtos.Nutrients;

namespace api.tests;

public class NutrientRepositoryTest
{
    public async Task<AppDbContext> GetDatabase()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
        var databaseCtx = new AppDbContext(options);
        databaseCtx.Database.EnsureCreated();

        Random random = new Random();

        if(await databaseCtx.Nutrients.CountAsync() <= 0)
        {
            for(int i = 0; i < 10; i++)
            {
                databaseCtx.Nutrients.Add(
                    new Models.Nutrients()
                    {
                        Calories = random.Next(1800, 3000),
                        Carbohidrates = random.Next(180, 235),
                        Fats = random.Next(100, 230),
                        Proteins = random.Next(80, 180),
                        Fiber = random.Next(10, 40),
                        CurrentBodyWeight = random.Next(50, 150),
                    }
                );
            }
            databaseCtx.SaveChanges();
        }

        return databaseCtx;
    }

    [Fact]
    public async void NutrientRepository_GetallNutrientsAsync_ReturnsNutrient()
    {
        //Arrange
        string email = "email";
        AppDbContext databaseCtx = await GetDatabase();
        NutrientRepository nutrientRepository = new NutrientRepository(databaseCtx);

        //Act
        var result = nutrientRepository.GetAllNutrientsAsync(email);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<Task<List<Nutrients>>>();

    }

    [Fact]
    public async void NutrientRepository_GetByIdAsync_ReturnsNutrient()
    {
        //Arrange
        int id = 0;
        string email = "email";
        AppDbContext databaseCtx = await GetDatabase();
        NutrientRepository repository = new NutrientRepository(databaseCtx);

        //Act
        var result = repository.GetByIdAsync(id, email);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<Task<Nutrients>>();
    }

    [Fact]
    public async void NutrientRepository_CreateNutrientAsync_ReturnsNutrient()
    {
        Nutrients nutrient = new Nutrients();
        AppDbContext databaseCtx = await GetDatabase();
        NutrientRepository repository = new NutrientRepository(databaseCtx);

        var result = repository.CreateNutrientAsync(nutrient);

        result.Should().NotBeNull();
        result.Should().BeOfType<Task<Nutrients>>();
    }

    [Fact]
    public async void NutrientRepository_UpdateAsync_ReturnsNutrient()
    {
        //Arrange
        int id = 0;
        string email = "email";
        UpdateNutrientDto nutrientDto = new UpdateNutrientDto();

        AppDbContext databaseCtx = await GetDatabase();
        NutrientRepository repository = new NutrientRepository(databaseCtx);

        //Act
        var result = repository.UpdateAsync(id, email, nutrientDto);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<Task<Nutrients>>();
    }

    [Fact]
    public async void NutrientRepository_DeleteAsync_ReturnsNutrient()
    {
        //Arrange
        int id = 0;
        string email = "email";

        AppDbContext databaseCtx = await GetDatabase();
        NutrientRepository repository = new NutrientRepository(databaseCtx);

        //Act
        var result = repository.DeleteAsync(id, email);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<Task<Nutrients>>();
    }
}
