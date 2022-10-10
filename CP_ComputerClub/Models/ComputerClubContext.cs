using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CP_ComputerClub.Models
{
    public partial class ComputerClubContext : DbContext
    {
        public ComputerClubContext()
        {
        }

        public ComputerClubContext(DbContextOptions<ComputerClubContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Computer> Computers { get; set; } = null!;
        public virtual DbSet<FeaturesPc> FeaturesPcs { get; set; } = null!;
        public virtual DbSet<History> Histories { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-PIQBD5T;Initial Catalog=ComputerClub;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Computer>(entity =>
            {
                entity.Property(e => e.TimeEnd).HasColumnType("datetime");

                entity.Property(e => e.TimeStart).HasColumnType("datetime");

                entity.Property(e => e.TypePcid).HasColumnName("TypePCId");

                entity.HasOne(d => d.TypePc)
                    .WithMany(p => p.Computers)
                    .HasForeignKey(d => d.TypePcid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Computers__TypeP__440B1D61");
            });

            modelBuilder.Entity<FeaturesPc>(entity =>
            {
                entity.ToTable("FeaturesPC");

                entity.Property(e => e.Cpu)
                    .HasMaxLength(50)
                    .HasColumnName("CPU");

                entity.Property(e => e.Headphones).HasMaxLength(50);

                entity.Property(e => e.Keyboard).HasMaxLength(50);

                entity.Property(e => e.Monitor).HasMaxLength(50);

                entity.Property(e => e.Mouse).HasMaxLength(50);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.Ram)
                    .HasMaxLength(50)
                    .HasColumnName("RAM");

                entity.Property(e => e.TypePc)
                    .HasMaxLength(20)
                    .HasColumnName("TypePC");

                entity.Property(e => e.VideoCard).HasMaxLength(50);
            });

            modelBuilder.Entity<History>(entity =>
            {
                entity.ToTable("History");

                entity.Property(e => e.TimeEnd).HasColumnType("datetime");

                entity.Property(e => e.TimeStart).HasColumnType("datetime");

                entity.Property(e => e.TotalSum).HasColumnType("money");

                entity.Property(e => e.TypePcid).HasColumnName("TypePCId");

                entity.HasOne(d => d.TypePc)
                    .WithMany(p => p.Histories)
                    .HasForeignKey(d => d.TypePcid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__History__TypePCI__4AB81AF0");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
