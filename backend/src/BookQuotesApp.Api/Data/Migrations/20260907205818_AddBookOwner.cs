using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookQuotesApp.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddBookOwner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Books become user-owned. Any pre-existing rows have no owner and would
            // violate the new foreign key, so clear them first — the seeded catalogue
            // is now created per account at registration instead.
            migrationBuilder.Sql("DELETE FROM \"Books\";");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Books",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Books_UserId",
                table: "Books",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Users_UserId",
                table: "Books",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Users_UserId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_UserId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Books");
        }
    }
}
