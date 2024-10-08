using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Billder.Infrastructure.Entities;

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
        public virtual DbSet<Gasto> Gastos { get; set; } = null!;
        public virtual DbSet<Pago> Pagos { get; set; } = null!;
        public virtual DbSet<Presupuesto> Presupuestos { get; set; } = null!;
        public virtual DbSet<Trabajo> Trabajos { get; set; } = null!;
        public virtual DbSet<UsuarioRegistrado> UsuarioRegistrados { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("Cliente");

                entity.HasIndex(e => e.NroIdentificacion, "UQ__Cliente__05462304F9170B5C")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__Cliente__A9D10534893235E3")
                    .IsUnique();

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

                entity.Property(e => e.Imagen)
                    .HasMaxLength(255)
                    .IsUnicode(false);

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
                    .HasConstraintName("FK__Cliente__Usuario__440B1D61");
            });

            modelBuilder.Entity<Contrato>(entity =>
            {
                entity.ToTable("Contrato");

                entity.Property(e => e.Condiciones).IsUnicode(false);

                entity.Property(e => e.Estado).HasMaxLength(50);

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaFirma)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FirmaDigital)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Trabajo)
                    .WithMany(p => p.Contratos)
                    .HasForeignKey(d => d.TrabajoId)
                    .HasConstraintName("FK__Contrato__Trabaj__5535A963");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Contratos)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Contrato__Usuari__5629CD9C");
            });

            modelBuilder.Entity<Gasto>(entity =>
            {
                entity.ToTable("Gasto");

                entity.Property(e => e.CostoHoraLaboral).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.HorasTrabajadas).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Precio).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Presupuesto)
                    .WithMany(p => p.Gastos)
                    .HasForeignKey(d => d.PresupuestoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Gasto__Presupues__619B8048");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Gastos)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Gasto__UsuarioId__60A75C0F");
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
                    .HasConstraintName("FK__Pago__TrabajoId__59FA5E80");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Pagos)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Pago__UsuarioId__5AEE82B9");
            });

            modelBuilder.Entity<Presupuesto>(entity =>
            {
                entity.ToTable("Presupuesto");

                entity.Property(e => e.EstadoPresupuesto).HasMaxLength(50);

                entity.Property(e => e.FechaVencimiento)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(dateadd(month,(2),getdate()))");

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.Presupuestos)
                    .HasForeignKey(d => d.ClienteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Presupues__Clien__49C3F6B7");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Presupuestos)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Presupues__Usuar__48CFD27E");
            });

            modelBuilder.Entity<Trabajo>(entity =>
            {
                entity.ToTable("Trabajo");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.EstadoTrabajo).HasMaxLength(50);

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.Property(e => e.Imagen)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.Trabajos)
                    .HasForeignKey(d => d.ClienteId)
                    .HasConstraintName("FK__Trabajo__Cliente__4D94879B");

                entity.HasOne(d => d.Presupuesto)
                    .WithMany(p => p.Trabajos)
                    .HasForeignKey(d => d.PresupuestoId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__Trabajo__Presupu__4E88ABD4");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Trabajos)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Trabajo__Usuario__4F7CD00D");
            });

            modelBuilder.Entity<UsuarioRegistrado>(entity =>
            {
                entity.ToTable("UsuarioRegistrado");

                entity.HasIndex(e => e.NroIdentificacion, "UQ__UsuarioR__05462304B9BFE777")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__UsuarioR__A9D1053477BCA17C")
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

                entity.Property(e => e.Firma)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Identificacion).HasMaxLength(50);

                entity.Property(e => e.Imagen)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NroIdentificacion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Pais)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(160)
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
