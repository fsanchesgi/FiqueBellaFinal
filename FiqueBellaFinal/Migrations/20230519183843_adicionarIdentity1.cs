using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FiqueBellaFinal.Migrations
{
    public partial class adicionarIdentity1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Horario",
                table: "Agendas",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "ClienteId",
                table: "Agendas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProcedimentoId",
                table: "Agendas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Agendas_ClienteId",
                table: "Agendas",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Agendas_ProcedimentoId",
                table: "Agendas",
                column: "ProcedimentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Agendas_Clientes_ClienteId",
                table: "Agendas",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "ClienteId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Agendas_Procedimentos_ProcedimentoId",
                table: "Agendas",
                column: "ProcedimentoId",
                principalTable: "Procedimentos",
                principalColumn: "ProcedimentoId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agendas_Clientes_ClienteId",
                table: "Agendas");

            migrationBuilder.DropForeignKey(
                name: "FK_Agendas_Procedimentos_ProcedimentoId",
                table: "Agendas");

            migrationBuilder.DropIndex(
                name: "IX_Agendas_ClienteId",
                table: "Agendas");

            migrationBuilder.DropIndex(
                name: "IX_Agendas_ProcedimentoId",
                table: "Agendas");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "Agendas");

            migrationBuilder.DropColumn(
                name: "ProcedimentoId",
                table: "Agendas");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Horario",
                table: "Agendas",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);
        }
    }
}
