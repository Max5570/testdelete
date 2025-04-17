using TrainRunnerServer.Enum;

namespace TrainRunnerServer.Models;

public class UserResourceModel
{
    public int Id { get; set; }
    public Resources Resource  { get; set; }
    public int Count  { get; set; }
    public string UserModelId  { get; set; }
    public virtual UserModel User  { get; set; }

    public UserResourceModel() {}
    
    public UserResourceModel(Resources resource, int count)
    {
        Resource = resource;
        Count = count;
    }
}