using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LtuUniversity.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Address",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Address",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Address",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Address",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 4);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Avatar", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "123", "Anka", "Kalle" },
                    { 2, "123", "Anka", "Nissa" },
                    { 3, "123", "Anka", "Olof" },
                    { 4, "123", "Anka", "Anna" }
                });

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "Id", "City", "Street", "StudentId", "ZipCode" },
                values: new object[,]
                {
                    { 1, "Stockholm", "Gatan1", 1, "123" },
                    { 2, "Stockholm2", "Gatan2", 2, "123" },
                    { 3, "Stockholm3", "Gatan3", 3, "123" },
                    { 4, "Stockholm4", "Gatan4", 4, "123" }
                });
        }
    }
}
