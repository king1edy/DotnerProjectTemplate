using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HNIExceptionNotification.Core.Migrations
{
    public partial class InitDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "MonitoringFailedHNITxns",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserMessage = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNo = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonitoringFailedHNITxns", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MonitoringFailedHNITxnExs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FailedHNITxnId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsProcessed = table.Column<bool>(type: "bit", nullable: false),
                    IsEmailSent = table.Column<bool>(type: "bit", nullable: false),
                    RetryCount = table.Column<int>(type: "int", nullable: false),
                    DateEmailSent = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonitoringFailedHNITxnExs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MonitoringFailedHNITxnExs_MonitoringFailedHNITxns_FailedHNITxnId",
                        column: x => x.FailedHNITxnId,
                        principalSchema: "dbo",
                        principalTable: "MonitoringFailedHNITxns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MonitoringFailedHNITxnExs_FailedHNITxnId",
                table: "MonitoringFailedHNITxnExs",
                column: "FailedHNITxnId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MonitoringFailedHNITxnExs");

            migrationBuilder.DropTable(
                name: "MonitoringFailedHNITxns",
                schema: "dbo");
        }
    }
}
