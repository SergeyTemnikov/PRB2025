using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebRoadsApi.Models;

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

            entity.HasOne(d => d.IdParentDepartamentNavigation).WithMany(p => p.InverseIdParentDepartamentNavigation)
                .HasForeignKey(d => d.IdParentDepartament)
                .HasConstraintName("FK_Departament_Departament");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
