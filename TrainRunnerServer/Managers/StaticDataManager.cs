using Newtonsoft.Json;
using TrainRunnerServer.Models.StaticDataModels;

namespace TrainRunnerServer.Managers;

public class StaticDataManager
{
    public List<TrainModel> TrainsData;

    public StaticDataManager()
    {
        var jsonContent = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Resources", "train_data.json"));
        var trains = JsonConvert.DeserializeObject<List<TrainModel>>(jsonContent);
        
        TrainsData = trains;
    }
}