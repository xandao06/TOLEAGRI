using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TOLEAGRI.Migrations
{
    /// <inheritdoc />
    public partial class TOLE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estoques",
                columns: table => new
                {
                    EstoqueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    Observacao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodigoSistema = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Modelo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Locacao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuantidadeEntrada = table.Column<int>(type: "int", nullable: false),
                    QuantidadeSaida = table.Column<int>(type: "int", nullable: false),
                    DataEntrada = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataSaida = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estoques", x => x.EstoqueId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Estoques");
        }
    }
}
