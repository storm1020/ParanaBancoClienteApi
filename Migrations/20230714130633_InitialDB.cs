using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParanaBancoClienteApi.Migrations
{
    public partial class InitialDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_CLIENTE",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Sobrenome = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_CLIENTE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_TELEFONE",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DDD = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Numero = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TipoTelefone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ClienteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_TELEFONE", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_TELEFONE_TB_CLIENTE_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "TB_CLIENTE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_TELEFONE_ClienteId",
                table: "TB_TELEFONE",
                column: "ClienteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_TELEFONE");

            migrationBuilder.DropTable(
                name: "TB_CLIENTE");
        }
    }
}
