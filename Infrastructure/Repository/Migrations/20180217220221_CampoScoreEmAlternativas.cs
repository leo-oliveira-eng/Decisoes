using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Repository.Migrations
{
    public partial class CampoScoreEmAlternativas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCadastro",
                table: "Projeto",
                nullable: false,
                defaultValue: new DateTime(2018, 2, 17, 20, 2, 21, 805, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 2, 11, 17, 50, 54, 359, DateTimeKind.Local));

            migrationBuilder.AddColumn<double>(
                name: "Score",
                table: "Alternativa",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Score",
                table: "Alternativa");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCadastro",
                table: "Projeto",
                nullable: false,
                defaultValue: new DateTime(2018, 2, 11, 17, 50, 54, 359, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 2, 17, 20, 2, 21, 805, DateTimeKind.Local));
        }
    }
}
