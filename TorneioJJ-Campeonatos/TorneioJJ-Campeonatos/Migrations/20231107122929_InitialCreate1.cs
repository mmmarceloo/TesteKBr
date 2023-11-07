using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TorneioJJ_Campeonatos.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Tipo",
                table: "Campeonatos",
                newName: "Destaque");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Destaque",
                table: "Campeonatos",
                newName: "Tipo");
        }
    }
}
