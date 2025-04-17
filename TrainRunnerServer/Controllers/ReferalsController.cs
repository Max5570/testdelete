using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TrainRunnerServer.Managers;
using TrainRunnerServer.Models;

namespace TrainRunnerServer.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("[controller]")]
public class ReferalsController : Controller
{
    private readonly UserManager<UserModel> _userManager;

    public ReferalsController(UserManager<UserModel> userManager)
    {
        _userManager = userManager;
    }
    
    [HttpPost("[action]")]
    public async Task<IActionResult> AddReferalTest([FromBody] int userId)
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
        {
            return NotFound();
        }

        var newRef = new ReferalModel();
        newRef.UserId = userId;
        user.Referals.Add(newRef);
        
        await _userManager.UpdateAsync(user);
        
        return Ok();
    }
    
    [HttpPost("[action]")]
    public async Task<IActionResult> GetReferals()
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user.Referals.Select(x => new
        {
            UserId = x.UserId
        }));
    }
    
    [HttpPost("[action]")]
    public async Task<IActionResult> ClearReferalsTest()
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
        {
            return NotFound();
        }

        user.Referals.Clear();
        
        await _userManager.UpdateAsync(user);
        
        return Ok();
    }
}