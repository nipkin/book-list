using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookList.Api.Migrations
{
    /// <inheritdoc />
    public partial class RenameEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "AppUsers",
                newName: "Username");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "AppUsers",
                newName: "UserName");
        }
    }
}
