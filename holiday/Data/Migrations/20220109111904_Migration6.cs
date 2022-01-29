using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace holiday.Data.Migrations
{
    public partial class Migration6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Approver",
                table: "Holiday",
                newName: "ApproverId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ApproverId",
                table: "Holiday",
                newName: "Approver");
        }
    }
}
