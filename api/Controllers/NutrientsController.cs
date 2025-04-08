using api.Dtos.Nutrients;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api;

public class NutrientsController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly INutrientsRepository _nutrientRepository;

    public NutrientsController(UserManager<AppUser> userManager, INutrientsRepository nutrientsRepository)
    {
        _userManager = userManager;
        _nutrientRepository = nutrientsRepository;
    }

    [HttpGet("GetAll")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> GetAll()
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);


        var email = User.Claims.FirstOrDefault().Value;
        var nutrients = await _nutrientRepository.GetAllNutrientsAsync(email);

        return Ok(nutrients);
    }

    [HttpGet("{id:int}")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> GetOne([FromRoute] int id)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);

        var email = User.Claims.FirstOrDefault().Value;
        var nutrient = await _nutrientRepository.GetByIdAsync(id, email);
        if(nutrient == null) return NotFound();

        return Ok(nutrient);
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> CreateOne([FromBody] CreateNutrientDto nutrientDto)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);

        var email = User.Claims.FirstOrDefault().Value;
        var AudienceId = (await _userManager.Users.FirstOrDefaultAsync(user => user.Email == email)).Id;

        var Nutrient = new Nutrients
        {
            UserId = AudienceId,
            Calories = nutrientDto.Calories,
            Carbohidrates = nutrientDto.Carbohidrates,
            Fats = nutrientDto.Fats,
            Proteins = nutrientDto.Proteins,
            Fiber = nutrientDto.Fiber,
            CurrentBodyWeight = nutrientDto.CurrentBodyWeight,
            CreationDate = DateOnly.FromDateTime(DateTime.Now)
        };

        await _nutrientRepository.CreateNutrientAsync(Nutrient);

        return Ok(Nutrient);
    }

    [HttpPut]
    [Route("{id:int}")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateNutrientDto nutrientDto)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);

        var email = User.Claims.FirstOrDefault().Value; 
        var nutrient = await _nutrientRepository.UpdateAsync(id, email, nutrientDto);
        if(nutrient == null) return NotFound();
        
        return Ok(nutrient);
    }

    [HttpDelete]
    [Route("{id:int}")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> DeleteOne(int id)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);

        var email = User.Claims.FirstOrDefault().Value;
        var nutrient = await _nutrientRepository.DeleteAsync(id, email);
        if(nutrient == null) return NotFound();

        return NoContent();
    }   
}
