using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TrainRunnerServer.Enum;
using TrainRunnerServer.Managers;
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
    public async Task<IActionResult> SetResource([FromBody] ResourceQuerryModel querry)
    {
        var user = await _userManager.GetUserAsync(User);
        
        if (user == null)
        {
            return NotFound();
        }
        
        var resource = user.SetResource((Resources)querry.Resource, querry.Value);

        await _userManager.UpdateAsync(user);

        return Ok(new
        {
            Resource = resource.Resource,
            Value = resource.Count
        });
    }
    
    [HttpPost("[action]")]
    public async Task<IActionResult> ChangeResource([FromBody] ResourceQuerryModel querry)
    {
        var user = await _userManager.GetUserAsync(User);
        
        if (user == null)
        {
            return NotFound();
        }
        
        var resource = user.AddResource((Resources)querry.Resource, querry.Value);

        await _userManager.UpdateAsync(user);

        return Ok(new
        {
            Resource = resource.Resource,
            Value = resource.Count
        });
    }
    
    [HttpPost("[action]")]
    public async Task<IActionResult> GetResource([FromBody] int resourceType)
    {
        var user = await _userManager.GetUserAsync(User);
        
        if (user == null)
        {
            return NotFound();
        }
        
        var resource = user.GetResource((Resources)resourceType);

        return Ok(new
        {
            Resource = resource.Resource,
            Value = resource.Count
        });
    }
}