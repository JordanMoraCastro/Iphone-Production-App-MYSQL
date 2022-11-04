using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LCD_Installation.Migrations
{
    public partial class V11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LCDCenter_WIP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ReceiptDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ReceiptDateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastScanDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastScanDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Imei = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: true, collation: "latin1_general_ci"),
                    Process = table.Column<string>(type: "longtext", nullable: true, collation: "latin1_general_ci"),
                    UserName = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true, collation: "latin1_general_ci"),
                    CurrentLocation = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true, collation: "latin1_general_ci"),
                    PreviousLocation = table.Column<string>(type: "longtext", nullable: true, collation: "latin1_general_ci"),
                    DaysInLocation = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "longtext", nullable: true, collation: "latin1_general_ci"),
                    Failure = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, collation: "latin1_general_ci"),
                    Lap = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LCDCenter_WIP", x => x.Id);
                })
                .Annotation("Relational:Collation", "latin1_general_ci");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LCDCenter_WIP");
        }
    }
}
