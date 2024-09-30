using System;
using System.Collections.Generic;
using Billder.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Billder.Infrastructure.Data
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Contrato> Contratos { get; set; } = null!;
        public virtual DbSet<Empleado> Empleados { get; set; } = null!;
        public virtual DbSet<Material> Materials { get; set; } = null!;
        public virtual DbSet<Pago> Pagos { get; set; } = null!;
        public virtual DbSet<Presupuesto> Presupuestos { get; set; } = null!;
        public virtual DbSet<PresupuestoEmpleado> PresupuestoEmpleados { get; set; } = null!;
        public virtual DbSet<PresupuestoMaterial> PresupuestoMaterials { get; set; } = null!;
        public virtual DbSet<Trabajo> Trabajos { get; set; } = null!;
        public virtual DbSet<UsuarioRegistrado> UsuarioRegistrados { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("Cliente");

                entity.Property(e => e.Ciudad)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FechaAlta)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Identificacion).HasMaxLength(50);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NroIdentificacion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Pais)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Provincia)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cliente__Usuario__3E52440B");
            });

            modelBuilder.Entity<Contrato>(entity =>
            {
                entity.ToTable("Contrato");

                entity.Property(e => e.Condiciones)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Trabajo)
                    .WithMany(p => p.Contratos)
                    .HasForeignKey(d => d.TrabajoId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Contrato__Trabaj__4E88ABD4");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Contratos)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Contrato__Usuari__4F7CD00D");
            });

            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.ToTable("Empleado");

                entity.Property(e => e.Fullname)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Identificacion).HasMaxLength(50);

                entity.Property(e => e.NroIdentificacion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Puesto)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Empleados)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Empleado__Usuari__45F365D3");
            });

            modelBuilder.Entity<Material>(entity =>
            {
                entity.ToTable("Material");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Materials)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Material__Usuari__571DF1D5");
            });

            modelBuilder.Entity<Pago>(entity =>
            {
                entity.ToTable("Pago");

                entity.Property(e => e.FechaPago).HasColumnType("datetime");

                entity.Property(e => e.Metodo).HasMaxLength(50);

                entity.Property(e => e.Monto).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Trabajo)
                    .WithMany(p => p.Pagos)
                    .HasForeignKey(d => d.TrabajoId)
                    .HasConstraintName("FK__Pago__TrabajoId__534D60F1");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Pagos)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Pago__UsuarioId__5441852A");
            });

            modelBuilder.Entity<Presupuesto>(entity =>
            {
                entity.ToTable("Presupuesto");

                entity.Property(e => e.EstadoPresupuesto).HasMaxLength(50);

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Presupuestos)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Presupues__Usuar__4222D4EF");
            });

            modelBuilder.Entity<PresupuestoEmpleado>(entity =>
            {
                entity.ToTable("PresupuestoEmpleado");

                entity.Property(e => e.CostoHora).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.HorasTrabajadas).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Empleado)
                    .WithMany(p => p.PresupuestoEmpleados)
                    .HasForeignKey(d => d.EmpleadoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Presupues__Emple__5AEE82B9");

                entity.HasOne(d => d.Presupuesto)
                    .WithMany(p => p.PresupuestoEmpleados)
                    .HasForeignKey(d => d.PresupuestoId)
                    .HasConstraintName("FK__Presupues__Presu__59FA5E80");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.PresupuestoEmpleados)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Presupues__Usuar__5BE2A6F2");
            });

            modelBuilder.Entity<PresupuestoMaterial>(entity =>
            {
                entity.ToTable("PresupuestoMaterial");

                entity.Property(e => e.Costo).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Observacion)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Material)
                    .WithMany(p => p.PresupuestoMaterials)
                    .HasForeignKey(d => d.MaterialID)
                    .HasConstraintName("FK__Presupues__Mater__5FB337D6");

                entity.HasOne(d => d.Presupuesto)
                    .WithMany(p => p.PresupuestoMaterials)
                    .HasForeignKey(d => d.PresupuestoID)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Presupues__Presu__5EBF139D");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.PresupuestoMaterials)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Presupues__Usuar__60A75C0F");
            });

            modelBuilder.Entity<Trabajo>(entity =>
            {
                entity.ToTable("Trabajo");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.EstadoTrabajo).HasMaxLength(50);

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.Trabajos)
                    .HasForeignKey(d => d.ClienteId)
                    .HasConstraintName("FK__Trabajo__Cliente__49C3F6B7");

                entity.HasOne(d => d.Presupuesto)
                    .WithMany(p => p.Trabajos)
                    .HasForeignKey(d => d.PresupuestoId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__Trabajo__Presupu__4AB81AF0");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Trabajos)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Trabajo__Usuario__4BAC3F29");
            });

            modelBuilder.Entity<UsuarioRegistrado>(entity =>
            {
                entity.ToTable("UsuarioRegistrado");

                entity.HasIndex(e => e.NroIdentificacion, "UQ__UsuarioR__0546230441777584")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__UsuarioR__A9D10534A85F105E")
                    .IsUnique();

                entity.Property(e => e.Ciudad)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FechaNacimiento).HasColumnType("date");

                entity.Property(e => e.FullName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Identificacion).HasMaxLength(50);

                entity.Property(e => e.NroIdentificacion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Pais)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Provincia)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
