using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SistemaInventario.Entity.Models;

namespace SistemaInventario.Entity.Models
{
    public partial class dbhospitalcontext : DbContext
    {
        public dbhospitalcontext()
        {
        }

        public dbhospitalcontext(DbContextOptions<dbhospitalcontext> options)
            : base(options)
        {
        }

        public virtual DbSet<Area> Areas { get; set; } = null!;
        public virtual DbSet<Articulo> Articulos { get; set; } = null!;
        public virtual DbSet<Categoria> Categoria { get; set; } = null!;
        public virtual DbSet<DetalleIngreso> DetalleIngresos { get; set; } = null!;
        public virtual DbSet<DetalleSalida> DetalleSalida { get; set; } = null!;
        public virtual DbSet<Ingreso> Ingresos { get; set; } = null!;
        public virtual DbSet<Marca> Marcas { get; set; } = null!;
        public virtual DbSet<Origen> Origens { get; set; } = null!;
        public virtual DbSet<Personal> Personals { get; set; } = null!;
        public virtual DbSet<Salida> Salida { get; set; } = null!;
        public virtual DbSet<Servicio> Servicios { get; set; } = null!;
        public virtual DbSet<Subcategoria> Subcategoria { get; set; } = null!;
        public virtual DbSet<UnidadMedida> UnidadMedida { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
 {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseMySql("server=bkmzouldxpir4aybfu0u-mysql.services.clever-cloud.com;port=3306;database=bkmzouldxpir4aybfu0u;user=us9fvaqbrj6yjnso;password=s7NOEiJvVWwEISQFKdNe", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.22-mysql"));
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8_general_ci")
                .HasCharSet("utf8");

            modelBuilder.Entity<Area>(entity =>
            {
                entity.HasKey(e => e.IdArea)
                    .HasName("PRIMARY");

                entity.ToTable("Area");

                entity.Property(e => e.Descripcion).HasMaxLength(50);

                entity.Property(e => e.EsActivo).HasColumnName("esActivo");
            });

            modelBuilder.Entity<Articulo>(entity =>
            {
                entity.HasKey(e => e.IdArticulo)
                    .HasName("PRIMARY");

                entity.ToTable("Articulo");

                entity.HasIndex(e => e.IdMarca, "Id_Marca");

                entity.HasIndex(e => e.IdSubcategoria, "Id_Subcategoria");

                entity.HasIndex(e => e.IdUnidadMedida, "Id_UnidadMedida");

                entity.Property(e => e.CodigoArticulo).HasMaxLength(20);
                entity.Property(e => e.Descripcion).HasMaxLength(50);

                entity.Property(e => e.EsActivo).HasColumnName("esActivo");

                entity.Property(e => e.IdMarca).HasColumnName("Id_Marca");

                entity.Property(e => e.IdSubcategoria).HasColumnName("Id_Subcategoria");

                entity.Property(e => e.IdUnidadMedida).HasColumnName("Id_UnidadMedida");

                entity.Property(e => e.Serie).HasMaxLength(12);

                entity.HasOne(d => d.IdMarcaNavigation)
                    .WithMany(p => p.Articulos)
                    .HasForeignKey(d => d.IdMarca)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Articulo_ibfk_1");

                entity.HasOne(d => d.IdSubcategoriaNavigation)
                    .WithMany(p => p.Articulos)
                    .HasForeignKey(d => d.IdSubcategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Articulo_ibfk_3");

                entity.HasOne(d => d.IdUnidadMedidaNavigation)
                    .WithMany(p => p.Articulos)
                    .HasForeignKey(d => d.IdUnidadMedida)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Articulo_ibfk_2");
            });

            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.IdCategoria)
                    .HasName("PRIMARY");

                entity.Property(e => e.Descripcion).HasMaxLength(50);

                entity.Property(e => e.EsActivo).HasColumnName("esActivo");
            });

            modelBuilder.Entity<DetalleIngreso>(entity =>
            {
                entity.HasKey(e => e.IdDetalleIngreso)
                    .HasName("PRIMARY");

                entity.ToTable("DetalleIngreso");

                entity.HasIndex(e => e.IdArticulo, "Id_Articulo");

                entity.HasIndex(e => e.IdIngreso, "Id_Ingreso");

                entity.Property(e => e.EsActivo).HasColumnName("esActivo");

                entity.Property(e => e.FechaIngreso).HasColumnType("datetime");

                entity.Property(e => e.IdArticulo).HasColumnName("Id_Articulo");

                entity.Property(e => e.IdIngreso).HasColumnName("Id_Ingreso");

                entity.HasOne(d => d.IdArticuloNavigation)
                    .WithMany(p => p.DetalleIngresos)
                    .HasForeignKey(d => d.IdArticulo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("DetalleIngreso_ibfk_2");

                entity.HasOne(d => d.IdIngresoNavigation)
                    .WithMany(p => p.DetalleIngresos)
                    .HasForeignKey(d => d.IdIngreso)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("DetalleIngreso_ibfk_1");
            });

            modelBuilder.Entity<DetalleSalida>(entity =>
            {
                entity.HasKey(e => e.IdDetalleSalida)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.IdArticulo, "Id_Articulo");

                entity.HasIndex(e => e.IdSalida, "Id_Salida");

                entity.Property(e => e.EsActivo).HasColumnName("esActivo");

                entity.Property(e => e.IdArticulo).HasColumnName("Id_Articulo");

                entity.Property(e => e.IdSalida).HasColumnName("Id_Salida");

                entity.HasOne(d => d.IdArticuloNavigation)
                    .WithMany(p => p.DetalleSalida)
                    .HasForeignKey(d => d.IdArticulo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("DetalleSalida_ibfk_2");

                entity.HasOne(d => d.IdSalidaNavigation)
                    .WithMany(p => p.DetalleSalida)
                    .HasForeignKey(d => d.IdSalida)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("DetalleSalida_ibfk_1");
            });

            modelBuilder.Entity<Ingreso>(entity =>
            {
                entity.HasKey(e => e.IdIngreso)
                    .HasName("PRIMARY");

                entity.ToTable("Ingreso");

                entity.HasIndex(e => e.IdOrigen, "Id_Origen");

                entity.HasIndex(e => e.IdPersonal, "Id_Personal");

                entity.Property(e => e.Descripcion).HasMaxLength(50);

                entity.Property(e => e.EsActivo).HasColumnName("esActivo");

                entity.Property(e => e.IdOrigen).HasColumnName("Id_Origen");

                entity.Property(e => e.IdPersonal).HasColumnName("Id_Personal");

                entity.Property(e => e.Justificacion).HasMaxLength(50);

                entity.Property(e => e.NumeroPecosa).HasMaxLength(20);

                entity.HasOne(d => d.IdOrigenNavigation)
                    .WithMany(p => p.Ingresos)
                    .HasForeignKey(d => d.IdOrigen)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Ingreso_ibfk_1");

                entity.HasOne(d => d.IdPersonalNavigation)
                    .WithMany(p => p.Ingresos)
                    .HasForeignKey(d => d.IdPersonal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Ingreso_ibfk_2");
            });

            modelBuilder.Entity<Marca>(entity =>
            {
                entity.HasKey(e => e.IdMarca)
                    .HasName("PRIMARY");

                entity.ToTable("Marca");

                entity.Property(e => e.Descripcion).HasMaxLength(50);

                entity.Property(e => e.EsActivo).HasColumnName("esActivo");
            });

            modelBuilder.Entity<Origen>(entity =>
            {
                entity.HasKey(e => e.IdOrigen)
                    .HasName("PRIMARY");

                entity.ToTable("Origen");

                entity.Property(e => e.Descripcion).HasMaxLength(50);

                entity.Property(e => e.EsActivo).HasColumnName("esActivo");
            });

            modelBuilder.Entity<Personal>(entity =>
            {
                entity.HasKey(e => e.IdPersonal)
                    .HasName("PRIMARY");

                entity.ToTable("Personal");

                entity.HasIndex(e => e.Dni, "Dni")
                    .IsUnique();

                entity.HasIndex(e => e.IdServicio, "Id_Servicio");

                entity.Property(e => e.Apellidos).HasMaxLength(50);

                entity.Property(e => e.Celular).HasMaxLength(9);

                entity.Property(e => e.Condicion).HasMaxLength(50);

                entity.Property(e => e.Dni).HasMaxLength(8);

                entity.Property(e => e.EsActivo).HasColumnName("esActivo");

                //entity.Property(e => e.IdArea).HasColumnName("Id_Area");

                entity.Property(e => e.IdServicio).HasColumnName("Id_Servicio");

                entity.Property(e => e.Nombres).HasMaxLength(50);

              

                entity.HasOne(d => d.IdServicioNavigation)
                    .WithMany(p => p.Personals)
                    .HasForeignKey(d => d.IdServicio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Personal_ibfk_2");
            });

            modelBuilder.Entity<Salida>(entity =>
            {
                entity.HasKey(e => e.IdSalida)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.IdPersonal, "Id_Personal");

                entity.Property(e => e.EsActivo).HasColumnName("esActivo");

                entity.Property(e => e.FechaSalida).HasColumnType("datetime");

                entity.Property(e => e.IdPersonal).HasColumnName("Id_Personal");

                entity.Property(e => e.NumeroPedido).HasMaxLength(10);

                entity.Property(e => e.Recepcion).HasMaxLength(50);

                entity.HasOne(d => d.IdPersonalNavigation)
                    .WithMany(p => p.Salida)
                    .HasForeignKey(d => d.IdPersonal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Salida_ibfk_1");
            });

            modelBuilder.Entity<Servicio>(entity =>
            {
                entity.HasKey(e => e.IdServicio)
                    .HasName("PRIMARY");

                entity.ToTable("Servicio");

                entity.HasIndex(e => e.IdArea, "Id_Area");

                entity.Property(e => e.Descripcion).HasMaxLength(50);

                entity.Property(e => e.EsActivo).HasColumnName("esActivo");

                entity.Property(e => e.IdArea).HasColumnName("Id_Area");

                entity.HasOne(d => d.IdAreaNavigation)
                    .WithMany(p => p.Servicios)
                    .HasForeignKey(d => d.IdArea)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Servicio_ibfk_1");
            });

            modelBuilder.Entity<Subcategoria>(entity =>
            {
                entity.HasKey(e => e.IdSubcategoria)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.IdCategoria, "Id_Categoria");

                entity.Property(e => e.Descripcion).HasMaxLength(50);

                entity.Property(e => e.EsActivo).HasColumnName("esActivo");

                entity.Property(e => e.IdCategoria).HasColumnName("Id_Categoria");

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Subcategoria)
                    .HasForeignKey(d => d.IdCategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Subcategoria_ibfk_1");
            });

            modelBuilder.Entity<UnidadMedida>(entity =>
            {
                entity.HasKey(e => e.IdUnidadMedida)
                    .HasName("PRIMARY");

                entity.Property(e => e.Descripcion).HasMaxLength(50);

                entity.Property(e => e.EsActivo).HasColumnName("esActivo");
            });

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
