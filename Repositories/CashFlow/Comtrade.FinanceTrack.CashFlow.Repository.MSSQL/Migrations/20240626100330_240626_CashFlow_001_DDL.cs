using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Comtrade.FinanceTrack.CashFlow.Repository.MSSQL.Migrations
{
    /// <inheritdoc />
    public partial class _240626_CashFlow_001_DDL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Income_Categories_CategoryId",
                table: "Income");

            migrationBuilder.DropIndex(
                name: "IX_Income_CategoryId",
                table: "Income");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Income");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CategoryId",
                table: "Income",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Income_CategoryId",
                table: "Income",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Income_Categories_CategoryId",
                table: "Income",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
