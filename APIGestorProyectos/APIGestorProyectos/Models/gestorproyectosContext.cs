using System;
using APIGestorProyectos.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace APIGestorProyectos.Models
{
    public partial class gestorproyectosContext : DbContext
    {
        public gestorproyectosContext()
        {
        }

        public gestorproyectosContext(DbContextOptions<gestorproyectosContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Empresa> Empresas { get; set; }
        public virtual DbSet<Estado> Estados { get; set; }
        public virtual DbSet<Proyecto> Proyectos { get; set; }
        public virtual DbSet<Responsable> Responsables { get; set; }
        public virtual DbSet<Tarea> Tareas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //{
            //    optionsBuilder.UseSqlServer("Server=.\\SQLExpress;user=LAPTOP-932HCS9I\\Eduar;password=;Database=gestorproyectos;Trusted_Connection=True;");
            //}
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Empresa>(entity =>
            {
                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Estado>(entity =>
            {
                entity.ToTable("Estado");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Proyecto>(entity =>
            {
                entity.Property(e => e.Descripcion).IsUnicode(false);

                entity.Property(e => e.FechaAsignacion).HasColumnType("datetime");

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.FechaTermino).HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdEmpresaNavigation)
                    .WithMany(p => p.Proyectos)
                    .HasForeignKey(d => d.IdEmpresa)
                    .HasConstraintName("FK_Proyectos_Empresas");

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.Proyectos)
                    .HasForeignKey(d => d.IdEstado)
                    .HasConstraintName("FK_Proyectos_Estado");

                entity.HasOne(d => d.IdResponsableNavigation)
                    .WithMany(p => p.Proyectos)
                    .HasForeignKey(d => d.IdResponsable)
                    .HasConstraintName("FK_Proyectos_Responsable");
            });

            modelBuilder.Entity<Responsable>(entity =>
            {
                entity.ToTable("Responsable");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Tarea>(entity =>
            {
                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FechaAsignacion).HasColumnType("datetime");

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.FechaTermino).HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.Tareas)
                    .HasForeignKey(d => d.IdEstado)
                    .HasConstraintName("FK_Tareas_Estado");

                entity.HasOne(d => d.IdProyectoNavigation)
                    .WithMany(p => p.Tareas)
                    .HasForeignKey(d => d.IdProyecto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tareas_Proyectos");

                entity.HasOne(d => d.IdResponsableNavigation)
                    .WithMany(p => p.Tareas)
                    .HasForeignKey(d => d.IdResponsable)
                    .HasConstraintName("FK_Tareas_Responsable");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
