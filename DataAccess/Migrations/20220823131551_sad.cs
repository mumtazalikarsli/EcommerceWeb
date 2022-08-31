using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class sad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "dbo",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 23, 16, 15, 50, 729, DateTimeKind.Local).AddTicks(6464),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 17, 16, 2, 25, 0, DateTimeKind.Local).AddTicks(9527));

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                schema: "dbo",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Token",
                schema: "dbo",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "TokenExpireDate",
                schema: "dbo",
                table: "Users",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                schema: "dbo",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Token",
                schema: "dbo",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TokenExpireDate",
                schema: "dbo",
                table: "Users");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "dbo",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 17, 16, 2, 25, 0, DateTimeKind.Local).AddTicks(9527),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 23, 16, 15, 50, 729, DateTimeKind.Local).AddTicks(6464));

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Users",
                columns: new[] { "Id", "Address", "CreatedDate", "CreatedUserId", "DateOfBirth", "Email", "FirstName", "Gender", "LastName", "Password", "UpdatedDate", "UpdatedUserId", "UserName" },
                values: new object[] { 1, "İSTANBUL", new DateTime(2022, 8, 17, 16, 2, 25, 1, DateTimeKind.Local).AddTicks(116), 1, new DateTime(1999, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "deneme@gmail.com", "deneme deneme", true, "aladden", "12345", null, null, "deneme" });
        }
    }
}
