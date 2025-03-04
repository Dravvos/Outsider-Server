using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Outsider.CupomAPI.Migrations
{
    /// <inheritdoc />
    public partial class CupomDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cupom",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodigoCupom = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PorcentagemDesconto = table.Column<float>(type: "real", nullable: false),
                    DataValidade = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataInclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cupom", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Cupom",
                columns: new[] { "Id", "CodigoCupom", "DataAlteracao", "DataInclusao", "DataValidade", "PorcentagemDesconto" },
                values: new object[,]
                {
                    { new Guid("31c4f02a-db8a-4f28-ad70-70c6b0eb8388"), "DN10", null, new DateTime(2025, 2, 13, 15, 54, 28, 505, DateTimeKind.Local).AddTicks(8186), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10f },
                    { new Guid("a77c16d5-49de-42ba-b762-e58e93ba454b"), "DN20", null, new DateTime(2025, 2, 13, 15, 54, 28, 505, DateTimeKind.Local).AddTicks(8233), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 20f }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cupom");
        }
    }
}
