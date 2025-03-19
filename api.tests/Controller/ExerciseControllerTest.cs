using api;
using api.Dtos.Exercises;
using api.Interfaces;
using api.Models;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace api.tests;

public class ExerciseControllerTest
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IExerciseRepository _exerciseRepository;

    public ExerciseControllerTest()
    {
        _userManager = A.Fake<UserManager<AppUser>>();
        _exerciseRepository = A.Fake<IExerciseRepository>();
    }

    [Fact]
    public void ExerciseController_GetAll_ReturnTASK()
    {
        //ARRANGE
        var exercisesList = A.Fake<List<Exercise>>();
        string email = "";
        A.CallTo(()=> _exerciseRepository.GetAllUserExercisesAsync(email)).Returns(exercisesList);

        var ExerciseController = new ExercisesController(_userManager, _exerciseRepository);

        //ACT
        var resutl = ExerciseController.GetAll();

        //ASSERT
        resutl.Should().NotBeNull();
        resutl.Should().BeOfType(typeof(Task<IActionResult>));
    }

    [Fact]
    public void ExerciseController_GetOne_ReturnsTASK()
    {
        //ARRANGE
        string email = "";
        int id = 0;
        var exercise = A.Fake<Exercise>();

        A.CallTo(() => _exerciseRepository.GetByIdAsync(id, email)).Returns(exercise);
        var ExerciseController = new ExercisesController(_userManager, _exerciseRepository);
        
        //ACT
        var result = ExerciseController.GetOne(id);

        //ASSERT
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(Task<IActionResult>));
    }

    [Fact]
    public void ExerciseController_CreateOne_ReturnsTask()
    {
        //Arrange

        var exercise = A.Fake<Exercise>();
        var exerciseCreate = A.Fake<CreateExerciseDto>();
        
        A.CallTo(() => _exerciseRepository.GetByNameAsync(exerciseCreate.ExerciseName)).Returns(exercise);
        A.CallTo(() => _exerciseRepository.CreateExerciseAsync(exercise)).Returns(exercise);
        var ExerciseController = new ExercisesController(_userManager, _exerciseRepository);

        //Act
        var result = ExerciseController.CreateOne(exerciseCreate);


        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(Task<IActionResult>));
    }

    [Fact]
    public void ExerciseController_Update_ReturnsTask()
    {
        string email = "";
        int id = 0;
        var exerciseDto = new UpdateExerciseDto();
        var exercise = new Exercise();

        A.CallTo(()=> _exerciseRepository.UpdateAsync(id, email, exerciseDto)).Returns(exercise);
        var ExerciseController = new ExercisesController(_userManager, _exerciseRepository);

        var result = ExerciseController.Update(id, exerciseDto);

        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(Task<IActionResult>));
    }

    [Fact]
    public void ExerciseController_DeleteOne_ReturnsTask()
    {
        string email = "";
        int id = 0;
        var exerciseDto = new UpdateExerciseDto();
        var exercise = new Exercise();

        A.CallTo(()=> _exerciseRepository.DeleteAsync(id, email)).Returns(exercise);
        var ExerciseController = new ExercisesController(_userManager, _exerciseRepository);

        var result = ExerciseController.DeleteOne(id);

        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(Task<IActionResult>));
    }

    [Fact]
    public void ExerciseController_DeleteAll_ReturnsTask()
    {
        string email = "";
        int id = 0;
        var exerciseDto = new UpdateExerciseDto();

        A.CallTo(()=> _exerciseRepository.DeleteAllAsync(email)).Should().NotBeNull();
        var ExerciseController = new ExercisesController(_userManager, _exerciseRepository);

        var result = ExerciseController.DeleteAll();

        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(Task<IActionResult>));
    }
}
