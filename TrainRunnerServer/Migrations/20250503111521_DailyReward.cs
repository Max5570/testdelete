using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainRunnerServer.Migrations
{
    /// <inheritdoc />
    public partial class DailyReward : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastTimeRewardClaimed",
                table: "AspNetUsers",
                newName: "LastTimePassiveRewardClaimed");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastTimeDailyRewardClaimed",
                table: "AspNetUsers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastTimeDailyRewardClaimed",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "LastTimePassiveRewardClaimed",
                table: "AspNetUsers",
                newName: "LastTimeRewardClaimed");
        }
    }
}
