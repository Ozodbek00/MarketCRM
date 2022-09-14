using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Market.Data.Migrations
{
    public partial class minor2Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Stete",
                table: "Users",
                newName: "State");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "State",
                table: "Users",
                newName: "Stete");
        }
    }
}
