using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projeto_API_BackEnd_Estacionamento.Migrations
{
    /// <inheritdoc />
    public partial class AttMovimentacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataSaida",
                table: "movimentacaoEstacionamento");

            migrationBuilder.DropColumn(
                name: "EstabelecimentoId",
                table: "movimentacaoEstacionamento");

            migrationBuilder.DropColumn(
                name: "VeiculoId",
                table: "movimentacaoEstacionamento");

            migrationBuilder.RenameColumn(
                name: "TipoVaga",
                table: "movimentacaoEstacionamento",
                newName: "TipoVeiculo");

            migrationBuilder.RenameColumn(
                name: "DataEntrada",
                table: "movimentacaoEstacionamento",
                newName: "HoraSaida");

            migrationBuilder.AddColumn<DateTime>(
                name: "HoraEntrada",
                table: "movimentacaoEstacionamento",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "PlacaVeiculo",
                table: "movimentacaoEstacionamento",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HoraEntrada",
                table: "movimentacaoEstacionamento");

            migrationBuilder.DropColumn(
                name: "PlacaVeiculo",
                table: "movimentacaoEstacionamento");

            migrationBuilder.RenameColumn(
                name: "TipoVeiculo",
                table: "movimentacaoEstacionamento",
                newName: "TipoVaga");

            migrationBuilder.RenameColumn(
                name: "HoraSaida",
                table: "movimentacaoEstacionamento",
                newName: "DataEntrada");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataSaida",
                table: "movimentacaoEstacionamento",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EstabelecimentoId",
                table: "movimentacaoEstacionamento",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VeiculoId",
                table: "movimentacaoEstacionamento",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
