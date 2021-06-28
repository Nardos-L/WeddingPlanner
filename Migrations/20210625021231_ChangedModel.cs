using Microsoft.EntityFrameworkCore.Migrations;

namespace WeddingPlanner.Migrations
{
    public partial class ChangedModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WeddingId",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_WeddingId",
                table: "Users",
                column: "WeddingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_weddings_WeddingId",
                table: "Users",
                column: "WeddingId",
                principalTable: "weddings",
                principalColumn: "WeddingId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_weddings_WeddingId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_WeddingId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "WeddingId",
                table: "Users");
        }
    }
}
