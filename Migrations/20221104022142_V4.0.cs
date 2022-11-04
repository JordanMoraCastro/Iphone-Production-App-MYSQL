using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LCD_Installation.Migrations
{
    public partial class V40 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tipo_Material",
                table: "Sorting_Production",
                type: "longtext",
                nullable: true,
                collation: "latin1_general_ci");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tipo_Material",
                table: "Sorting_Production");
        }
    }
}
