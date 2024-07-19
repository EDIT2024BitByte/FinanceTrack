using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Comtrade.FinanceTrack.Budget.Repository.MSSQL.Migrations
{
    /// <inheritdoc />
    public partial class _240701_Budget_001_DDL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Budget",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Budget");
        }
    }
}
