using Microsoft.AspNetCore.Identity;
using TrainRunnerServer.Models.StaticDataModels;

namespace TrainRunnerServer.Models;

public class UserModel : IdentityUser
{
    public Train CurrentTrain;
    
    public virtual DateTime LastTimePassiveRewardClaimed { get; set; }
    public virtual DateTime LastTimeDailyRewardClaimed { get; set; }
    public virtual List<UserResourceModel> Resources { get; set; } = new ();
    public virtual List<ReferalModel> Referals { get; set; } = new ();
    public virtual PlayerSettingsModel Settings { get; set; } = new ();
}