using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DegreePlanner.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class removeSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //Delete specific Terms entry with Id = 1, if it exists
            migrationBuilder.DeleteData(
                table: "Terms",
                keyColumn: "Id",
                keyValue: 1);

            // Delete any Terms entry that references the specific UserId
            migrationBuilder.Sql("DELETE FROM Terms WHERE UserId = '9a59c8a6-5231-4647-96cb-b3fe84a85dfe'");

            // Delete specific AspNetUsers entry with Id = "1", if it exists
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1");

            // Delete any AspNetUsers entry with specific UserId = '9a59c8a6-5231-4647-96cb-b3fe84a85dfe'
            migrationBuilder.Sql("DELETE FROM AspNetUsers WHERE Id = '9a59c8a6-5231-4647-96cb-b3fe84a85dfe'");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Reinsert original seed data for AspNetUsers
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "Major", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1", 0, "69434944-f5a2-473d-8803-eeabe137d413", "User", "studenttester1@gmail.com", false, "", "", false, null, "Undecided", "STUDENTTESTER1@GMAIL.COM", "STUDENTTESTER1@GMAIL.COM", "AQAAAAIAAYagAAAAEE8qTehN67DNoAM/JbRrzB62HT9mvPxZCyXdMmfeSwavCnwaULe/hFmDVRWNSzBZIg==", null, false, "CULID4DV2H7E6SHABGQOE27Y7JCATJLE", false, "studenttester1@gmail.com" });

            // Reinsert original seed data for Terms
            migrationBuilder.InsertData(
                table: "Terms",
                columns: new[] { "Id", "EndDate", "Name", "StartDate", "UserId" },
                values: new object[] { 1, new DateTime(2024, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Term 1", new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1" });
        }
    }
}
