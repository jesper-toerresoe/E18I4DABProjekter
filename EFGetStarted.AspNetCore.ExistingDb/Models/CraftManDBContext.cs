using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EFGetStarted.AspNetCore.ExistingDb.Models
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

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Server=(localdb)\\ProjectsV13;Database=Database3;Trusted_Connection=True;");
//            }
//        }

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
