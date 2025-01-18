using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebRoadsApi;

namespace WebRoadsApi.Data;

public partial class RoadsContext : DbContext
{
    public RoadsContext()
    {
    }

    public RoadsContext(DbContextOptions<RoadsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Departament> Departaments { get; set; }

    public virtual DbSet<SubDepartament> SubDepartaments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-3SNADHI;Database=prb;TrustServerCertificate=True;Trusted_Connection=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Departament>(entity =>
        {
            entity.HasKey(e => e.IdDepartament);

            entity.ToTable("Departament");

            entity.Property(e => e.NameDepartament).HasMaxLength(150);
        });

        modelBuilder.Entity<SubDepartament>(entity =>
        {
            entity.HasKey(e => e.IdSubDepartament);

            entity.ToTable("SubDepartament");

            entity.HasOne(d => d.IdDaughterDepartamentNavigation).WithMany(p => p.SubDepartamentIdDaughterDepartamentNavigations)
                .HasForeignKey(d => d.IdDaughterDepartament)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SubDepartament_Departament1");

            entity.HasOne(d => d.IdDepartamentNavigation).WithMany(p => p.SubDepartamentIdDepartamentNavigations)
                .HasForeignKey(d => d.IdDepartament)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SubDepartament_Departament");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
