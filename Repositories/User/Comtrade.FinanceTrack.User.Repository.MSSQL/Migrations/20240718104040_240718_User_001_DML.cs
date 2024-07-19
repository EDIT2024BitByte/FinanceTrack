using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Comtrade.FinanceTrack.User.Repository.MSSQL.Migrations
{
    /// <inheritdoc />
    public partial class _240718_User_001_DML : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = "INSERT INTO [dbo].[User] ([Username] ,[Password] ,[Firstname] ,[Lastname] ,[IsDeleted]) VALUES ('marko.markovic','mmarkovic',N'Marko',N'Marković',0), ('nadja.lazarevic','nlazarevic',N'Nađa',N'Lazarević',0)";

            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
