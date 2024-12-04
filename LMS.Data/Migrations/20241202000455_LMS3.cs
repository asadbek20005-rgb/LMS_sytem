using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class LMS3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9265cfa2-cd6a-4eb7-8567-77920a714ff0"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "FirstName", "IsBlocked", "LastName", "PasswordHash", "PhoneNumber", "Role", "Username" },
                values: new object[] { new Guid("e65009b4-25aa-448d-8aea-e8cf2ff5f633"), new DateTime(2024, 12, 2, 0, 4, 55, 0, DateTimeKind.Utc).AddTicks(892), "Asadbek", false, "Shermatov", "AQAAAAIAAYagAAAAED8lx42nPJuo4Uuoo7fMiIyvTX+KNBOYZ87inII26w5BGLIm7cauCe6IsDTjHiEeuA==", "+998945631282", "admin", "spawn" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e65009b4-25aa-448d-8aea-e8cf2ff5f633"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "FirstName", "IsBlocked", "LastName", "PasswordHash", "PhoneNumber", "Role", "Username" },
                values: new object[] { new Guid("9265cfa2-cd6a-4eb7-8567-77920a714ff0"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Asadbek", false, "Shermatov", "AQAAAAIAAYagAAAAENRBAdaajXn71cwNkqhc+da927usl3s8noUr6u5mTtoQ3yOzZfhBQaK3/7wGsRBnrA==", "+998945631282", "admin", "spawn" });
        }
    }
}
