using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Comtrade.FinanceTrack.User.Repository.MSSQL.Migrations
{
    /// <inheritdoc />
    public partial class _240708_User_001_DML : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = "INSERT INTO [dbo].[User] ([Username] ,[Password] ,[Firstname] ,[Lastname] ,[IsDeleted]) VALUES ('emilija.jevremovic','ejevremovic',N'Emilija',N'Jevremović',0), ('milos.miletic','mmiletic',N'Miloš',N'Miletić',0), ('andrija.matijasevic','amatijasevic',N'Andrija',N'Matijašević',0), ('luka.petrovic','lpetrovic',N'Luka',N'Petrović',0), ('nemanja.milanovic','nmilanovic',N'Nemanja',N'Milanović',0), ('iman.veljan','iveljan',N'Iman',N'Veljan',0), ('ivona.jozic','ijozic',N'Ivona',N'Jozić',0), ('jovan.brzakovic','jbrzakovic',N'Jovan',N'Brzaković',0), ('katarina.mosic','kmosic',N'Katarina',N'Mošić',0), ('dušan.colic','dcolic',N'Dušan',N'Čolić',0), ('uroš.colic','ucolic',N'Uroš',N'Ćolić',0), ('nemanja.milovanovic','nmilovanovic',N'Nemanja',N'Milovanović',0), ('nikola.milašinovic','nmilašinovic',N'Nikola',N'Milašinović',0), ('nejra.adilovic','nadilovic',N'Nejra',N'Adilović',0), ('dalila.krslak','dkrslak',N'Dalila',N'Kršlak',0)";

            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sql = "DELETE FROM [dbo].[User]";

            migrationBuilder.Sql(sql);
        }
    }
}
