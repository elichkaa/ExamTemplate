using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exam.Data.Migrations
{
    public partial class afsdf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0bdba52e-e8ca-4481-84fe-ccd0142c33b6",
                column: "ConcurrencyStamp",
                value: "8bccce28-5735-42e5-9164-6fd9d521c4af");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2bc2f661-4a73-4105-9f9f-93009a35ca26",
                column: "ConcurrencyStamp",
                value: "fb875091-8d92-4c99-a3ff-a09427a35694");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11e87c16-bc89-4393-a2b0-eb4e2debbd08",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0be2962c-374c-493a-8a67-3317a1ed5e11", new DateTime(2022, 6, 12, 14, 29, 13, 228, DateTimeKind.Local).AddTicks(143), "AQAAAAEAACcQAAAAEPh2S4BMe9J+OUCgjwpi1l6H8yhkiCvpILWORXevYWNj8t80DqxvPzLxUxwC3b71VQ==", "dc817666-3461-4331-a4ae-f210b52749ab" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
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
                value: "912268bc-e997-434f-a892-6a8cd3e23a25");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2bc2f661-4a73-4105-9f9f-93009a35ca26",
                column: "ConcurrencyStamp",
                value: "541281b2-7099-4776-8ba1-c5ebdebee395");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11e87c16-bc89-4393-a2b0-eb4e2debbd08",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3bcafa6f-642f-47fe-912b-77079c7aaee6", new DateTime(2022, 6, 12, 13, 33, 1, 781, DateTimeKind.Local).AddTicks(3360), "AQAAAAEAACcQAAAAEHovf7lYq/sJmLV6XxicMjiEnRUjDBYqhJal9nEYooO9pUtJmdP/S7OXgVWyltN5vA==", "1ab9ff7d-a05d-4c78-affa-205edceef48c" });
        }
    }
}
