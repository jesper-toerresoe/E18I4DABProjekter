using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EFE18Demo2.AspNetCore.ExistingDbMigration.Models
{
    public partial class CraftManDBContext : DbContext
    {
        public CraftManDBContext()
        {
        }

        public CraftManDBContext(DbContextOptions<CraftManDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Håndværker> Håndværker { get; set; }
        public virtual DbSet<Værktøj> Værktøj { get; set; }
        public virtual DbSet<Værktøjskasse> Værktøjskasse { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Server=(localdb)\\ProjectsV13;Database=CraftManDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Håndværker>(entity =>
            {
                entity.Property(e => e.Ansættelsedato).HasColumnType("date");

                entity.Property(e => e.Efternavn)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Fagområde).HasMaxLength(50);

                entity.Property(e => e.Fornavn)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Værktøj>(entity =>
            {
                entity.HasKey(e => e.VærktøjsId);

                entity.Property(e => e.Vtanskaffet)
                    .HasColumnName("VTAnskaffet")
                    .HasColumnType("date");

                entity.Property(e => e.Vtfabrikat).HasColumnName("VTFabrikat");

                entity.Property(e => e.Vtkid).HasColumnName("VTKId");

                entity.Property(e => e.Vtmodel)
                    .HasColumnName("VTModel")
                    .HasMaxLength(50);

                entity.Property(e => e.Vtserienr)
                    .HasColumnName("VTSerienr")
                    .HasMaxLength(50);

                entity.Property(e => e.Vttype)
                    .HasColumnName("VTType")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Vtk)
                    .WithMany(p => p.Værktøj)
                    .HasForeignKey(d => d.Vtkid)
                    .HasConstraintName("FK_Værktøj_Værktøjskasse");
            });

            modelBuilder.Entity<Værktøjskasse>(entity =>
            {
                entity.HasKey(e => e.VkasseId);

                entity.Property(e => e.VkasseId).HasColumnName("VKasseId");

                entity.Property(e => e.HåndværkerId).HasColumnName("HåndværkerID");

                entity.Property(e => e.Vtkanskaffet)
                    .HasColumnName("VTKAnskaffet")
                    .HasColumnType("date");

                entity.Property(e => e.Vtkfabrikat)
                    .HasColumnName("VTKFabrikat")
                    .HasMaxLength(50);

                entity.Property(e => e.Vtkfarve).HasColumnName("VTKFarve");

                entity.Property(e => e.Vtkmodel)
                    .HasColumnName("VTKModel")
                    .HasMaxLength(50);

                entity.Property(e => e.Vtkserienummer)
                    .HasColumnName("VTKSerienummer")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Håndværker)
                    .WithMany(p => p.Værktøjskasse)
                    .HasForeignKey(d => d.HåndværkerId)
                    .HasConstraintName("FK_Værktøjskasse_ToHåndværker");
            });
        }
    }
}
