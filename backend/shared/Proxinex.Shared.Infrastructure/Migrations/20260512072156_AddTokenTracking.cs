using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proxinex.Shared.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTokenTracking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompletionTokens",
                table: "ChatHistories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "EstimatedCost",
                table: "ChatHistories",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "PromptTokens",
                table: "ChatHistories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalTokens",
                table: "ChatHistories",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompletionTokens",
                table: "ChatHistories");

            migrationBuilder.DropColumn(
                name: "EstimatedCost",
                table: "ChatHistories");

            migrationBuilder.DropColumn(
                name: "PromptTokens",
                table: "ChatHistories");

            migrationBuilder.DropColumn(
                name: "TotalTokens",
                table: "ChatHistories");
        }
    }
}
