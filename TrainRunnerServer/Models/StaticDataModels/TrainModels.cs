using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using TrainRunnerServer.Enum;

namespace TrainRunnerServer.Models.StaticDataModels;

public class TrainModel
{
    public int Id { get; set; }
    [JsonConverter(typeof(StringEnumConverter<,,>))]
    public Train Type { get; set; }
    public List<TrainPassiveRewardModel> PassiveRewards { get; set; } = new();
}

public class TrainPassiveRewardModel
{
    public int Id { get; set; }
    public int CurrencyPerMinute { get; set; }
    [JsonConverter(typeof(StringEnumConverter<,,>))]
    public Resources ResourceType { get; set; }
    public int TrainModelId { get; set; }
    public TrainModel TrainModel { get; set; }
}

public enum Train
{
    TrainLevel1 = 1,
    TrainLevel2 = 2,
}