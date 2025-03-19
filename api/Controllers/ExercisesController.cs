using api.Repository;
using Microsoft.AspNetCore.Mvc;
using api.Models;
using api.Interfaces;
using api.Dtos.Exercises;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;



namespace api;

[Route("api/Exercises")]
[ApiController]
public class ExercisesController : Controller
{

    private readonly UserManager<AppUser> _userManager;
    private readonly IExerciseRepository _exerciseRepository;

    public ExercisesController(UserManager<AppUser> userManager, IExerciseRepository exerciseRepository)
    {
        _exerciseRepository = exerciseRepository;
        _userManager = userManager;
    }


    [HttpGet("GetAll")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> GetAll()
    {
        if(!ModelState.IsValid) return BadRequest();


        // var data = User.Claims.FirstOrDefault().Value;
        var email = User.Claims.FirstOrDefault().Value;

        var exercises = await _exerciseRepository.GetAllUserExercisesAsync(email);
        return Ok(exercises);
    }

    [HttpGet("{id:int}")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> GetOne(int id)
    {
        if(!ModelState.IsValid) return BadRequest();
        var email = User.Claims.FirstOrDefault().Value;

        var exercise = await _exerciseRepository.GetByIdAsync(id, email);
        if(exercise == null) return NotFound();

        

        return Ok(exercise);
    }

    [HttpPost("CreateOne")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> CreateOne([FromBody] CreateExerciseDto exerciseDto)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);

        var userEmail = User.Claims.FirstOrDefault().Value;
        var AudienceUserId = (await _userManager.Users.FirstOrDefaultAsync(item => item.Email == userEmail)).Id;

        
        Exercise exercise = new Exercise
        {
            UserId = AudienceUserId,
            ExerciseName = exerciseDto.ExerciseName,
            Weight = exerciseDto.Weight,
            Repetitions = exerciseDto.Repetitions,
            Series = exerciseDto.Series,
            ExerciseBase = 0,
            CreationDate = DateOnly.FromDateTime(DateTime.Now)
        };

        var BaseExerciseResult = _exerciseRepository.GetByNameAsync(exerciseDto.ExerciseName);
        if(BaseExerciseResult.Result == null) exercise.ExerciseBase = 1;
        else exercise.ExerciseBase = 0;

        await _exerciseRepository.CreateExerciseAsync(exercise);
        
        return Ok(exercise);
    }

    [HttpPut]
    [Route("{id:int}")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateExerciseDto exerciseDto)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        var userEmail = User.Claims.FirstOrDefault().Value;

        var updatedExercises = await _exerciseRepository.UpdateAsync(id, userEmail, exerciseDto);
        if(updatedExercises == null) return NotFound();
        return Ok(updatedExercises);
    }

    [HttpDelete]
    [Route("{id:int}")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> DeleteOne([FromRoute] int id)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        var email = User.Claims.FirstOrDefault().Value;

        var exercise = await _exerciseRepository.DeleteAsync(id, email);
        if(exercise == null) return NotFound();

        return NoContent();
    }

    [HttpDelete("/DeleteAll")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> DeleteAll()
    {
        if(!ModelState.IsValid) return BadRequest();

        var email = User.Claims.FirstOrDefault().Value;
        _exerciseRepository.DeleteAllAsync(email);

        return NoContent();
    }




}
