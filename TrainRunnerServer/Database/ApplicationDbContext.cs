using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TrainRunnerServer.Models;
using TrainRunnerServer.Models.StaticDataModels;

namespace TrainRunnerServer.Database;

public class ApplicationDbContext : IdentityDbContext<UserModel>
{
    public DbSet<TrainModel> Trains { get; set; }
    public DbSet<TrainPassiveRewardModel> TrainRewards { get; set; }
    public DbSet<UserResourceModel> UserResources { get; set; }
    public DbSet<ReferalModel> Referals { get; set; }
    public DbSet<TelegramChatUserModel> TelegramReferalRelation { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<UserModel>()
            .OwnsOne(x => x.Settings);
        
        modelBuilder.Entity<UserModel>()
            .HasMany(x => x.Resources)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserModelId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // modelBuilder.Entity<UserModel>()
        //     .HasMany(x => x.Referals)
        //     .WithOne(x => x.User)
        //     .HasForeignKey(x => x.UserModelId)
        //     .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<ReferalModel>()
            .HasOne(x => x.User)
            .WithMany(x => x.Referals)
            .HasForeignKey(x => x.UserModelId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // modelBuilder.Entity<TelegramChatUserModel>().HasData(new TelegramChatUserModel { ReferalId = 0, RefererId = 0 });
    }
}