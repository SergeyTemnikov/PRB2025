using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebRoadsApi.Models;

namespace WebRoadsApi.Data;

public partial class PrbContext : DbContext
{
    public PrbContext()
    {
    }

    public PrbContext(DbContextOptions<PrbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cabinet> Cabinets { get; set; }

    public virtual DbSet<Calendar> Calendars { get; set; }

    public virtual DbSet<Departament> Departaments { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<EventStatus> EventStatuses { get; set; }

    public virtual DbSet<EventType> EventTypes { get; set; }

    public virtual DbSet<HolidayCalendar> HolidayCalendars { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<MaterialStatus> MaterialStatuses { get; set; }

    public virtual DbSet<MaterialType> MaterialTypes { get; set; }

    public virtual DbSet<MissCalendar> MissCalendars { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<TrainingCalendar> TrainingCalendars { get; set; }

    public virtual DbSet<TrainingMaterial> TrainingMaterials { get; set; }

    public virtual DbSet<Worker> Workers { get; set; }

    public virtual DbSet<WorkerPrivateInfo> WorkerPrivateInfos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-3SNADHI;Database=prb;TrustServerCertificate=True;Trusted_Connection=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cabinet>(entity =>
        {
            entity.HasKey(e => e.IdCabinet);

            entity.ToTable("Cabinet");

            entity.Property(e => e.CabinetNumber).HasMaxLength(10);
        });

        modelBuilder.Entity<Calendar>(entity =>
        {
            entity.HasKey(e => e.IdCalendar);

            entity.ToTable("Calendar");

            entity.Property(e => e.EndDateTime).HasColumnType("datetime");
            entity.Property(e => e.StartDateTime).HasColumnType("datetime");

            entity.HasOne(d => d.IdEventNavigation).WithMany(p => p.Calendars)
                .HasForeignKey(d => d.IdEvent)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Calendar_Event");
        });

        modelBuilder.Entity<Departament>(entity =>
        {
            entity.HasKey(e => e.IdDepartament);

            entity.ToTable("Departament");

            entity.Property(e => e.DepartamentDescription).HasMaxLength(250);
            entity.Property(e => e.NameDepartament).HasMaxLength(150);

            entity.HasOne(d => d.IdHeadOfDepartamentNavigation).WithMany(p => p.Departaments)
                .HasForeignKey(d => d.IdHeadOfDepartament)
                .HasConstraintName("FK_Departament_Worker");

            entity.HasOne(d => d.IdParentDepartamentNavigation).WithMany(p => p.InverseIdParentDepartamentNavigation)
                .HasForeignKey(d => d.IdParentDepartament)
                .HasConstraintName("FK_Departament_Departament");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.IdEvent);

            entity.ToTable("Event");

            entity.Property(e => e.Description).HasMaxLength(150);
            entity.Property(e => e.EventName).HasMaxLength(50);

            entity.HasOne(d => d.IdEventStatusNavigation).WithMany(p => p.Events)
                .HasForeignKey(d => d.IdEventStatus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Event_EventStatus");

            entity.HasOne(d => d.IdEventTypeNavigation).WithMany(p => p.Events)
                .HasForeignKey(d => d.IdEventType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Event_EventType");
        });

        modelBuilder.Entity<EventStatus>(entity =>
        {
            entity.HasKey(e => e.IdEventStatus);

            entity.ToTable("EventStatus");

            entity.Property(e => e.StatusName).HasMaxLength(50);
        });

        modelBuilder.Entity<EventType>(entity =>
        {
            entity.HasKey(e => e.IdEventType);

            entity.ToTable("EventType");

            entity.Property(e => e.TypeName).HasMaxLength(50);
        });

        modelBuilder.Entity<HolidayCalendar>(entity =>
        {
            entity.HasKey(e => e.IdHolidayCalendar);

            entity.ToTable("HolidayCalendar");

            entity.HasOne(d => d.IdWorkerNavigation).WithMany(p => p.HolidayCalendars)
                .HasForeignKey(d => d.IdWorker)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HolidayCalendar_Worker");
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.IdMaterial);

            entity.ToTable("Material");

            entity.Property(e => e.MaterialName).HasMaxLength(50);

            entity.HasOne(d => d.IdAuthorNavigation).WithMany(p => p.Materials)
                .HasForeignKey(d => d.IdAuthor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Material_Worker");

            entity.HasOne(d => d.IdStatusNavigation).WithMany(p => p.Materials)
                .HasForeignKey(d => d.IdStatus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Material_MaterialStatus");

            entity.HasOne(d => d.IdTypeNavigation).WithMany(p => p.Materials)
                .HasForeignKey(d => d.IdType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Material_MaterialType");
        });

        modelBuilder.Entity<MaterialStatus>(entity =>
        {
            entity.HasKey(e => e.IdMaterialStatus);

            entity.ToTable("MaterialStatus");

            entity.Property(e => e.StatusName).HasMaxLength(50);
        });

        modelBuilder.Entity<MaterialType>(entity =>
        {
            entity.HasKey(e => e.IdMaterialType);

            entity.ToTable("MaterialType");

            entity.Property(e => e.TypeName).HasMaxLength(50);
        });

        modelBuilder.Entity<MissCalendar>(entity =>
        {
            entity.HasKey(e => e.IdMissCalendar);

            entity.ToTable("MissCalendar");

            entity.HasOne(d => d.IdWorkerNavigation).WithMany(p => p.MissCalendars)
                .HasForeignKey(d => d.IdWorker)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MissCalendar_Worker");
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.IdPosition);

            entity.ToTable("Position");

            entity.Property(e => e.NamePosition).HasMaxLength(50);
        });

        modelBuilder.Entity<TrainingCalendar>(entity =>
        {
            entity.HasKey(e => e.IdTrainingCalendar);

            entity.ToTable("TrainingCalendar");

            entity.Property(e => e.EndDateTime).HasColumnType("datetime");
            entity.Property(e => e.StartDateTime).HasColumnType("datetime");

            entity.HasOne(d => d.IdTrainingNavigation).WithMany(p => p.TrainingCalendars)
                .HasForeignKey(d => d.IdTraining)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TrainingCalendar_Event");

            entity.HasOne(d => d.IdWorkerNavigation).WithMany(p => p.TrainingCalendars)
                .HasForeignKey(d => d.IdWorker)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TrainingCalendar_Worker");
        });

        modelBuilder.Entity<TrainingMaterial>(entity =>
        {
            entity.HasKey(e => e.IdTrainingMaterial);

            entity.ToTable("TrainingMaterial");

            entity.HasOne(d => d.IdMaterialNavigation).WithMany(p => p.TrainingMaterials)
                .HasForeignKey(d => d.IdMaterial)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TrainingMaterial_Material");

            entity.HasOne(d => d.IdTrainingNavigation).WithMany(p => p.TrainingMaterials)
                .HasForeignKey(d => d.IdTraining)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TrainingMaterial_Event");
        });

        modelBuilder.Entity<Worker>(entity =>
        {
            entity.HasKey(e => e.IdWorker);

            entity.ToTable("Worker");

            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FullName).HasMaxLength(50);
            entity.Property(e => e.WorkPhoneNumber).HasMaxLength(20);

            entity.HasOne(d => d.IdCabinetNavigation).WithMany(p => p.Workers)
                .HasForeignKey(d => d.IdCabinet)
                .HasConstraintName("FK_Worker_Cabinet");

            entity.HasOne(d => d.IdHelperNavigation).WithMany(p => p.InverseIdHelperNavigation)
                .HasForeignKey(d => d.IdHelper)
                .HasConstraintName("FK_Worker_Worker1");

            entity.HasOne(d => d.IdLeadNavigation).WithMany(p => p.InverseIdLeadNavigation)
                .HasForeignKey(d => d.IdLead)
                .HasConstraintName("FK_Worker_Worker");

            entity.HasOne(d => d.IdPositionNavigation).WithMany(p => p.Workers)
                .HasForeignKey(d => d.IdPosition)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Worker_Position");

            entity.HasOne(d => d.IdPrivateInfoNavigation).WithMany(p => p.Workers)
                .HasForeignKey(d => d.IdPrivateInfo)
                .HasConstraintName("FK_Worker_WorkerPrivateInfo");
        });

        modelBuilder.Entity<WorkerPrivateInfo>(entity =>
        {
            entity.HasKey(e => e.IdInfo);

            entity.ToTable("WorkerPrivateInfo");

            entity.Property(e => e.PrivatePhoneNumber).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
