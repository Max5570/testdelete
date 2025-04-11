using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TrainRunnerServer.Models;
using TrainRunnerServer.Models.StaticDataModels;

namespace TrainRunnerServer.Database;

public class ApplicationDbContext : IdentityDbContext<UserModel>
{
    public DbSet<TrainModel> Trains { get; set; }
    public DbSet<TrainPassiveRewardModel> TrainRewards { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<UserModel>()
            .OwnsOne(x => x.SettingsModel);
        
        modelBuilder.Entity<UserModel>()
            .OwnsOne(x => x.PassiveRewardModel);
    }
}