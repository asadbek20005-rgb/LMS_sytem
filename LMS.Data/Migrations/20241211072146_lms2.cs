using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class lms2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("4bf45f7a-7fd7-4e1c-b7d0-bc14d6149007"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "FirstName", "IsBlocked", "LastName", "PasswordHash", "PhoneNumber", "Role", "Username" },
                values: new object[] { new Guid("bd0771d1-1e21-4d29-8870-542db76249a9"), new DateTime(2024, 12, 11, 7, 21, 46, 384, DateTimeKind.Utc).AddTicks(8686), "Asadbek", false, "Shermatov", "AQAAAAIAAYagAAAAEF8PIiF6WNlr2SKtnTofKiOqg4Apb+gOthdPe0yqbJqhuL3/cEJDzXJircPip8G6GQ==", "+998945631282", "admin", "spawn" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bd0771d1-1e21-4d29-8870-542db76249a9"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "FirstName", "IsBlocked", "LastName", "PasswordHash", "PhoneNumber", "Role", "Username" },
                values: new object[] { new Guid("4bf45f7a-7fd7-4e1c-b7d0-bc14d6149007"), new DateTime(2024, 12, 10, 14, 17, 51, 972, DateTimeKind.Utc).AddTicks(5376), "Asadbek", false, "Shermatov", "AQAAAAIAAYagAAAAEHsxc7rvzXjX/Bmw2G/C0dPyQ9D0estbG8onKWSm25EDzPeRAKqodwXzxNTsbM9uYA==", "+998945631282", "admin", "spawn" });
        }
    }
}
