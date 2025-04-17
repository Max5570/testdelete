using TrainRunnerServer.Enum;
using TrainRunnerServer.Models;

namespace TrainRunnerServer.Managers;

public static class UserResourcesManager
{
    public static UserResourceModel AddResource(this UserModel user, Resources resource, int count)
    {
        if (!user.Resources.Any(x => x.Resource == resource))
        {
            user.Resources.Add(new UserResourceModel(resource, count));
        }

        var resourceData = user.Resources.First(x => x.Resource == resource);
        resourceData.Count += count;
        
        return resourceData;
    }
    
    public static UserResourceModel SetResource(this UserModel user, Resources resource, int count)
    {
        if (!user.Resources.Any(x => x.Resource == resource))
        {
            user.Resources.Add(new UserResourceModel(resource, count));
        }

        var resourceData = user.Resources.First(x => x.Resource == resource);
        resourceData.Count = count;

        return resourceData;
    }
    
    public static UserResourceModel GetResource(this UserModel user, Resources resource)
    {
        if (!user.Resources.Any(x => x.Resource == resource))
        {
            user.Resources.Add(new UserResourceModel(resource, 0));
        }

        return user.Resources.First(x => x.Resource == resource);
    }
}