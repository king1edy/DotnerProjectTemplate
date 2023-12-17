using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HNIExceptionNotification.Core.Migrations
{
    public partial class changedreferenceId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MonitoringFailedHNITxnExs_MonitoringFailedHNITxns_FailedHNITxnId",
                schema: "dbo",
                table: "MonitoringFailedHNITxnExs");

            migrationBuilder.RenameColumn(
                name: "FailedHNITxnId",
                schema: "dbo",
                table: "MonitoringFailedHNITxnExs",
                newName: "MonitoringFailedHNITxnId");

            migrationBuilder.RenameIndex(
                name: "IX_MonitoringFailedHNITxnExs_FailedHNITxnId",
                schema: "dbo",
                table: "MonitoringFailedHNITxnExs",
                newName: "IX_MonitoringFailedHNITxnExs_MonitoringFailedHNITxnId");

            migrationBuilder.AddForeignKey(
                name: "FK_MonitoringFailedHNITxnExs_MonitoringFailedHNITxns_MonitoringFailedHNITxnId",
                schema: "dbo",
                table: "MonitoringFailedHNITxnExs",
                column: "MonitoringFailedHNITxnId",
                principalSchema: "dbo",
                principalTable: "MonitoringFailedHNITxns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MonitoringFailedHNITxnExs_MonitoringFailedHNITxns_MonitoringFailedHNITxnId",
                schema: "dbo",
                table: "MonitoringFailedHNITxnExs");

            migrationBuilder.RenameColumn(
                name: "MonitoringFailedHNITxnId",
                schema: "dbo",
                table: "MonitoringFailedHNITxnExs",
                newName: "FailedHNITxnId");

            migrationBuilder.RenameIndex(
                name: "IX_MonitoringFailedHNITxnExs_MonitoringFailedHNITxnId",
                schema: "dbo",
                table: "MonitoringFailedHNITxnExs",
                newName: "IX_MonitoringFailedHNITxnExs_FailedHNITxnId");

            migrationBuilder.AddForeignKey(
                name: "FK_MonitoringFailedHNITxnExs_MonitoringFailedHNITxns_FailedHNITxnId",
                schema: "dbo",
                table: "MonitoringFailedHNITxnExs",
                column: "FailedHNITxnId",
                principalSchema: "dbo",
                principalTable: "MonitoringFailedHNITxns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
