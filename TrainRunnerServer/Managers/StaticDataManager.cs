using Newtonsoft.Json;
using TrainRunnerServer.Models.StaticDataModels;

namespace TrainRunnerServer.Managers;

public class StaticDataManager
{
    public List<TrainModel> TrainsData;
    public List<DailyRewardModel> DailyRewards;

    public StaticDataManager()
    {
        var jsonContent = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "Resources", "TrainData.json"));
        TrainsData = JsonConvert.DeserializeObject<List<TrainModel>>(jsonContent);
        
        
        var dailyRewardsJson = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "Resources", "DailyRewardData.json"));
        DailyRewards = JsonConvert.DeserializeObject<List<DailyRewardModel>>(dailyRewardsJson);
    }
}