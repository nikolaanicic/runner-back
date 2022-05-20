using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class BusyField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Busy",
                table: "Deliverer",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Busy",
                table: "Deliverer");
        }
    }
}
