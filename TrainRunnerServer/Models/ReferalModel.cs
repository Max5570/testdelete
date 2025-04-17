namespace TrainRunnerServer.Models;

public class ReferalModel
{
    public int Id { get; set; }
    public string UserModelId  { get; set; }
    public virtual UserModel User  { get; set; }

    public int UserId { get; set; }
    
    public ReferalModel() { }
}