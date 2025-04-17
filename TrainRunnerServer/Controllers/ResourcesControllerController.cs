using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TrainRunnerServer.Models;
using TrainRunnerServer.Querry;

namespace TrainRunnerServer.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("[controller]")]
public class ResourcesController : ControllerBase
{
    private readonly UserManager<UserModel> _userManager;
    
    public ResourcesController(UserManager<UserModel> userManager)
    {
        _userManager = userManager;
    }
    
    [HttpPost("[action]")]
    public async Task<IActionResult> SetGold([FromBody] SetPlayerGoldQuerryModel querryModel)
    {
        var user = await _userManager.GetUserAsync(User);
        
        if (user == null)
        {
            return NotFound();
        }
        
        // user.Gold = querryModel.Gold;
        //
        // await _userManager.UpdateAsync(user);
    
        return Ok(new
            {
                Gold = 100
            });
    }
    
    [HttpPost("[action]")]
    public async Task<IActionResult> Check([FromBody] string query)
    {
        var user = await _userManager.GetUserAsync(User);
        
        if (user == null)
        {
            return NotFound();
        }

        return Ok(new
        {
            Gold = 100
        });
    }
}