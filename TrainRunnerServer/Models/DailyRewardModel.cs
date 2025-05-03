using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TrainRunnerServer.Enum;

namespace TrainRunnerServer.Models.StaticDataModels;

public class DailyRewardModel
{
    [JsonConverter(typeof(StringEnumConverter))]
    public Resources Resource { get; set; }
    public int Count { get; set; }
}