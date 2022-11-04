using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LCD_Installation.Migrations
{
    public partial class V10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LCDCenter_Production",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    Imei = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, collation: "latin1_general_ci"),
                    Step = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, collation: "latin1_general_ci"),
                    Failure = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, collation: "latin1_general_ci"),
                    Condition = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true, collation: "latin1_general_ci"),
                    User = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, collation: "latin1_general_ci"),
                    Location = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true, collation: "latin1_general_ci"),
                    Lap = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LCDCenter_Production", x => x.Id);
                })
                .Annotation("Relational:Collation", "latin1_general_ci");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LCDCenter_Production");
        }
    }
}
