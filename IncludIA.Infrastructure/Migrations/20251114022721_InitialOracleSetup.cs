using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IncludIA.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialOracleSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_CANDIDATO",
                columns: table => new
                {
                    ID_CANDIDATO = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    NOME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    EMAIL = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_CANDIDATO", x => x.ID_CANDIDATO);
                });

            migrationBuilder.CreateTable(
                name: "TB_CANDIDATURA",
                columns: table => new
                {
                    ID_CANDIDATURA = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    ID_CANDIDATO = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    ID_VAGA_MONGO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DT_APLICACAO = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_CANDIDATURA", x => x.ID_CANDIDATURA);
                    table.ForeignKey(
                        name: "FK_TB_CANDIDATURA_TB_CANDIDATO_ID_CANDIDATO",
                        column: x => x.ID_CANDIDATO,
                        principalTable: "TB_CANDIDATO",
                        principalColumn: "ID_CANDIDATO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_CANDIDATURA_ID_CANDIDATO",
                table: "TB_CANDIDATURA",
                column: "ID_CANDIDATO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_CANDIDATURA");

            migrationBuilder.DropTable(
                name: "TB_CANDIDATO");
        }
    }
}
