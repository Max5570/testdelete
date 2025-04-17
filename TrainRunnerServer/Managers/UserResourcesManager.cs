using TrainRunnerServer.Enum;
using TrainRunnerServer.Models;

namespace TrainRunnerServer.Managers;

public static class UserResourcesManager
{
    public static void AddResource(this UserModel user, Resources resource, int count)
    {
        if (!user.UserResources.Any(x => x.Resource == resource))
        {
            user.UserResources.Add(new UserResourceModel(resource, count));
            return;
        }

        var resourceData = user.UserResources.First(x => x.Resource == resource);
        resourceData.Count += count;
    }
}