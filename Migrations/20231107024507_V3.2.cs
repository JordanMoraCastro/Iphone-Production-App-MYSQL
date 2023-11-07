using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LCD_Installation.Migrations
{
    public partial class V32 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Second_Pass",
                table: "Roxer",
                type: "longtext",
                nullable: true,
                collation: "latin1_general_ci",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

            migrationBuilder.AlterColumn<string>(
                name: "Firts_Pass",
                table: "Roxer",
                type: "longtext",
                nullable: true,
                collation: "latin1_general_ci",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Second_Pass",
                table: "Roxer",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "latin1_general_ci");

            migrationBuilder.AlterColumn<bool>(
                name: "Firts_Pass",
                table: "Roxer",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "latin1_general_ci");
        }
    }
}
