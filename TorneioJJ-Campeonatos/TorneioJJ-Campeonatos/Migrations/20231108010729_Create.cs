using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TorneioJJ_Campeonatos.Migrations
{
    /// <inheritdoc />
    public partial class Create : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tipo",
                table: "Campeonatos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Campeonatos");
        }
    }
}
