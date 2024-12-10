using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class LMS5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardInfos_Users_UserId",
                table: "CardInfos");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("889ccd90-3980-4727-ab9f-69bf1df516bd"));

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "CardInfos",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "FirstName", "IsBlocked", "LastName", "PasswordHash", "PhoneNumber", "Role", "Username" },
                values: new object[] { new Guid("9885b67f-dc83-496e-95b9-426fc9b9535d"), new DateTime(2024, 12, 7, 5, 10, 1, 195, DateTimeKind.Utc).AddTicks(9284), "Asadbek", false, "Shermatov", "AQAAAAIAAYagAAAAEEDKEcIpScHSee1kka6mmXRyudmo0yZqoXf9L16y2jHco7KXByoxfHXPQ7g4DtqecA==", "+998945631282", "admin", "spawn" });

            migrationBuilder.AddForeignKey(
                name: "FK_CardInfos_Users_UserId",
                table: "CardInfos",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardInfos_Users_UserId",
                table: "CardInfos");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9885b67f-dc83-496e-95b9-426fc9b9535d"));

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "CardInfos",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "FirstName", "IsBlocked", "LastName", "PasswordHash", "PhoneNumber", "Role", "Username" },
                values: new object[] { new Guid("889ccd90-3980-4727-ab9f-69bf1df516bd"), new DateTime(2024, 12, 2, 9, 3, 7, 584, DateTimeKind.Utc).AddTicks(1825), "Asadbek", false, "Shermatov", "AQAAAAIAAYagAAAAECoDZ/4NAq4O1ZGz+ddwbzUXQFAdComYuPw1aW4TDEeLk+OT6ORxyDF1Q2xAI+vuCA==", "+998945631282", "admin", "spawn" });

            migrationBuilder.AddForeignKey(
                name: "FK_CardInfos_Users_UserId",
                table: "CardInfos",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
