using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TrainRunnerServer.Enum;
using TrainRunnerServer.Models;
using TrainRunnerServer.Models.StaticDataModels;

namespace TrainRunnerServer.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("[controller]")]
public class PassiveResourcesController : Controller
{
    private readonly UserManager<UserModel> _userManager;
    
    public PassiveResourcesController(UserManager<UserModel> userManager)
    {
        _userManager = userManager;
    }
    
    
}