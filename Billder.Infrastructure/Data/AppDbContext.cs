﻿using System;
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

                entity.Property(e => e.Ciudad)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

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
                    .HasMaxLength(50)
                    .IsUnicode(false);
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
                    .HasConstraintName("FK__Contrato__Trabaj__52593CB8");
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
            });

            modelBuilder.Entity<Material>(entity =>
            {
                entity.ToTable("Material");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(255)
                    .IsUnicode(false);
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
            });

            modelBuilder.Entity<Presupuesto>(entity =>
            {
                entity.ToTable("Presupuesto");

                entity.Property(e => e.EstadoPresupuesto).HasMaxLength(50);
            });

            modelBuilder.Entity<PresupuestoEmpleado>(entity =>
            {
                entity.ToTable("PresupuestoEmpleado");

                entity.Property(e => e.CostoHora).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.HorasTrabajadas).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Empleado)
                    .WithMany(p => p.PresupuestoEmpleados)
                    .HasForeignKey(d => d.EmpleadoId)
                    .HasConstraintName("FK__Presupues__Emple__4F7CD00D");

                entity.HasOne(d => d.Presupuesto)
                    .WithMany(p => p.PresupuestoEmpleados)
                    .HasForeignKey(d => d.PresupuestoId)
                    .HasConstraintName("FK__Presupues__Presu__4E88ABD4");
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
                    .HasConstraintName("FK__Presupues__Mater__5535A963");

                entity.HasOne(d => d.Presupuesto)
                    .WithMany(p => p.PresupuestoMaterials)
                    .HasForeignKey(d => d.PresupuestoID)
                    .HasConstraintName("FK__Presupues__Presu__5441852A");
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
                    .HasConstraintName("FK__Trabajo__Cliente__5070F446");

                entity.HasOne(d => d.Presupuesto)
                    .WithMany(p => p.Trabajos)
                    .HasForeignKey(d => d.PresupuestoId)
                    .HasConstraintName("FK__Trabajo__Presupu__5165187F");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuario");

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
