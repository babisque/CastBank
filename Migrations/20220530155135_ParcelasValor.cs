using Microsoft.EntityFrameworkCore.Migrations;

namespace CastBank.Migrations
{
    public partial class ParcelasValor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ValorParcelas",
                table: "Emprestimo",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValorParcelas",
                table: "Emprestimo");
        }
    }
}
