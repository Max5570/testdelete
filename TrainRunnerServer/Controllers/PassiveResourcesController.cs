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
public class PassiveResourcesController : Controller
{
    private readonly UserManager<UserModel> _userManager;
    private readonly StaticDataManager _dataManager;

    public PassiveResourcesController(UserManager<UserModel> userManager, StaticDataManager dataManager)
    {
        _userManager = userManager;
        _dataManager = dataManager;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> GetReward([FromBody] string data)
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
        {
            return NotFound();
        }

        var rewardData = CalculateReward(user);
        
        foreach (var reward in rewardData)
        {
            user.AddResource(reward.Resource, reward.Count);
        }
        
        user.LastTimeRewardClaimed = DateTime.UtcNow;

        await _userManager.UpdateAsync(user);
        
        return Ok(rewardData);
    }
    
    [HttpPost("[action]")]
    public async Task<IActionResult> CheckReward([FromBody] string header)
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
        {
            return NotFound();
        }

        var rewardData = CalculateReward(user);
        
        return Ok(rewardData);
    }
    
    [HttpPost("[action]")]
    public async Task<IActionResult> AddTimeTest([FromBody] string header)
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
        {
            return NotFound();
        }

        user.LastTimeRewardClaimed -= new TimeSpan(0, 0,10,0);
        
        await _userManager.UpdateAsync(user);
        
        return Ok();
    }

    private List<PassiveRewardModel> CalculateReward(UserModel user)
    {
        var trainData = _dataManager.TrainsData.First(x => x.Type == user.CurrentTrain);

        var rewardClaimed = user.LastTimeRewardClaimed;
        var timeDifference = DateTime.UtcNow - rewardClaimed;
        var minutesPast = timeDifference.Minutes;

        var rewardData = new List<PassiveRewardModel>();
        foreach (var reward in trainData.PassiveRewards)
        {
            var count = reward.CurrencyPerMinute * minutesPast;
            
            rewardData.Add(new PassiveRewardModel(reward.ResourceType, count));
        }

        return rewardData;
    }
}