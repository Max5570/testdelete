using TrainRunnerServer.Enum;

namespace TrainRunnerServer.Models;

public class PassiveRewardModel
{
    public Resources Resource { get; set; }
    public int Count { get; set; }

    public PassiveRewardModel(Resources resource, int count)
    {
        Resource = resource;
        Count = count;
    }
}