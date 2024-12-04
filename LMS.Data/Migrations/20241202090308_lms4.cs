using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class lms4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e65009b4-25aa-448d-8aea-e8cf2ff5f633"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "FirstName", "IsBlocked", "LastName", "PasswordHash", "PhoneNumber", "Role", "Username" },
                values: new object[] { new Guid("889ccd90-3980-4727-ab9f-69bf1df516bd"), new DateTime(2024, 12, 2, 9, 3, 7, 584, DateTimeKind.Utc).AddTicks(1825), "Asadbek", false, "Shermatov", "AQAAAAIAAYagAAAAECoDZ/4NAq4O1ZGz+ddwbzUXQFAdComYuPw1aW4TDEeLk+OT6ORxyDF1Q2xAI+vuCA==", "+998945631282", "admin", "spawn" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("889ccd90-3980-4727-ab9f-69bf1df516bd"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "FirstName", "IsBlocked", "LastName", "PasswordHash", "PhoneNumber", "Role", "Username" },
                values: new object[] { new Guid("e65009b4-25aa-448d-8aea-e8cf2ff5f633"), new DateTime(2024, 12, 2, 0, 4, 55, 0, DateTimeKind.Utc).AddTicks(892), "Asadbek", false, "Shermatov", "AQAAAAIAAYagAAAAED8lx42nPJuo4Uuoo7fMiIyvTX+KNBOYZ87inII26w5BGLIm7cauCe6IsDTjHiEeuA==", "+998945631282", "admin", "spawn" });
        }
    }
}
