using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainRunnerServer.Migrations
{
    /// <inheritdoc />
    public partial class TrainUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentTrain",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentTrain",
                table: "AspNetUsers");
        }
    }
}
