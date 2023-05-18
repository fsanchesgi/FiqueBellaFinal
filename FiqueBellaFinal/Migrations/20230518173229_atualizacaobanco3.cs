using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FiqueBellaFinal.Migrations
{
    public partial class atualizacaobanco3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EmPromocao",
                table: "Procedimentos",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmPromocao",
                table: "Procedimentos");
        }
    }
}
