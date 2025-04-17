using Microsoft.AspNetCore.Identity;
using TrainRunnerServer.Models.StaticDataModels;

namespace TrainRunnerServer.Models;

public class UserModel : IdentityUser
{
    public Train CurrentTrain;
    
    // use utc time!!!
    public DateTime LastTimeRewardClaimed { get; set; }
    public List<UserResourceModel> UserResources { get; set; } = new ();
    public PlayerSettingsModel SettingsModel { get; set; } = new ();
}