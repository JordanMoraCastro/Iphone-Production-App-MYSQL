using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace LCD_Installation.Models
{
    public partial class Iphone_Production_AppContext : DbContext
    {

        //Constructor
        public Iphone_Production_AppContext()
        {
        }

        public Iphone_Production_AppContext(DbContextOptions<Iphone_Production_AppContext> options)
            : base(options)
        {
        }


        public virtual DbSet<AssemblyFailureCode> AssemblyFailureCodes { get; set; }
        public virtual DbSet<LCDCenterProduction> LCDCenterProductions { get; set; }
        public virtual DbSet<LCDCenterWIP> LCDCenterWips { get; set; }
        public virtual DbSet<AssyFailure> AssyFailures { get; set; }
        public virtual DbSet<AsyJigProduction> AsyJigProductions { get; set; }
        public virtual DbSet<Bpproduction> Bpproductions { get; set; }
        public virtual DbSet<DisassemblyProduction> DisassemblyProductions { get; set; }
        public virtual DbSet<DisassemblyWIP> DisassemblyWips { get; set; }
        public virtual DbSet<Permiso> Permisos { get; set; }
        public virtual DbSet<PermisosUsuario> PermisosUsuarios { get; set; }
        public virtual DbSet<SortingFailure> SortingFailures { get; set; }
        public virtual DbSet<SortingProduction> SortingProductions { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer(ConfigurationManager.AppSettings["ConnectionString"]);


                optionsBuilder.UseMySql(connectionString: @"server=crrcizweng1001;database=iphone.production;uid=JordanCastro;password=2uhOXoW6;",
                new MySqlServerVersion(new Version(10, 4, 17)));
          

                //System.Diagnostics.Debugger.IsAttached == true ? ConfigurationManager.AppSettings["ConnectionString"] : "L"
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "latin1_general_ci");

            modelBuilder.Entity<AssemblyFailureCode>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Assembly_FailureCodes");
            });

            modelBuilder.Entity<AssyFailure>(entity =>
            {

                entity.ToTable("Assembly_Dummy");

                entity.Property(e => e.Condition).HasMaxLength(50);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.Failure).HasMaxLength(50);

                entity.Property(e => e.Imei).HasMaxLength(50);

                entity.Property(e => e.Origin).HasMaxLength(50);

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .HasColumnName("User_Name");
            });

            modelBuilder.Entity<AsyJigProduction>(entity =>
            {
                entity.ToTable("Assembly_Leak");

                entity.Property(e => e.Condition).HasMaxLength(50);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.Imei).HasMaxLength(50);

                entity.Property(e => e.Mic).HasMaxLength(5);

                entity.Property(e => e.Model).HasMaxLength(5);

                entity.Property(e => e.Provenance).HasMaxLength(15);

                entity.Property(e => e.Receiver).HasMaxLength(50);

                entity.Property(e => e.Speaker).HasMaxLength(5);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.Usb).HasMaxLength(5);

                entity.Property(e => e.User)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<Bpproduction>(entity =>
            {
                entity.ToTable("BuffandPolish_Production");

                entity.Property(e => e.Condition).HasMaxLength(50);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.Material).HasMaxLength(50);

                entity.Property(e => e.NextLocation)
                    .HasMaxLength(50)
                    .HasColumnName("Next_Location");

                entity.Property(e => e.PreviousLocation)
                    .HasMaxLength(50)
                    .HasColumnName("Previous_Location");

                entity.Property(e => e.Qty).HasColumnName("QTY");

                entity.Property(e => e.User).HasMaxLength(50);
            });

            modelBuilder.Entity<DisassemblyProduction>(entity =>
            {
                entity.ToTable("Disassembly_Production");

                entity.Property(e => e.Condition).HasMaxLength(5);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.Failure).HasMaxLength(50);

                entity.Property(e => e.FinalLocation).HasMaxLength(10);

                entity.Property(e => e.FailureT)
                    .HasMaxLength(50)
                    .HasColumnName("Failure_T");

                entity.Property(e => e.Imei).HasMaxLength(50);

                entity.Property(e => e.Step).HasMaxLength(50);

                entity.Property(e => e.User).HasMaxLength(50);
            });


            modelBuilder.Entity<LCDCenterProduction>(entity =>
            {
                entity.ToTable("LCDCenter_Production");

                entity.Property(e => e.Condition).HasMaxLength(5);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.Failure).HasMaxLength(50);

                entity.Property(e => e.Location).HasMaxLength(10);

                entity.Property(e => e.Imei).HasMaxLength(50);

                entity.Property(e => e.Step).HasMaxLength(50);

                entity.Property(e => e.User).HasMaxLength(50);
            });



            modelBuilder.Entity<LCDCenterWIP>(entity =>
            {
                entity.ToTable("LCDCenter_WIP");


                entity.Property(e => e.ReceiptDate).HasColumnType("datetime");

                entity.Property(e => e.ReceiptDateTime).HasColumnType("datetime");

                entity.Property(e => e.LastScanDate).HasColumnType("datetime");

                entity.Property(e => e.LastScanDate).HasColumnType("datetime");

                entity.Property(e => e.Failure).HasMaxLength(50);

                entity.Property(e => e.CurrentLocation).HasMaxLength(20);

                entity.Property(e => e.Imei).HasMaxLength(16);

 
                entity.Property(e => e.UserName).HasMaxLength(10);
            });


            modelBuilder.Entity<DisassemblyWIP>(entity =>
            {
                entity.ToTable("Disassembly_Input");


                entity.Property(e => e.Id).HasColumnType("int(11)");


                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DateTime).HasColumnType("datetime");


                entity.Property(e => e.PidType).HasMaxLength(35);

                entity.Property(e => e.Imei).HasMaxLength(16);

                entity.Property(e => e.Provenance).HasMaxLength(15);

                entity.Property(e => e.Lap).HasColumnType("int(3)");


                entity.Property(e => e.User).HasMaxLength(10);
            });

            modelBuilder.Entity<Permiso>(entity =>
            {
                entity.Property(e => e.Descripcion).HasMaxLength(50);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<PermisosUsuario>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Permisos_Usuarios");

                entity.Property(e => e.IdPermiso).HasColumnName("id_permiso");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.HasOne(d => d.IdPermisoNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdPermiso)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_permisos_usuario_permisos");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_permisos_usuario_usuarios");
            });

            modelBuilder.Entity<SortingFailure>(entity =>
            {
                entity.ToTable("Sorting_Failure");

                entity.HasNoKey();

                entity.Property(e => e.Categoria).HasMaxLength(255);

                entity.Property(e => e.Defecto).HasMaxLength(255);

                entity.Property(e => e.Destino).HasMaxLength(255);

                entity.Property(e => e.Traduccion).HasMaxLength(255);
            });

            modelBuilder.Entity<SortingProduction>(entity =>
            {
                entity.ToTable("Sorting_Production");

                entity.Property(e => e.Aa)
                    .HasMaxLength(50)
                    .HasColumnName("AA");

                entity.Property(e => e.Bd)
                    .HasMaxLength(50)
                    .HasColumnName("BD");

                entity.Property(e => e.Condition).HasMaxLength(50);

                entity.Property(e => e.Fallos).HasMaxLength(50);

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.Hora).HasColumnType("datetime");

                entity.Property(e => e.Imei).HasMaxLength(50);

                entity.Property(e => e.MFecha)
                    .HasColumnType("datetime")
                    .HasColumnName("M_Fecha");

                entity.Property(e => e.PCondition)
                    .HasMaxLength(50)
                    .HasColumnName("P_Condition");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.Property(e => e.Area).HasMaxLength(50);

                entity.Property(e => e.LastConnection)
                    .HasColumnType("datetime")
                    .HasColumnName("Last_connection");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .HasColumnName("User_Name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
