using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DegreePlanner.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UserIdonTermsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0a8bbfd0-988f-4dd4-a6b7-e10112624c5a");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "Major", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "9a59c8a6-5231-4647-96cb-b3fe84a85dfe", 0, "df0b5310-f820-4904-8a1b-29b9f96e9958", "testuser@example.com", true, "Test", "User", false, null, "Undecided", "TESTUSER@EXAMPLE.COM", "TESTUSER", "AQAAAAIAAYagAAAAEKjjA3zk5eGa/SE22WuXLYE64TxBtS1lIn0YW0E3macExOcd/jWyXL32uxQW1wmNMQ==", null, false, "", false, "testuser" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9a59c8a6-5231-4647-96cb-b3fe84a85dfe");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "Major", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "0a8bbfd0-988f-4dd4-a6b7-e10112624c5a", 0, "a7877edd-c03b-4362-8838-32d80c6b3550", "testuser@example.com", true, "Test", "User", false, null, "Undecided", "TESTUSER@EXAMPLE.COM", "TESTUSER", "AQAAAAIAAYagAAAAEG3Txd14uXjvda06YAcXQOq0qSCmV4PfcUEOYTEYu3zfDsMyVPdh8y/lcazNpvGq5w==", null, false, "", false, "testuser" });
        }
    }
}
