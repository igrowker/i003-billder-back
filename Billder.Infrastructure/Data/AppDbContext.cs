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
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("Cliente");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Ciudad)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Identificacion).HasMaxLength(255);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NroIdentificacion)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Pais)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Provincia)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Contrato>(entity =>
            {
                entity.ToTable("Contrato");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Condiciones)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Trabajo)
                    .WithMany(p => p.Contratos)
                    .HasForeignKey(d => d.TrabajoId)
                    .HasConstraintName("FK__Contrato__Trabaj__5070F446");
            });

            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.ToTable("Empleado");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DNI)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Fullname)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Puesto)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Material>(entity =>
            {
                entity.ToTable("Material");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Pago>(entity =>
            {
                entity.ToTable("Pago");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.FechaPago).HasColumnType("datetime");

                entity.Property(e => e.Metodo).HasMaxLength(255);

                entity.Property(e => e.Monto).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Trabajo)
                    .WithMany(p => p.Pagos)
                    .HasForeignKey(d => d.TrabajoId)
                    .HasConstraintName("FK__Pago__TrabajoId__5165187F");
            });

            modelBuilder.Entity<Presupuesto>(entity =>
            {
                entity.ToTable("Presupuesto");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.EstadoPresupuesto).HasMaxLength(255);
            });

            modelBuilder.Entity<PresupuestoEmpleado>(entity =>
            {
                entity.ToTable("PresupuestoEmpleado");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CostoHora).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.HorasTrabajadas).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Empleado)
                    .WithMany(p => p.PresupuestoEmpleados)
                    .HasForeignKey(d => d.EmpleadoId)
                    .HasConstraintName("FK__Presupues__Emple__4D94879B");

                entity.HasOne(d => d.Presupuesto)
                    .WithMany(p => p.PresupuestoEmpleados)
                    .HasForeignKey(d => d.PresupuestoId)
                    .HasConstraintName("FK__Presupues__Presu__4CA06362");
            });

            modelBuilder.Entity<PresupuestoMaterial>(entity =>
            {
                entity.ToTable("PresupuestoMaterial");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Costo).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Observacion)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Material)
                    .WithMany(p => p.PresupuestoMaterials)
                    .HasForeignKey(d => d.MaterialID)
                    .HasConstraintName("FK__Presupues__Mater__534D60F1");

                entity.HasOne(d => d.Presupuesto)
                    .WithMany(p => p.PresupuestoMaterials)
                    .HasForeignKey(d => d.PresupuestoID)
                    .HasConstraintName("FK__Presupues__Presu__52593CB8");
            });

            modelBuilder.Entity<Trabajo>(entity =>
            {
                entity.ToTable("Trabajo");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.EstadoTrabajo).HasMaxLength(255);

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.Trabajos)
                    .HasForeignKey(d => d.ClienteId)
                    .HasConstraintName("FK__Trabajo__Cliente__4E88ABD4");

                entity.HasOne(d => d.Presupuesto)
                    .WithMany(p => p.Trabajos)
                    .HasForeignKey(d => d.PresupuestoId)
                    .HasConstraintName("FK__Trabajo__Presupu__4F7CD00D");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuario");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Ciudad)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DNI)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FechaNacimiento).HasColumnType("date");

                entity.Property(e => e.FullName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Pais)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Provincia)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
