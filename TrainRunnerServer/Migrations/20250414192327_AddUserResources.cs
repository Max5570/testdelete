using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TrainRunnerServer.Migrations
{
    /// <inheritdoc />
    public partial class AddUserResources : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gold",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "TrainType",
                table: "Trains",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "PassiveRewardModel_LastTimeClaimed",
                table: "AspNetUsers",
                newName: "LastTimeRewardClaimed");

            migrationBuilder.CreateTable(
                name: "UserResources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Resource = table.Column<int>(type: "integer", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false),
                    UserModelId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserResources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserResources_AspNetUsers_UserModelId",
                        column: x => x.UserModelId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserResources_UserModelId",
                table: "UserResources",
                column: "UserModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserResources");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Trains",
                newName: "TrainType");

            migrationBuilder.RenameColumn(
                name: "LastTimeRewardClaimed",
                table: "AspNetUsers",
                newName: "PassiveRewardModel_LastTimeClaimed");

            migrationBuilder.AddColumn<int>(
                name: "Gold",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
