using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Outsider.ProdutoAPI.Migrations
{
    /// <inheritdoc />
    public partial class ProdutoDB : Migration
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

            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Preco = table.Column<float>(type: "real", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", maxLength: 10000, nullable: true),
                    Imagem = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Peso = table.Column<float>(type: "real", nullable: false),
                    Estoque = table.Column<int>(type: "int", nullable: false),
                    SKU = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    IdTGCategoria = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdTGCor = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdTGTamanho = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataInclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produto_TabelaGeralItem_IdTGCategoria",
                        column: x => x.IdTGCategoria,
                        principalTable: "TabelaGeralItem",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Produto_TabelaGeralItem_IdTGCor",
                        column: x => x.IdTGCor,
                        principalTable: "TabelaGeralItem",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Produto_TabelaGeralItem_IdTGTamanho",
                        column: x => x.IdTGTamanho,
                        principalTable: "TabelaGeralItem",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Produto_IdTGCategoria",
                table: "Produto",
                column: "IdTGCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_IdTGCor",
                table: "Produto",
                column: "IdTGCor");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_IdTGTamanho",
                table: "Produto",
                column: "IdTGTamanho");

            migrationBuilder.CreateIndex(
                name: "IX_TabelaGeralItem_TabelaGeralId",
                table: "TabelaGeralItem",
                column: "TabelaGeralId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Produto");

            migrationBuilder.DropTable(
                name: "TabelaGeralItem");

            migrationBuilder.DropTable(
                name: "TabelaGeral");
        }
    }
}
