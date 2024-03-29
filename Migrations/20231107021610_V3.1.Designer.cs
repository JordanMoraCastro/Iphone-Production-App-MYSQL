﻿// <auto-generated />
using System;
using LCD_Installation.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LCD_Installation.Migrations
{
    [DbContext(typeof(Iphone_Production_AppContext))]
    [Migration("20231107021610_V3.1")]
    partial class V31
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("latin1_general_ci")
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("LCD_Installation.Models.AssemblyFailureCode", b =>
                {
                    b.Property<string>("Failure")
                        .HasColumnType("longtext");

                    b.Property<double?>("Id")
                        .HasColumnType("double");

                    b.ToTable("Assembly_FailureCodes", (string)null);
                });

            modelBuilder.Entity("LCD_Installation.Models.AssyFailure", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Condition")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("DateTime")
                        .HasColumnType("datetime");

                    b.Property<string>("Failure")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Imei")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int?>("Lap")
                        .HasColumnType("int");

                    b.Property<string>("Origin")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int?>("Shift")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("User_Name");

                    b.HasKey("Id");

                    b.ToTable("Assembly_Dummy", (string)null);
                });

            modelBuilder.Entity("LCD_Installation.Models.AsyJigProduction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Condition")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("DateTime")
                        .HasColumnType("datetime");

                    b.Property<string>("Imei")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int?>("Lap")
                        .HasColumnType("int");

                    b.Property<string>("Mic")
                        .HasMaxLength(5)
                        .HasColumnType("varchar(5)");

                    b.Property<string>("Model")
                        .HasMaxLength(5)
                        .HasColumnType("varchar(5)");

                    b.Property<string>("Provenance")
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)");

                    b.Property<string>("Receiver")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int?>("Shift")
                        .HasColumnType("int");

                    b.Property<string>("Speaker")
                        .HasMaxLength(5)
                        .HasColumnType("varchar(5)");

                    b.Property<string>("Status")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Usb")
                        .HasMaxLength(5)
                        .HasColumnType("varchar(5)");

                    b.Property<string>("User")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.HasKey("Id");

                    b.ToTable("Assembly_Leak", (string)null);
                });

            modelBuilder.Entity("LCD_Installation.Models.Bpproduction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Condition")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("DateTime")
                        .HasColumnType("datetime");

                    b.Property<string>("Material")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("NextLocation")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Next_Location");

                    b.Property<string>("PreviousLocation")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Previous_Location");

                    b.Property<int?>("Qty")
                        .HasColumnType("int")
                        .HasColumnName("QTY");

                    b.Property<string>("User")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("BuffandPolish_Production", (string)null);
                });

            modelBuilder.Entity("LCD_Installation.Models.DisassemblyProduction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Condition")
                        .HasMaxLength(5)
                        .HasColumnType("varchar(5)");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("DateTime")
                        .HasColumnType("datetime");

                    b.Property<string>("Failure")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("FailureT")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Failure_T");

                    b.Property<string>("FinalLocation")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Imei")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int?>("Lap")
                        .HasColumnType("int");

                    b.Property<string>("Step")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("User")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Disassembly_Production", (string)null);
                });

            modelBuilder.Entity("LCD_Installation.Models.DisassemblyWIP", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("DateTime")
                        .HasColumnType("datetime");

                    b.Property<string>("Imei")
                        .HasMaxLength(16)
                        .HasColumnType("varchar(16)");

                    b.Property<bool>("InProcess")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("Lap")
                        .HasColumnType("int(3)");

                    b.Property<bool>("Pid")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("PidType")
                        .HasMaxLength(35)
                        .HasColumnType("varchar(35)");

                    b.Property<string>("Provenance")
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)");

                    b.Property<bool>("Repaired")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("User")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.HasKey("Id");

                    b.ToTable("Disassembly_Input", (string)null);
                });

            modelBuilder.Entity("LCD_Installation.Models.LCDCenterProduction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Condition")
                        .HasMaxLength(5)
                        .HasColumnType("varchar(5)");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("DateTime")
                        .HasColumnType("datetime");

                    b.Property<string>("Failure")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Imei")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int?>("Lap")
                        .HasColumnType("int");

                    b.Property<string>("Location")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Step")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("User")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("LCDCenter_Production", (string)null);
                });

            modelBuilder.Entity("LCD_Installation.Models.LCDCenterWIP", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CurrentLocation")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<int>("DaysInLocation")
                        .HasColumnType("int");

                    b.Property<string>("Failure")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Imei")
                        .HasMaxLength(16)
                        .HasColumnType("varchar(16)");

                    b.Property<int>("Lap")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastScanDate")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("LastScanDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("PreviousLocation")
                        .HasColumnType("longtext");

                    b.Property<string>("Process")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("ReceiptDate")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("ReceiptDateTime")
                        .HasColumnType("datetime");

                    b.Property<string>("Status")
                        .HasColumnType("longtext");

                    b.Property<string>("UserName")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.HasKey("Id");

                    b.ToTable("LCDCenter_WIP", (string)null);
                });

            modelBuilder.Entity("LCD_Installation.Models.Permiso", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Nombre")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Permisos");
                });

            modelBuilder.Entity("LCD_Installation.Models.PermisosUsuario", b =>
                {
                    b.Property<int>("IdPermiso")
                        .HasColumnType("int")
                        .HasColumnName("id_permiso");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int")
                        .HasColumnName("id_usuario");

                    b.HasIndex("IdPermiso");

                    b.HasIndex("IdUsuario");

                    b.ToTable("Permisos_Usuarios", (string)null);
                });

            modelBuilder.Entity("LCD_Installation.Models.Roxer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Component")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("DateTime")
                        .HasColumnType("datetime");

                    b.Property<bool>("Firts_Pass")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Imei")
                        .HasMaxLength(16)
                        .HasColumnType("varchar(16)");

                    b.Property<int?>("Lap")
                        .HasColumnType("int");

                    b.Property<string>("Part")
                        .HasColumnType("longtext");

                    b.Property<string>("Roxer_Condition")
                        .HasColumnType("longtext");

                    b.Property<bool>("Second_Pass")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Status")
                        .HasColumnType("longtext");

                    b.Property<string>("User")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Roxer", (string)null);
                });

            modelBuilder.Entity("LCD_Installation.Models.SortingFailure", b =>
                {
                    b.Property<string>("Categoria")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Defecto")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Destino")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<double?>("Id")
                        .HasColumnType("double");

                    b.Property<string>("Traduccion")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.ToTable("Sorting_Failure", (string)null);
                });

            modelBuilder.Entity("LCD_Installation.Models.SortingProduction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Aa")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("AA");

                    b.Property<string>("Bd")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("BD");

                    b.Property<string>("Condition")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Fallos")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime?>("Fecha")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("Hora")
                        .HasColumnType("datetime");

                    b.Property<string>("Imei")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime?>("MFecha")
                        .HasColumnType("datetime")
                        .HasColumnName("M_Fecha");

                    b.Property<string>("Origen")
                        .HasColumnType("longtext");

                    b.Property<string>("PCondition")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("P_Condition");

                    b.Property<string>("Tipo_Material")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Sorting_Production", (string)null);
                });

            modelBuilder.Entity("LCD_Installation.Models.TimeOut", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("AdminArea")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("Bathroom")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("ConsultingRoom")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("ExitTime")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("Locker")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("Parking")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("ReturnTime")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserID")
                        .HasColumnType("longtext");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.ToTable("TimeOut", (string)null);
                });

            modelBuilder.Entity("LCD_Installation.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Area")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime?>("LastConnection")
                        .HasColumnType("datetime")
                        .HasColumnName("Last_connection");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("UserName")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("User_Name");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("LCD_Installation.Models.PermisosUsuario", b =>
                {
                    b.HasOne("LCD_Installation.Models.Permiso", "IdPermisoNavigation")
                        .WithMany()
                        .HasForeignKey("IdPermiso")
                        .IsRequired()
                        .HasConstraintName("FK_permisos_usuario_permisos");

                    b.HasOne("LCD_Installation.Models.Usuario", "IdUsuarioNavigation")
                        .WithMany()
                        .HasForeignKey("IdUsuario")
                        .IsRequired()
                        .HasConstraintName("FK_permisos_usuario_usuarios");

                    b.Navigation("IdPermisoNavigation");

                    b.Navigation("IdUsuarioNavigation");
                });
#pragma warning restore 612, 618
        }
    }
}
