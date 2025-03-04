using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Outsider.EnderecoAPI.Migrations
{
    /// <inheritdoc />
    public partial class EnderecoDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Endereco",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Recebedor = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CEP = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Bairro = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Logradouro = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    Complemento = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataInclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endereco", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Endereco");
        }
    }
}
