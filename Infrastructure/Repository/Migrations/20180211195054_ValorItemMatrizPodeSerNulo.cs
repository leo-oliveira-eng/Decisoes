using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Repository.Migrations
{
    public partial class ValorItemMatrizPodeSerNulo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCadastro",
                table: "Projeto",
                nullable: false,
                defaultValue: new DateTime(2018, 2, 11, 17, 50, 54, 359, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 2, 4, 16, 21, 3, 11, DateTimeKind.Local));

            migrationBuilder.AlterColumn<string>(
                name: "Valor",
                table: "MatrizDeDecisao",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCadastro",
                table: "Projeto",
                nullable: false,
                defaultValue: new DateTime(2018, 2, 4, 16, 21, 3, 11, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 2, 11, 17, 50, 54, 359, DateTimeKind.Local));

            migrationBuilder.AlterColumn<string>(
                name: "Valor",
                table: "MatrizDeDecisao",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);
        }
    }
}
