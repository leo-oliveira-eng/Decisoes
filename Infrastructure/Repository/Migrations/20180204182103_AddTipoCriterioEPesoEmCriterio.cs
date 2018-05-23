using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Repository.Migrations
{
    public partial class AddTipoCriterioEPesoEmCriterio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Criterio");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCadastro",
                table: "Projeto",
                nullable: false,
                defaultValue: new DateTime(2018, 2, 4, 16, 21, 3, 11, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2017, 12, 20, 13, 25, 40, 910, DateTimeKind.Local));

            migrationBuilder.AddColumn<decimal>(
                name: "Peso",
                table: "Criterio",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "TipoDeCriterio",
                table: "Criterio",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TipoDeDados",
                table: "Criterio",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Peso",
                table: "Criterio");

            migrationBuilder.DropColumn(
                name: "TipoDeCriterio",
                table: "Criterio");

            migrationBuilder.DropColumn(
                name: "TipoDeDados",
                table: "Criterio");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCadastro",
                table: "Projeto",
                nullable: false,
                defaultValue: new DateTime(2017, 12, 20, 13, 25, 40, 910, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 2, 4, 16, 21, 3, 11, DateTimeKind.Local));

            migrationBuilder.AddColumn<int>(
                name: "Tipo",
                table: "Criterio",
                nullable: false,
                defaultValue: 0);
        }
    }
}
