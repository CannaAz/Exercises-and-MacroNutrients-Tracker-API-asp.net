using api.Interfaces;
using api.Models;
using api;
using FakeItEasy;
using Microsoft.AspNetCore.Identity;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using api.Dtos.Nutrients;
using FakeItEasy.Sdk;

namespace api.tests;

public class NutrientsControllerTest
{
    private readonly UserManager<AppUser> _userManager;
    private readonly INutrientsRepository _nutrientRepository;

    public NutrientsControllerTest()
    {
        _userManager = A.Fake<UserManager<AppUser>>();
        _nutrientRepository = A.Fake<INutrientsRepository>();
    }

    [Fact]
    public void NutrientController_GetAll_ReturnsTask()
    {
        //Arrange
        string email = "";
        var NutrientsList = new List<Nutrients>();

        A.CallTo(() => _nutrientRepository.GetAllNutrientsAsync(email)).Returns(NutrientsList);
        NutrientsController NutrientController = new NutrientsController(_userManager, _nutrientRepository);

        //Act
        var result = NutrientController.GetAll();


        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(Task<IActionResult>));
    }

    [Fact]
    public void NutrientController_GetOne_ReturnsTask()
    {
        //Arrange
        int id = 0;
        string email = "";
        Nutrients nutrients = new Nutrients();

        A.CallTo(() => _nutrientRepository.GetByIdAsync(id, email)).Returns(nutrients);
        NutrientsController NutrientController = new NutrientsController(_userManager, _nutrientRepository);

        //Act
        var result = NutrientController.GetOne(id);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(Task<IActionResult>));
    }

    [Fact]
    public void NutrientController_CreateOne_ReturnsTask()
    {
        //Arrange
        Nutrients nutrients = new Nutrients();
        CreateNutrientDto nutrientDto = new CreateNutrientDto();

        A.CallTo(()=> _nutrientRepository.CreateNutrientAsync(nutrients)).Returns(nutrients);
        NutrientsController nutrientsController = new NutrientsController(_userManager, _nutrientRepository);

        //Act
        var result = nutrientsController.CreateOne(nutrientDto);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(Task<IActionResult>));
    }

    [Fact]
    public void NutrientController_Update_ReturnsTask()
    {
        //Arrange
        int id = 0;
        string email = "";
        UpdateNutrientDto nutrientDto = new UpdateNutrientDto();
        Nutrients nutrient = new Nutrients();

        A.CallTo(()=> _nutrientRepository.UpdateAsync(id, email, nutrientDto)).Returns(nutrient);
        NutrientsController nutrientsController = new NutrientsController(_userManager, _nutrientRepository);

        //Act
        var result = nutrientsController.Update(id, nutrientDto);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(Task<IActionResult>));
    }

    [Fact]
    public void NutrientController_Delete_ReturnsTask()
    {
        //Arrange
        int id = 0;
        string email = "";;

        A.CallTo(()=> _nutrientRepository.DeleteAsync(id, email)).Should().NotBeNull();
        NutrientsController nutrientsController = new NutrientsController(_userManager, _nutrientRepository);

        //Act
        var result = nutrientsController.DeleteOne(id);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(Task<IActionResult>));
    }
}
