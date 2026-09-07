using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookQuotesApp.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddQuoteBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Book",
                table: "Quotes",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Book",
                table: "Quotes");
        }
    }
}
