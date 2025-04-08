using api.Dtos.Account;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api;

[Route("api/Account")]
[ApiController]
public class AccountController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly SignInManager<AppUser> _signInManager;

    public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _signInManager = signInManager;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto userDto)
    {
        try
        {   
            if(!ModelState.IsValid) return BadRequest(ModelState);

            AppUser user = new AppUser
            {
                UserName = userDto.Username,
                Email = userDto.Email,
            };

            var createdUser = await _userManager.CreateAsync(user, userDto.Password);
            if(!createdUser.Succeeded) return StatusCode(500, createdUser.Errors);

            var roleResult = await _userManager.AddToRoleAsync(user, "User");
            if(!roleResult.Succeeded) return StatusCode(500, roleResult.Errors);

            return Ok(
                new NewUserDto
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user)
                }
            );
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] loginDto userDto)
    {
        try
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var userAccount = await _userManager.Users.FirstOrDefaultAsync(user => user.Email == userDto.Email);
            if(userAccount == null) return Unauthorized("Invalid Email");

            var passwordResult = await _signInManager.CheckPasswordSignInAsync(userAccount, userDto.Password, false);
            if(!passwordResult.Succeeded) return Unauthorized("User email not found and/or password Incorrect");

            return Ok(
                new NewUserDto
                {
                    UserName = userAccount.UserName,
                    Email = userAccount.Email,
                    Token = _tokenService.CreateToken(userAccount)
                }
            );

        }catch(Exception ex)
        {
            return StatusCode(500, ex);
        }
    }
}
