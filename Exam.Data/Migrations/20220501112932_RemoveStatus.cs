using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exam.Data.Migrations
{
    public partial class RemoveStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_StatusNames_StatusId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "StatusNames");

            migrationBuilder.DropIndex(
                name: "IX_Orders_StatusId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0bdba52e-e8ca-4481-84fe-ccd0142c33b6",
                column: "ConcurrencyStamp",
                value: "53aa9566-b47d-466c-8267-3d957aea872f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2bc2f661-4a73-4105-9f9f-93009a35ca26",
                column: "ConcurrencyStamp",
                value: "abfec760-9f77-4978-9328-7e551c71b418");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9bf1gh11-joa8-24n3-343x-akdi20fjs932",
                column: "ConcurrencyStamp",
                value: "868bbc69-2153-415e-98d7-89f80312ffca");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11e87c16-bc89-4393-a2b0-eb4e2debbd08",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9ac110ff-b8f3-4d4d-9e1d-227e7aaf3ae5", new DateTime(2022, 5, 1, 14, 29, 32, 445, DateTimeKind.Local).AddTicks(9485), "AQAAAAEAACcQAAAAEOcgxjg469f+eRG+O/W0KPmLoc22IlmXcM3jxTRNz7LrTWDlN1kTqEu7h+3HE4vCaw==", "abefe780-7975-4ce0-b321-61a92b0ecf50" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "StatusNames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusNames", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0bdba52e-e8ca-4481-84fe-ccd0142c33b6",
                column: "ConcurrencyStamp",
                value: "fb626fb8-d26f-4e39-82df-7177a4801632");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2bc2f661-4a73-4105-9f9f-93009a35ca26",
                column: "ConcurrencyStamp",
                value: "f4a01bea-1cd7-436f-a967-31623b5a0817");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9bf1gh11-joa8-24n3-343x-akdi20fjs932",
                column: "ConcurrencyStamp",
                value: "57aa244a-bf9d-456b-8edf-79f2e4202b80");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11e87c16-bc89-4393-a2b0-eb4e2debbd08",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0359ffb6-8174-43dd-8681-5e214ff4300d", new DateTime(2022, 5, 1, 14, 17, 47, 508, DateTimeKind.Local).AddTicks(3482), "AQAAAAEAACcQAAAAEKhdA4+FHQBUHmZztJraYcfuHADsnoqB9u5YkW8a7BHHp6XkAOkynIZXRfgW5n530w==", "8574467c-f49a-4496-84cf-c8bee8d71d7f" });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_StatusId",
                table: "Orders",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_StatusNames_StatusId",
                table: "Orders",
                column: "StatusId",
                principalTable: "StatusNames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
