using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Image_processing.domain.Entities;

namespace Image_processing.infrastructure.DataContext;

public partial class ImageProcessingDbContext : DbContext
{
    public ImageProcessingDbContext()
    {
    }

    public ImageProcessingDbContext(DbContextOptions<ImageProcessingDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Image> Images { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Image_processing;Username=postgres;Password=cesarammar2003");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("images_pkey");

            entity.ToTable("images");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Image1).HasColumnName("image");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
