using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LCD_Installation.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assembly_Dummy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    Imei = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, collation: "latin1_general_ci"),
                    Failure = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, collation: "latin1_general_ci"),
                    User_Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, collation: "latin1_general_ci"),
                    Condition = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, collation: "latin1_general_ci"),
                    Origin = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, collation: "latin1_general_ci"),
                    Lap = table.Column<int>(type: "int", nullable: true),
                    Shift = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assembly_Dummy", x => x.Id);
                })
                .Annotation("Relational:Collation", "latin1_general_ci");

            migrationBuilder.CreateTable(
                name: "Assembly_FailureCodes",
                columns: table => new
                {
                    Id = table.Column<double>(type: "double", nullable: true),
                    Failure = table.Column<string>(type: "longtext", nullable: true, collation: "latin1_general_ci")
                },
                constraints: table =>
                {
                })
                .Annotation("Relational:Collation", "latin1_general_ci");

            migrationBuilder.CreateTable(
                name: "Assembly_Leak",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    Imei = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, collation: "latin1_general_ci"),
                    Shift = table.Column<int>(type: "int", nullable: true),
                    Speaker = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true, collation: "latin1_general_ci"),
                    Mic = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true, collation: "latin1_general_ci"),
                    Usb = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true, collation: "latin1_general_ci"),
                    Receiver = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, collation: "latin1_general_ci"),
                    Model = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true, collation: "latin1_general_ci"),
                    Provenance = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true, collation: "latin1_general_ci"),
                    Condition = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, collation: "latin1_general_ci"),
                    Status = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, collation: "latin1_general_ci"),
                    User = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false, collation: "latin1_general_ci"),
                    Lap = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assembly_Leak", x => x.Id);
                })
                .Annotation("Relational:Collation", "latin1_general_ci");

            migrationBuilder.CreateTable(
                name: "BuffandPolish_Production",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    Material = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, collation: "latin1_general_ci"),
                    Previous_Location = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, collation: "latin1_general_ci"),
                    Next_Location = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, collation: "latin1_general_ci"),
                    QTY = table.Column<int>(type: "int", nullable: true),
                    Condition = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, collation: "latin1_general_ci"),
                    User = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, collation: "latin1_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuffandPolish_Production", x => x.Id);
                })
                .Annotation("Relational:Collation", "latin1_general_ci");

            migrationBuilder.CreateTable(
                name: "Disassembly_Input",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    Imei = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: true, collation: "latin1_general_ci"),
                    Provenance = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true, collation: "latin1_general_ci"),
                    Pid = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PidType = table.Column<string>(type: "varchar(35)", maxLength: 35, nullable: true, collation: "latin1_general_ci"),
                    Repaired = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    InProcess = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    User = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true, collation: "latin1_general_ci"),
                    Lap = table.Column<int>(type: "int(3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disassembly_Input", x => x.Id);
                })
                .Annotation("Relational:Collation", "latin1_general_ci");

            migrationBuilder.CreateTable(
                name: "Disassembly_Production",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    Imei = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, collation: "latin1_general_ci"),
                    Step = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, collation: "latin1_general_ci"),
                    Failure = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, collation: "latin1_general_ci"),
                    Failure_T = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, collation: "latin1_general_ci"),
                    Condition = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true, collation: "latin1_general_ci"),
                    User = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, collation: "latin1_general_ci"),
                    FinalLocation = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true, collation: "latin1_general_ci"),
                    Lap = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disassembly_Production", x => x.Id);
                })
                .Annotation("Relational:Collation", "latin1_general_ci");

            migrationBuilder.CreateTable(
                name: "Permisos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, collation: "latin1_general_ci"),
                    Descripcion = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, collation: "latin1_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permisos", x => x.Id);
                })
                .Annotation("Relational:Collation", "latin1_general_ci");

            migrationBuilder.CreateTable(
                name: "Sorting_Failure",
                columns: table => new
                {
                    Id = table.Column<double>(type: "double", nullable: true),
                    Defecto = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "latin1_general_ci"),
                    Traduccion = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "latin1_general_ci"),
                    Destino = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "latin1_general_ci"),
                    Categoria = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "latin1_general_ci")
                },
                constraints: table =>
                {
                })
                .Annotation("Relational:Collation", "latin1_general_ci");

            migrationBuilder.CreateTable(
                name: "Sorting_Production",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Imei = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, collation: "latin1_general_ci"),
                    Condition = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, collation: "latin1_general_ci"),
                    AA = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, collation: "latin1_general_ci"),
                    BD = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, collation: "latin1_general_ci"),
                    P_Condition = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, collation: "latin1_general_ci"),
                    Fallos = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, collation: "latin1_general_ci"),
                    Fecha = table.Column<DateTime>(type: "datetime", nullable: true),
                    Hora = table.Column<DateTime>(type: "datetime", nullable: true),
                    M_Fecha = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sorting_Production", x => x.Id);
                })
                .Annotation("Relational:Collation", "latin1_general_ci");

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    User_Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, collation: "latin1_general_ci"),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, collation: "latin1_general_ci"),
                    Last_connection = table.Column<DateTime>(type: "datetime", nullable: true),
                    Area = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, collation: "latin1_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                })
                .Annotation("Relational:Collation", "latin1_general_ci");

            migrationBuilder.CreateTable(
                name: "Permisos_Usuarios",
                columns: table => new
                {
                    id_usuario = table.Column<int>(type: "int", nullable: false),
                    id_permiso = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_permisos_usuario_permisos",
                        column: x => x.id_permiso,
                        principalTable: "Permisos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_permisos_usuario_usuarios",
                        column: x => x.id_usuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                })
                .Annotation("Relational:Collation", "latin1_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Permisos_Usuarios_id_permiso",
                table: "Permisos_Usuarios",
                column: "id_permiso");

            migrationBuilder.CreateIndex(
                name: "IX_Permisos_Usuarios_id_usuario",
                table: "Permisos_Usuarios",
                column: "id_usuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assembly_Dummy");

            migrationBuilder.DropTable(
                name: "Assembly_FailureCodes");

            migrationBuilder.DropTable(
                name: "Assembly_Leak");

            migrationBuilder.DropTable(
                name: "BuffandPolish_Production");

            migrationBuilder.DropTable(
                name: "Disassembly_Input");

            migrationBuilder.DropTable(
                name: "Disassembly_Production");

            migrationBuilder.DropTable(
                name: "Permisos_Usuarios");

            migrationBuilder.DropTable(
                name: "Sorting_Failure");

            migrationBuilder.DropTable(
                name: "Sorting_Production");

            migrationBuilder.DropTable(
                name: "Permisos");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
