using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainRunnerServer.Models;

namespace TrainRunnerServer.Database;

public class Conf
{
    public class conftest : IEntityTypeConfiguration<ReferalModel>
    {
        public void Configure(EntityTypeBuilder<ReferalModel> modelBuilder)
        {
            modelBuilder.HasOne(x => x.User)
                .WithMany(x => x.Referals)
                .HasForeignKey(x => x.UserModelId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}