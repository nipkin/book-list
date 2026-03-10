using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookList.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Quotes");

            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "Quotes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_AppUserId",
                table: "Quotes",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_AppUsers_AppUserId",
                table: "Quotes",
                column: "AppUserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_AppUsers_AppUserId",
                table: "Quotes");

            migrationBuilder.DropIndex(
                name: "IX_Quotes_AppUserId",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Quotes");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Quotes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
