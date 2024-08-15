using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DegreePlanner.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAddIdentityToCallUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9a59c8a6-5231-4647-96cb-b3fe84a85dfe");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "Major", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1", 0, "873df54c-8b7e-4a45-8d9c-dd466167bf1b", "studenttester1@gmail.com", false, "", "", false, null, "Undecided", "STUDENTTESTER1@GMAIL.COM", "STUDENTTESTER1@GMAIL.COM", "AQAAAAIAAYagAAAAEE8qTehN67DNoAM/JbRrzB62HT9mvPxZCyXdMmfeSwavCnwaULe/hFmDVRWNSzBZIg==", null, false, "CULID4DV2H7E6SHABGQOE27Y7JCATJLE", false, "studenttester1@gmail.com" });

            migrationBuilder.UpdateData(
                table: "Terms",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserId",
                value: "1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "Major", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "9a59c8a6-5231-4647-96cb-b3fe84a85dfe", 0, "df0b5310-f820-4904-8a1b-29b9f96e9958", "testuser@example.com", true, "Test", "User", false, null, "Undecided", "TESTUSER@EXAMPLE.COM", "TESTUSER", "AQAAAAIAAYagAAAAEKjjA3zk5eGa/SE22WuXLYE64TxBtS1lIn0YW0E3macExOcd/jWyXL32uxQW1wmNMQ==", null, false, "", false, "testuser" });

            migrationBuilder.UpdateData(
                table: "Terms",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserId",
                value: "9a59c8a6-5231-4647-96cb-b3fe84a85dfe");
        }
    }
}
