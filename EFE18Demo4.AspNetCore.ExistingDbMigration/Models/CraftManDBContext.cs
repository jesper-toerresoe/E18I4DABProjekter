using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EFE18Demo4.AspNetCore.ExistingDbMigration.Models
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

        public virtual DbSet<Haandvaerker> Haandvaerker { get; set; }
        public virtual DbSet<Vaerktoej> Vaerktoej { get; set; }
        public virtual DbSet<Vaerktoejskasse> Vaerktoejskasse { get; set; }

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
            modelBuilder.Entity<Haandvaerker>(entity =>
            {
                entity.Property(e => e.Ansaettelsedato).HasColumnType("date");

                entity.Property(e => e.Efternavn)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Fagomraade).HasMaxLength(50);

                entity.Property(e => e.Fornavn)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Vaerktoej>(entity =>
            {
                entity.HasKey(e => e.VaerktoejsId);

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
                    .WithMany(p => p.Vaerktoej)
                    .HasForeignKey(d => d.Vtkid)
                    .HasConstraintName("FK_Værktøj_Værktøjskasse");
            });

            modelBuilder.Entity<Vaerktoejskasse>(entity =>
            {
                entity.HasKey(e => e.VkasseId);

                entity.Property(e => e.VkasseId).HasColumnName("VKasseId");

                entity.Property(e => e.HaandvaerkerId).HasColumnName("HåndværkerID");

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

                entity.HasOne(d => d.Haandvaerker)
                    .WithMany(p => p.Vaerktoejskasse)
                    .HasForeignKey(d => d.HaandvaerkerId)
                    .HasConstraintName("FK_Værktøjskasse_ToHåndværker");
            });
        }
    }
}
