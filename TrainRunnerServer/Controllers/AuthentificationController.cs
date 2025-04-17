using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TrainRunnerServer.Database;
using TrainRunnerServer.Models;
using TrainRunnerServer.Querry;

namespace TrainRunnerServer.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthentificationController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;
    private readonly UserManager<UserModel> _userManager;
    private readonly SignInManager<UserModel> _signInManager;
    private readonly IConfiguration _config;
    
    public AuthentificationController(ApplicationDbContext dbContext, IConfiguration config, UserManager<UserModel> userManager, SignInManager<UserModel> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _dbContext = dbContext;
        _config = config;
    }
    
    [HttpPost("[action]")]
    public async Task<IActionResult> Register([FromBody] RegisterPlayerQuerryModel querry)
    {
        var user = await _userManager.FindByNameAsync(querry.UserName);
        
        if (user != null)
        {
            ModelState.AddModelError("Error", "Пользователь уже существует!");
            return BadRequest(ModelState);
        }
        
        var newUser = new UserModel() { UserName = querry.UserName };
        InitializeUser(newUser);

        try
        {
            var result = await _userManager.CreateAsync(newUser, querry.Password);
            
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("Error", $"{error.Code}, {error.Description}");
                }
                return BadRequest(ModelState);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        
        
        return Ok();
    }

    private void InitializeUser(UserModel userModel)
    {
        userModel.LastTimeRewardClaimed = DateTime.UtcNow;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Login([FromBody] RegisterPlayerQuerryModel querry)
    {
        var user = await _userManager.FindByNameAsync(querry.UserName);

        if (user == null)
        {
            ModelState.AddModelError("Error", "Пользователь не найден!");
            return BadRequest(ModelState);
        }
        
        var result = await _signInManager.CheckPasswordSignInAsync(user, querry.Password, false);
        
        if (!result.Succeeded)
        {
            ModelState.AddModelError("Error", "Ошибка при входе в аккаунт");
            return BadRequest(ModelState);
        }
        
        var token = GenerateJwtToken(user);
        
        return Ok(new
        {
            Token = token
        });
    }
    
    private string GenerateJwtToken(UserModel user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Auth:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim("UserId", user.Id.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _config["Auth:Issuer"],
            audience: _config["Auth:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}