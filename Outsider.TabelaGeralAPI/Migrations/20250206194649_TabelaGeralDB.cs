using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Outsider.TabelaGeralAPI.Migrations
{
    /// <inheritdoc />
    public partial class TabelaGeralDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TabelaGeral",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    DataInclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TabelaGeral", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TabelaGeralItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TabelaGeralId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Sigla = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    DataInclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TabelaGeralItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TabelaGeralItem_TabelaGeral_TabelaGeralId",
                        column: x => x.TabelaGeralId,
                        principalTable: "TabelaGeral",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TabelaGeralItem_TabelaGeralId",
                table: "TabelaGeralItem",
                column: "TabelaGeralId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TabelaGeralItem");

            migrationBuilder.DropTable(
                name: "TabelaGeral");
        }
    }
}
