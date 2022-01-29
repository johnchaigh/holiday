using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace holiday.Data.Migrations
{
    public partial class Migration7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApproverId",
                table: "Holiday");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApproverId",
                table: "Holiday",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
