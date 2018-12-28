using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bank.Infra.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "contas_corrente",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    numero = table.Column<int>(nullable: false),
                    digito = table.Column<int>(nullable: false),
                    saldo = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contas_corrente", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "extratos",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    conta_corrente_id = table.Column<int>(nullable: false),
                    tipo_transacao = table.Column<string>(type: "char(1)", nullable: false),
                    saldo_anterior = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    saldo_atual = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    data_transacao = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_extratos", x => x.id);
                    table.ForeignKey(
                        name: "fk_contas_corrente_extrato",
                        column: x => x.conta_corrente_id,
                        principalTable: "contas_corrente",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_extratos_conta_corrente_id",
                table: "extratos",
                column: "conta_corrente_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "extratos");

            migrationBuilder.DropTable(
                name: "contas_corrente");
        }
    }
}
