using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exam.Data.Migrations
{
    public partial class FixOrderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SupervisorTechnicianName",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0bdba52e-e8ca-4481-84fe-ccd0142c33b6",
                column: "ConcurrencyStamp",
                value: "2c42e99d-53b0-471f-a157-76b489fdc097");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2bc2f661-4a73-4105-9f9f-93009a35ca26",
                column: "ConcurrencyStamp",
                value: "72a45aca-967e-4bac-8306-7e930072d1d1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9bf1gh11-joa8-24n3-343x-akdi20fjs932",
                column: "ConcurrencyStamp",
                value: "690ba3f6-c65a-45a4-9c11-f63a723124bb");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11e87c16-bc89-4393-a2b0-eb4e2debbd08",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7fadc277-79a2-45fd-819e-71a87d4eb8ed", new DateTime(2022, 5, 1, 14, 46, 58, 807, DateTimeKind.Local).AddTicks(9711), "AQAAAAEAACcQAAAAECkTLgo7AJuHaZs58azS0D5+Qxv/MVQanoG2OR487V570d7Q3JtTeekrv4HccdfMCw==", "8bfdc3b3-9d1f-49b7-9908-ce621add8f44" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SupervisorTechnicianName",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
    }
}
