using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class LMS6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9885b67f-dc83-496e-95b9-426fc9b9535d"));

            migrationBuilder.RenameColumn(
                name: "CardHolderNumber",
                table: "CardInfos",
                newName: "CardHolderName");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "FirstName", "IsBlocked", "LastName", "PasswordHash", "PhoneNumber", "Role", "Username" },
                values: new object[] { new Guid("105e2cd0-4215-45a6-a143-7b05bae313d5"), new DateTime(2024, 12, 8, 3, 14, 15, 379, DateTimeKind.Utc).AddTicks(326), "Asadbek", false, "Shermatov", "AQAAAAIAAYagAAAAEKLSBtAoBBISJTK39fDKXhTYiEUEeGycR0O9N2KXmco5lF+/Ii4OUX4SFSl0QofREA==", "+998945631282", "admin", "spawn" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("105e2cd0-4215-45a6-a143-7b05bae313d5"));

            migrationBuilder.RenameColumn(
                name: "CardHolderName",
                table: "CardInfos",
                newName: "CardHolderNumber");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "FirstName", "IsBlocked", "LastName", "PasswordHash", "PhoneNumber", "Role", "Username" },
                values: new object[] { new Guid("9885b67f-dc83-496e-95b9-426fc9b9535d"), new DateTime(2024, 12, 7, 5, 10, 1, 195, DateTimeKind.Utc).AddTicks(9284), "Asadbek", false, "Shermatov", "AQAAAAIAAYagAAAAEEDKEcIpScHSee1kka6mmXRyudmo0yZqoXf9L16y2jHco7KXByoxfHXPQ7g4DtqecA==", "+998945631282", "admin", "spawn" });
        }
    }
}
