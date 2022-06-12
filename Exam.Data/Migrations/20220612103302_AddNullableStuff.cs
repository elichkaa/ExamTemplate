using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exam.Data.Migrations
{
    public partial class AddNullableStuff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_AspNetUsers_UserId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Rooms_RoomId",
                table: "Requests");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Requests",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "RoomId",
                table: "Requests",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_AspNetUsers_UserId",
                table: "Requests",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Rooms_RoomId",
                table: "Requests",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_AspNetUsers_UserId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Rooms_RoomId",
                table: "Requests");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Requests",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RoomId",
                table: "Requests",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0bdba52e-e8ca-4481-84fe-ccd0142c33b6",
                column: "ConcurrencyStamp",
                value: "95fb41b6-6065-4202-9d32-1e4a456d78ee");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2bc2f661-4a73-4105-9f9f-93009a35ca26",
                column: "ConcurrencyStamp",
                value: "df00ffde-2994-4bcb-93a9-06ae8b758c81");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11e87c16-bc89-4393-a2b0-eb4e2debbd08",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp" },
                values: new object[] { "740aa352-9493-4921-980e-6796d91eadc4", new DateTime(2022, 6, 12, 12, 28, 17, 967, DateTimeKind.Local).AddTicks(3449), "AQAAAAEAACcQAAAAEB4yo6lholx7dZv4/5L+EKTT31j9sC/DD7WbDmoNNb6pjHfjTh0YYBHofw6AdXURDA==", "6abb8911-55da-46fe-a4be-4c1a6e51da8c" });

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_AspNetUsers_UserId",
                table: "Requests",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Rooms_RoomId",
                table: "Requests",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
