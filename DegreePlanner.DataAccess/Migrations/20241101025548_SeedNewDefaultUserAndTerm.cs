using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DegreePlanner.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedNewDefaultUserAndTerm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "5dcac689-ee44-4c44-bb70-f82a99a40ea1", 0, "df406ba9-1611-431c-9b4b-66f4fd22542e", "IdentityUser", "studenttester1@gmail.com", true, false, null, "STUDENTTESTER1@GMAIL.COM", "STUDENTTESTER1@GMAIL.COM", "AQAAAAIAAYagAAAAECiF3s8j842mUUA1O1tg4ERA4dzPVjQnWrJZP/wCqVu+YlJHi/+g4zYfwFLRo/Z7pA==", null, false, "23087f11-f2f7-4c14-bc84-b8ad611ba7f9", false, "studenttester1@gmail.com" });

            migrationBuilder.InsertData(
                table: "Terms",
                columns: new[] { "Id", "EndDate", "Name", "StartDate", "UserId" },
                values: new object[] { 1, new DateTime(2024, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Term 1", new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "5dcac689-ee44-4c44-bb70-f82a99a40ea1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Terms",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5dcac689-ee44-4c44-bb70-f82a99a40ea1");
        }
    }
}
