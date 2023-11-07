using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LCD_Installation.Migrations
{
    public partial class V31 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roxer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    Imei = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: true, collation: "latin1_general_ci"),
                    Firts_Pass = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Second_Pass = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Part = table.Column<string>(type: "longtext", nullable: true, collation: "latin1_general_ci"),
                    Component = table.Column<string>(type: "longtext", nullable: true, collation: "latin1_general_ci"),
                    Roxer_Condition = table.Column<string>(type: "longtext", nullable: true, collation: "latin1_general_ci"),
                    Status = table.Column<string>(type: "longtext", nullable: true, collation: "latin1_general_ci"),
                    User = table.Column<string>(type: "longtext", nullable: true, collation: "latin1_general_ci"),
                    Lap = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roxer", x => x.Id);
                })
                .Annotation("Relational:Collation", "latin1_general_ci");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Roxer");
        }
    }
}
