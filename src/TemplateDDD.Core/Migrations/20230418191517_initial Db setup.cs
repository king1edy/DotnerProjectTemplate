using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HNIExceptionNotification.Core.Migrations
{
    /// <inheritdoc />
    public partial class initialDbsetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MonitoringFailedHNITxnExs",
                schema: "dbo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MonitoringFailedHNITxnExs",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MonitoringFailedHNITxnId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateEmailSent = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false),
                    IsEmailSent = table.Column<bool>(type: "bit", maxLength: 10, nullable: false),
                    IsProcessed = table.Column<bool>(type: "bit", maxLength: 10, nullable: false),
                    RetryCount = table.Column<int>(type: "int", maxLength: 2147483647, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonitoringFailedHNITxnExs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MonitoringFailedHNITxnExs_MonitoringFailedHNITxns_MonitoringFailedHNITxnId",
                        column: x => x.MonitoringFailedHNITxnId,
                        principalSchema: "dbo",
                        principalTable: "MonitoringFailedHNITxns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MonitoringFailedHNITxnExs_MonitoringFailedHNITxnId",
                schema: "dbo",
                table: "MonitoringFailedHNITxnExs",
                column: "MonitoringFailedHNITxnId");
        }
    }
}
