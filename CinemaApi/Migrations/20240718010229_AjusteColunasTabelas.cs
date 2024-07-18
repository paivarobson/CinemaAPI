using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaApi.Migrations
{
    /// <inheritdoc />
    public partial class AjusteColunasTabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NomeSala",
                table: "Salas");

            migrationBuilder.DropColumn(
                name: "AnoLancamento",
                table: "Filmes");

            migrationBuilder.DropColumn(
                name: "Genero",
                table: "Filmes");

            migrationBuilder.RenameColumn(
                name: "Capacidade",
                table: "Salas",
                newName: "Numero");

            migrationBuilder.RenameColumn(
                name: "Titulo",
                table: "Filmes",
                newName: "Nome");

            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Salas",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "SalaId",
                table: "Filmes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Filmes_SalaId",
                table: "Filmes",
                column: "SalaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Filmes_Salas_SalaId",
                table: "Filmes",
                column: "SalaId",
                principalTable: "Salas",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Filmes_Salas_SalaId",
                table: "Filmes");

            migrationBuilder.DropIndex(
                name: "IX_Filmes_SalaId",
                table: "Filmes");

            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Salas");

            migrationBuilder.DropColumn(
                name: "SalaId",
                table: "Filmes");

            migrationBuilder.RenameColumn(
                name: "Numero",
                table: "Salas",
                newName: "Capacidade");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Filmes",
                newName: "Titulo");

            migrationBuilder.AddColumn<string>(
                name: "NomeSala",
                table: "Salas",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "AnoLancamento",
                table: "Filmes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Genero",
                table: "Filmes",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
