using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class AdminAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Address", "DateOfBirth", "Email", "ImagePath", "LastName", "Name", "PasswordHash", "RoleId", "Username" },
                values: new object[] { 1L, "Korenita, Josifa Tronosca 25", new DateTime(1999, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "nikolaanicic99@gmail.com", "nema slike za sada", "Anicic", "Nikola", "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=", 1L, "admin" });

            migrationBuilder.InsertData(
                table: "Admin",
                column: "Id",
                value: 1L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admin",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1L);
        }
    }
}
