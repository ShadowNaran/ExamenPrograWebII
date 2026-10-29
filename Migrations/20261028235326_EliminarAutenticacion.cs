using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiExamen.Migrations
{
    /// <inheritdoc />
    public partial class EliminarAutenticacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "password_hash",
                table: "oficiales");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "password_hash",
                table: "oficiales",
                type: "character varying(512)",
                maxLength: 512,
                nullable: true);
        }
    }
}
