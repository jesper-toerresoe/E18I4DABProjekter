using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace EFGetStarted.AspNetCore.ExistingDbMigration.Models
{
    public partial class Database5Context : DbContext
    {
        public Database5Context()
        {
        }

        public Database5Context(DbContextOptions<Database5Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Haandvaerker> Haandvaerker { get; set; }

        public virtual DbSet<Vaerktoej> Vaerktoej { get; set; }
        public virtual DbSet<Vaerktoejskasse> Vaerktoejskasse { get; set; }

        public static readonly LoggerFactory SQLLoggerFactory
               = new LoggerFactory(new[]
                 {
                   new ConsoleLoggerProvider((category, level)
                    => category == DbLoggerCategory.Database.Command.Name
                        /*&& level == LogLevel.Information*/, true)
        });

        //    public static readonly LoggerFactory MyLoggerFactory
        //= new LoggerFactory(new[] { new ConsoleLoggerProvider((_, __) => true, true) });

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseLoggerFactory(SQLLoggerFactory);

            //            if (!optionsBuilder.IsConfigured)
            //            {
            //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            //                optionsBuilder.UseSqlServer("Server=(localdb)\\ProjectsV13;Database=Database4;Trusted_Connection=True;");
            //            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Haandvaerker>(entity =>
            {
                entity.Property(e => e.HVAnsaettelsedato).HasColumnType("date");

                entity.Property(e => e.HVEfternavn)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.HVFagomraade).HasMaxLength(50);

                entity.Property(e => e.HVFornavn)
                    .IsRequired()
                    .HasMaxLength(50);
            });


            modelBuilder.Entity<Vaerktoej>(entity =>
            {
                entity.HasKey(e => e.VTId);

                entity.Property(e => e.VTAnskaffet).HasColumnType("date");

                entity.Property(e => e.LiggerIvtk).HasColumnName("LiggerIVTK");

                entity.Property(e => e.VTModel).HasMaxLength(50);

                entity.Property(e => e.VTSerienr).HasMaxLength(50);

                entity.Property(e => e.VTType).HasMaxLength(50);

                entity.HasOne(d => d.LiggerIvtkNavigation)
                    .WithMany(p => p.Vaerktoej)
                    .HasForeignKey(d => d.LiggerIvtk)
                    .HasConstraintName("FK_Vaerktoej_Vaerktsejskasse");
            });

            modelBuilder.Entity<Vaerktoejskasse>(entity =>
            {
                entity.HasKey(e => e.VTKId);

                entity.Property(e => e.VTKId).HasColumnName("VKasseId");

                entity.Property(e => e.VTKAnskaffet).HasColumnType("date");

                entity.Property(e => e.VTKFabrikat).HasMaxLength(50);

                entity.Property(e => e.VTKModel).HasMaxLength(50);

                entity.Property(e => e.VTKSerienummer).HasMaxLength(50);

                entity.HasOne(d => d.EjesAfNavigation)
                    .WithMany(p => p.Vaerktoejskasse)
                    .HasForeignKey(d => d.VTKEjesAf)
                    .HasConstraintName("FK_Vaerktoejskasse_ToHaandvaerker");
            });


        }
    }
}
