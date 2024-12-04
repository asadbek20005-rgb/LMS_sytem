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
                keyValue: new Guid("076426af-7a8f-4049-a373-38777eeaecb6"));

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Users");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "FirstName", "IsBlocked", "LastName", "PasswordHash", "PhoneNumber", "Role", "Username" },
                values: new object[] { new Guid("9265cfa2-cd6a-4eb7-8567-77920a714ff0"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Asadbek", false, "Shermatov", "AQAAAAIAAYagAAAAENRBAdaajXn71cwNkqhc+da927usl3s8noUr6u5mTtoQ3yOzZfhBQaK3/7wGsRBnrA==", "+998945631282", "admin", "spawn" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9265cfa2-cd6a-4eb7-8567-77920a714ff0"));

            migrationBuilder.AddColumn<int>(
                name: "Code",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Code", "CreatedDate", "FirstName", "IsBlocked", "LastName", "PasswordHash", "PhoneNumber", "Role", "Username" },
                values: new object[] { new Guid("076426af-7a8f-4049-a373-38777eeaecb6"), 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Asadbek", false, "Shermatov", "AQAAAAIAAYagAAAAEFlfMoQburgqSiFfohs2/H2WqBRfJKuRQVX8mx6c6kZTYbI1K8120VWf43Wt7ldjSw==", "+998945631282", "admin", "spawn" });
        }
    }
}
