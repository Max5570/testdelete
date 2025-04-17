using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TrainRunnerServer.Migrations
{
    /// <inheritdoc />
    public partial class Referals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SettingsModel_Volume",
                table: "AspNetUsers",
                newName: "Settings_Volume");

            migrationBuilder.RenameColumn(
                name: "SettingsModel_IsSoundOn",
                table: "AspNetUsers",
                newName: "Settings_IsSoundOn");

            migrationBuilder.CreateTable(
                name: "Referals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserModelId = table.Column<string>(type: "text", nullable: false),
                    IconId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Referals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Referals_AspNetUsers_UserModelId",
                        column: x => x.UserModelId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Referals_UserModelId",
                table: "Referals",
                column: "UserModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Referals");

            migrationBuilder.RenameColumn(
                name: "Settings_Volume",
                table: "AspNetUsers",
                newName: "SettingsModel_Volume");

            migrationBuilder.RenameColumn(
                name: "Settings_IsSoundOn",
                table: "AspNetUsers",
                newName: "SettingsModel_IsSoundOn");
        }
    }
}
