using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agency.Migrations
{
    public partial class tests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Socials_TeamId",
                table: "Socials",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Socials_Teams_TeamId",
                table: "Socials",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Socials_Teams_TeamId",
                table: "Socials");

            migrationBuilder.DropIndex(
                name: "IX_Socials_TeamId",
                table: "Socials");
        }
    }
}
