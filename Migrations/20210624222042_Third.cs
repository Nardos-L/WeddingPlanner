using Microsoft.EntityFrameworkCore.Migrations;

namespace WeddingPlanner.Migrations
{
    public partial class Third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_weddings_Users_userId",
                table: "weddings");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "weddings",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_weddings_userId",
                table: "weddings",
                newName: "IX_weddings_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_weddings_Users_UserId",
                table: "weddings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_weddings_Users_UserId",
                table: "weddings");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "weddings",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_weddings_UserId",
                table: "weddings",
                newName: "IX_weddings_userId");

            migrationBuilder.AddForeignKey(
                name: "FK_weddings_Users_userId",
                table: "weddings",
                column: "userId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
