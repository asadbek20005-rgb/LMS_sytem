using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class LMS2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("49440ca5-21f8-4c7e-86c8-b2591f882a4b"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Code", "CreatedDate", "FirstName", "IsBlocked", "LastName", "PasswordHash", "PhoneNumber", "Role", "Username" },
                values: new object[] { new Guid("452e48dc-dd4b-4154-9f2f-1348355ee973"), 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Asadbek", false, "Shermatov", "AQAAAAIAAYagAAAAEEhAX3aKA+QykspgZKN7op4iwiGP/8ogw94PnIGn0Nh0tARMHCnAkxTiIDrKP8Ancg==", "+998945631282", "admin", "spawn" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("452e48dc-dd4b-4154-9f2f-1348355ee973"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Code", "CreatedDate", "FirstName", "IsBlocked", "LastName", "PasswordHash", "PhoneNumber", "Role", "Username" },
                values: new object[] { new Guid("49440ca5-21f8-4c7e-86c8-b2591f882a4b"), 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Asadbek", false, "Shermatov", "AQAAAAIAAYagAAAAEFb20H3UDk8xCwy+rIDW/wdZddU3tRgCObFD62D1fCxH3g4aTLBIqZDrVvxajSd/2g==", "+998945631282", "admin", null });
        }
    }
}
