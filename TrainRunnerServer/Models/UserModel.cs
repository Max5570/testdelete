using Microsoft.AspNetCore.Identity;

namespace TrainRunnerServer.Models;

public class UserModel : IdentityUser
{
    public int Gold { get; set; }
    public PassiveRewardModel PassiveRewardModel { get; set; } = new ();
    public PlayerSettingsModel SettingsModel { get; set; } = new ();
}