using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TrainRunnerServer.Managers;
using TrainRunnerServer.Models;
using TrainRunnerServer.Models.StaticDataModels;

namespace TrainRunnerServer.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("[controller]")]
public class DailyRewardController : Controller
{
    private readonly UserManager<UserModel> _userManager;
    private readonly StaticDataManager _dataManager;

    public DailyRewardController(UserManager<UserModel> userManager, StaticDataManager dataManager)
    {
        _userManager = userManager;
        _dataManager = dataManager;
    }
    
    [HttpPost("[action]")]
    public async Task<IActionResult> GetDailyReward()
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
        {
            return NotFound();
        }

        var rewardModel = new List<DailyRewardModel>();
        
        if (user.LastTimeDailyRewardClaimed.Date != DateTime.UtcNow.Date)
        {
            user.LastTimeDailyRewardClaimed = DateTime.UtcNow;

            rewardModel = _dataManager.DailyRewards;
            
            foreach (var reward in _dataManager.DailyRewards)
            {
                user.AddResource(reward.Resource, reward.Count);
            }
            
            await _userManager.UpdateAsync(user);
        }
        
        return Ok(rewardModel);
    }
    
    [HttpPost("[action]")]
    public async Task<IActionResult> ResetDailyReward()
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
        {
            return NotFound();
        }

        user.LastTimeDailyRewardClaimed = DateTime.UtcNow - TimeSpan.FromDays(100);
        
        await _userManager.UpdateAsync(user);

        return Ok();
    }
}