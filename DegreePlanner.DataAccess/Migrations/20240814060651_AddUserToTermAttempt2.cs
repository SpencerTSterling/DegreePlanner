using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DegreePlanner.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddUserToTermAttempt2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "Major", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "0a8bbfd0-988f-4dd4-a6b7-e10112624c5a", 0, "a7877edd-c03b-4362-8838-32d80c6b3550", "testuser@example.com", true, "Test", "User", false, null, "Undecided", "TESTUSER@EXAMPLE.COM", "TESTUSER", "AQAAAAIAAYagAAAAEG3Txd14uXjvda06YAcXQOq0qSCmV4PfcUEOYTEYu3zfDsMyVPdh8y/lcazNpvGq5w==", null, false, "", false, "testuser" });

            migrationBuilder.InsertData(
                table: "Terms",
                columns: new[] { "Id", "EndDate", "Name", "StartDate", "UserId" },
                values: new object[] { 1, new DateTime(2024, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Term 1", new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "9a59c8a6-5231-4647-96cb-b3fe84a85dfe" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0a8bbfd0-988f-4dd4-a6b7-e10112624c5a");

            migrationBuilder.DeleteData(
                table: "Terms",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
