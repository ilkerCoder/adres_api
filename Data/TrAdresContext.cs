using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using adres_api.Entities;

namespace adres_api.Data;

public partial class TrAdresContext : DbContext
{
    public TrAdresContext()
    {
    }

    public TrAdresContext(DbContextOptions<TrAdresContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Ilceler> Ilcelers { get; set; }

    public virtual DbSet<Iller> Illers { get; set; }

    public virtual DbSet<Mahalleler> Mahallelers { get; set; }

    public virtual DbSet<Sokaklar> Sokaklars { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ilceler>(entity =>
        {
            entity.HasKey(e => e.IlceId);

            entity.ToTable("ilceler");

            entity.Property(e => e.IlceId)
                .ValueGeneratedNever()
                .HasColumnType("INT")
                .HasColumnName("ilce_id");
            entity.Property(e => e.IlAdi)
                .HasDefaultValueSql("NULL")
                .HasColumnType("varchar(255)")
                .HasColumnName("il_adi");
            entity.Property(e => e.IlId)
                .HasDefaultValueSql("NULL")
                .HasColumnType("INT")
                .HasColumnName("il_id");
            entity.Property(e => e.IlceAdi)
                .HasDefaultValueSql("NULL")
                .HasColumnType("varchar(255)")
                .HasColumnName("ilce_adi");
        });

        modelBuilder.Entity<Iller>(entity =>
        {
            entity.HasKey(e => e.IlId);

            entity.ToTable("iller");

            entity.Property(e => e.IlId)
                .ValueGeneratedNever()
                .HasColumnType("INT")
                .HasColumnName("il_id");
            entity.Property(e => e.IlAdi)
                .HasDefaultValueSql("NULL")
                .HasColumnType("varchar(255)")
                .HasColumnName("il_adi");
        });

        modelBuilder.Entity<Mahalleler>(entity =>
        {
            entity.HasKey(e => e.MahalleId);

            entity.ToTable("mahalleler");

            entity.Property(e => e.MahalleId)
                .ValueGeneratedNever()
                .HasColumnType("INT")
                .HasColumnName("mahalle_id");
            entity.Property(e => e.IlAdi)
                .HasDefaultValueSql("NULL")
                .HasColumnType("varchar(255)")
                .HasColumnName("il_adi");
            entity.Property(e => e.IlId)
                .HasDefaultValueSql("NULL")
                .HasColumnType("INT")
                .HasColumnName("il_id");
            entity.Property(e => e.IlceAdi)
                .HasDefaultValueSql("NULL")
                .HasColumnType("varchar(255)")
                .HasColumnName("ilce_adi");
            entity.Property(e => e.IlceId)
                .HasDefaultValueSql("NULL")
                .HasColumnType("INT")
                .HasColumnName("ilce_id");
            entity.Property(e => e.MahalleAdi)
                .HasDefaultValueSql("NULL")
                .HasColumnType("varchar(255)")
                .HasColumnName("mahalle_adi");
        });

        modelBuilder.Entity<Sokaklar>(entity =>
        {
            entity.HasKey(e => e.SokakId);

            entity.ToTable("sokaklar");

            entity.Property(e => e.SokakId)
                .ValueGeneratedNever()
                .HasColumnType("INT")
                .HasColumnName("sokak_id");
            entity.Property(e => e.IlAdi)
                .HasDefaultValueSql("NULL")
                .HasColumnType("varchar(255)")
                .HasColumnName("il_adi");
            entity.Property(e => e.IlId)
                .HasDefaultValueSql("NULL")
                .HasColumnType("INT")
                .HasColumnName("il_id");
            entity.Property(e => e.IlceAdi)
                .HasDefaultValueSql("NULL")
                .HasColumnType("varchar(255)")
                .HasColumnName("ilce_adi");
            entity.Property(e => e.IlceId)
                .HasDefaultValueSql("NULL")
                .HasColumnType("INT")
                .HasColumnName("ilce_id");
            entity.Property(e => e.MahalleAdi)
                .HasDefaultValueSql("NULL")
                .HasColumnType("varchar(255)")
                .HasColumnName("mahalle_adi");
            entity.Property(e => e.MahalleId)
                .HasDefaultValueSql("NULL")
                .HasColumnType("INT")
                .HasColumnName("mahalle_id");
            entity.Property(e => e.SokakAdi)
                .HasDefaultValueSql("NULL")
                .HasColumnType("varchar(255)")
                .HasColumnName("sokak_adi");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
