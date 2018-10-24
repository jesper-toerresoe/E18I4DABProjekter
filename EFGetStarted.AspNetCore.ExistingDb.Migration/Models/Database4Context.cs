using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EFGetStarted.AspNetCore.ExistingDb.Migration.Models
{
    public partial class Database4Context : DbContext
    {
        public Database4Context()
        {
        }

        public Database4Context(DbContextOptions<Database4Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Haandvaerker> Haandvaerker { get; set; }
        public virtual DbSet<Håndværker> Håndværker { get; set; }
        public virtual DbSet<Vaerktoej> Vaerktoej { get; set; }
        public virtual DbSet<Vaerktoejskasse> Vaerktoejskasse { get; set; }
        public virtual DbSet<Værktøj> Værktøj { get; set; }
        public virtual DbSet<Værktøjskasse> Værktøjskasse { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\ProjectsV13;Database=Database4;Trusted_Connection=True;");
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

            modelBuilder.Entity<Vaerktoej>(entity =>
            {
                entity.HasKey(e => e.VaerktoejsId);

                entity.Property(e => e.Anskaffet).HasColumnType("date");

                entity.Property(e => e.LiggerIvtk).HasColumnName("LiggerIVTK");

                entity.Property(e => e.Model).HasMaxLength(50);

                entity.Property(e => e.Serienr).HasMaxLength(50);

                entity.Property(e => e.Type).HasMaxLength(50);

                entity.HasOne(d => d.LiggerIvtkNavigation)
                    .WithMany(p => p.Vaerktoej)
                    .HasForeignKey(d => d.LiggerIvtk)
                    .HasConstraintName("FK_Vaerktoej_Vaerktsejskasse");
            });

            modelBuilder.Entity<Vaerktoejskasse>(entity =>
            {
                entity.HasKey(e => e.VkasseId);

                entity.Property(e => e.VkasseId).HasColumnName("VKasseId");

                entity.Property(e => e.Anskaffet).HasColumnType("date");

                entity.Property(e => e.Fabrikat).HasMaxLength(50);

                entity.Property(e => e.Model).HasMaxLength(50);

                entity.Property(e => e.Serienummer).HasMaxLength(50);

                entity.HasOne(d => d.EjesAfNavigation)
                    .WithMany(p => p.Vaerktoejskasse)
                    .HasForeignKey(d => d.EjesAf)
                    .HasConstraintName("FK_Vaerktoejskasse_ToHaandvaerker");
            });

            modelBuilder.Entity<Værktøj>(entity =>
            {
                entity.HasKey(e => e.VærktøjsId);

                entity.Property(e => e.Anskaffet).HasColumnType("date");

                entity.Property(e => e.LiggerIvtk).HasColumnName("LiggerIVTK");

                entity.Property(e => e.Model).HasMaxLength(50);

                entity.Property(e => e.Serienr).HasMaxLength(50);

                entity.Property(e => e.Type).HasMaxLength(50);

                entity.HasOne(d => d.LiggerIvtkNavigation)
                    .WithMany(p => p.Værktøj)
                    .HasForeignKey(d => d.LiggerIvtk)
                    .HasConstraintName("FK_Værktøj_Værktøjskasse");
            });

            modelBuilder.Entity<Værktøjskasse>(entity =>
            {
                entity.HasKey(e => e.VkasseId);

                entity.Property(e => e.VkasseId).HasColumnName("VKasseId");

                entity.Property(e => e.Anskaffet).HasColumnType("date");

                entity.Property(e => e.Fabrikat).HasMaxLength(50);

                entity.Property(e => e.Model).HasMaxLength(50);

                entity.Property(e => e.Serienummer).HasMaxLength(50);

                entity.HasOne(d => d.EjesAfNavigation)
                    .WithMany(p => p.Værktøjskasse)
                    .HasForeignKey(d => d.EjesAf)
                    .HasConstraintName("FK_Værktøjskasse_ToHåndværker");
            });
        }
    }
}
