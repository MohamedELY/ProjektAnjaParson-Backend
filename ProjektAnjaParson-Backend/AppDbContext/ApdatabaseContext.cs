using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ProjektAnjaParson_Backend.Models;

namespace ProjektAnjaParson_Backend.AppDbContext;

public partial class ApdatabaseContext : DbContext
{
    public ApdatabaseContext()
    {
    }

    public ApdatabaseContext(DbContextOptions<ApdatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<FirstName> FirstNames { get; set; }

    public virtual DbSet<FullName> FullNames { get; set; }

    public virtual DbSet<LastName> LastNames { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Place> Places { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:anjap.database.windows.net,1433;Initial Catalog=apdatabase;Persist Security Info=False;User ID=apsql;Password=Anjapärson1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Category__3214EC079A0E7EFB");

            entity.ToTable("Category");

            entity.Property(e => e.Icon).HasMaxLength(1000);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Country__3214EC07CA88B905");

            entity.ToTable("Country");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<FirstName>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FirstNam__3214EC079FC66422");

            entity.Property(e => e.FirstName1)
                .HasMaxLength(25)
                .HasColumnName("FirstName");
        });

        modelBuilder.Entity<FullName>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FullName__3214EC07E7DB1EC8");

            entity.HasOne(d => d.FirstName).WithMany(p => p.FullNames)
                .HasForeignKey(d => d.FirstNameId)
                .HasConstraintName("FK_FullNames.FirstNameId");

            entity.HasOne(d => d.LastName).WithMany(p => p.FullNames)
                .HasForeignKey(d => d.LastNameId)
                .HasConstraintName("FK_FullNames.LastNameId");
        });

        modelBuilder.Entity<LastName>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LastName__3214EC0715072CD7");

            entity.Property(e => e.LastName1)
                .HasMaxLength(25)
                .HasColumnName("LastName");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Location__3214EC07A0C30D0C");

            entity.ToTable("Location");

            entity.Property(e => e.Name).HasMaxLength(88);

            entity.HasOne(d => d.Country).WithMany(p => p.Locations)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("FK_Location.CountryId");
        });

        modelBuilder.Entity<Place>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Place__3214EC07ACF4C66A");

            entity.ToTable("Place");

            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(35);

            entity.HasOne(d => d.Category).WithMany(p => p.Places)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Place.CategoryId");

            entity.HasOne(d => d.Location).WithMany(p => p.Places)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("FK_Place.LocationId");

            entity.Property(e => e.Pic).HasMaxLength(750);
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Post__3214EC070C47AFE6");

            entity.ToTable("Post");

            entity.Property(e => e.Description).HasMaxLength(900);
            entity.Property(e => e.Title).HasMaxLength(50);

            entity.HasOne(d => d.Place).WithMany(p => p.Posts)
                .HasForeignKey(d => d.PlaceId)
                .HasConstraintName("FK_Post.PlaceId");

            entity.HasOne(d => d.User).WithMany(p => p.Posts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Post.UserId");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07D6D68288");

            entity.Property(e => e.Password).HasMaxLength(30);
            entity.Property(e => e.Username).HasMaxLength(30);

            entity.HasOne(d => d.FullName).WithMany(p => p.Users)
                .HasForeignKey(d => d.FullNameId)
                .HasConstraintName("FK_Users.FullNameId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
