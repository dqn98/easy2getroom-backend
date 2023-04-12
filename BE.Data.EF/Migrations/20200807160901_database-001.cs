using Microsoft.EntityFrameworkCore.Migrations;

namespace BE.Data.EF.Migrations
{
    public partial class database001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "LogTypes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icon",
                table: "LogTypes");
        }
    }
}
