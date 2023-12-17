using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HNIExceptionNotification.Core.Migrations
{
    public partial class configuremodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "MonitoringFailedHNITxnExs",
                newName: "MonitoringFailedHNITxnExs",
                newSchema: "dbo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "MonitoringFailedHNITxnExs",
                schema: "dbo",
                newName: "MonitoringFailedHNITxnExs");
        }
    }
}
