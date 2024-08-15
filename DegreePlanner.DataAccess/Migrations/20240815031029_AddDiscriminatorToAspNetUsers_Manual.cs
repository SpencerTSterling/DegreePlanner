using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DegreePlanner.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddDiscriminatorToAspNetUsers_Manual : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "3bdcbf08-f9d8-4648-bbad-a1e3a1692af7");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "873df54c-8b7e-4a45-8d9c-dd466167bf1b");
        }
    }
}
