using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Comtrade.FinanceTrack.CashFlow.Repository.MSSQL.Migrations
{
    /// <inheritdoc />
    public partial class _240702_CashFlow_001_DML : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = "INSERT INTO [dbo].[Categories] ([Name],[IsDeleted]) VALUES ('Rent and utilities', 0),('Food and drink', 0),('Transportation', 0),('Health', 0),('Entertainment', 0),('Gifts', 0),('Clothing', 0),('Other', 0)";

            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sql = "DELETE FROM [dbo].[Categories]";

            migrationBuilder.Sql(sql);
        }
    }
}
