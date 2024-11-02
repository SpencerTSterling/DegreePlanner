using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DegreePlanner.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class undoIdentityUserModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "98973b9c-edf2-416f-af7d-fe3a79c051fa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "e994bcff-0774-47b3-817c-3f1567836fb7");
        }
    }
}
