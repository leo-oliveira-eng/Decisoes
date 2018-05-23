using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Repository.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Criterio_TipoDeDados_TipoDeDadosId",
                table: "Criterio");

            migrationBuilder.DropTable(
                name: "TipoDeDados");

            migrationBuilder.DropIndex(
                name: "IX_Criterio_TipoDeDadosId",
                table: "Criterio");

            migrationBuilder.DropColumn(
                name: "TipoDeDadosId",
                table: "Criterio");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCadastro",
                table: "Projeto",
                nullable: false,
                defaultValue: new DateTime(2017, 12, 20, 13, 25, 40, 910, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2017, 12, 16, 19, 58, 51, 324, DateTimeKind.Local));

            migrationBuilder.AddColumn<int>(
                name: "Tipo",
                table: "Criterio",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Criterio");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCadastro",
                table: "Projeto",
                nullable: false,
                defaultValue: new DateTime(2017, 12, 16, 19, 58, 51, 324, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2017, 12, 20, 13, 25, 40, 910, DateTimeKind.Local));

            migrationBuilder.AddColumn<int>(
                name: "TipoDeDadosId",
                table: "Criterio",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TipoDeDados",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoDeDados", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Criterio_TipoDeDadosId",
                table: "Criterio",
                column: "TipoDeDadosId");

            migrationBuilder.AddForeignKey(
                name: "FK_Criterio_TipoDeDados_TipoDeDadosId",
                table: "Criterio",
                column: "TipoDeDadosId",
                principalTable: "TipoDeDados",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
