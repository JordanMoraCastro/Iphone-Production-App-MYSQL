using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LCD_Installation.Migrations
{
    public partial class v10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TimeOut",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UserID = table.Column<string>(type: "longtext", nullable: true, collation: "latin1_general_ci"),
                    Bathroom = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Locker = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ConsultingRoom = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Parking = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AdminArea = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Status = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ExitTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ReturnTime = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                })
                .Annotation("Relational:Collation", "latin1_general_ci");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeOut");
        }
    }
}
