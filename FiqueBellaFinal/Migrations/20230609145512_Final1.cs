using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FiqueBellaFinal.Migrations
{
    public partial class Final1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InOut",
                table: "Contabilidades");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InOut",
                table: "Contabilidades",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
