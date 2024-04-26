using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace pr20.database;

public partial class Pr20Context : DbContext
{
    public Pr20Context()
    {
    }

    public Pr20Context(DbContextOptions<Pr20Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Catalog> Catalogs { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Kakoe> Kakoes { get; set; }

    public virtual DbSet<Nikakoe> Nikakoes { get; set; }

    public virtual DbSet<Zakazy> Zakazies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAB17-13\\SQLEXPRESS; Database=pr20; User=ИСП-31; Password=1234567890; Encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Catalog>(entity =>
        {
            entity.HasKey(e => e.CodeObj).HasName("PK_Каталог_товаров");

            entity.ToTable("Catalog");

            entity.Property(e => e.CodeObj).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Price).HasColumnType("money");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.CodeClient).HasName("PK_Клиенты");

            entity.Property(e => e.CodeClient).ValueGeneratedNever();
        });

        modelBuilder.Entity<Kakoe>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("kakoe");

            entity.Property(e => e.Month).HasColumnName("MONTH");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Nikakoe>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("nikakoe");

            entity.Property(e => e.Summa).HasColumnType("money");
        });

        modelBuilder.Entity<Zakazy>(entity =>
        {
            entity.HasKey(e => e.NumZak).HasName("PK_Заказы");

            entity.ToTable("Zakazy");

            entity.Property(e => e.NumZak).ValueGeneratedNever();

            entity.HasOne(d => d.CodeClientNavigation).WithMany(p => p.Zakazies)
                .HasForeignKey(d => d.CodeClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Заказы_Клиенты");

            entity.HasOne(d => d.CodeObjNavigation).WithMany(p => p.Zakazies)
                .HasForeignKey(d => d.CodeObj)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Заказы_Каталог_товаров");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
