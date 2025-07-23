using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace Project_Prn.Models;

public partial class Prngroup4Context : DbContext
{
    public Prngroup4Context()
    {
    }

    public Prngroup4Context(DbContextOptions<Prngroup4Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Attendance> Attendances { get; set; }

    public virtual DbSet<Certificate> Certificates { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Exam> Exams { get; set; }

    public virtual DbSet<ExamResult> ExamResults { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Registration> Registrations { get; set; }

    public virtual DbSet<Result> Results { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory());
        builder.AddJsonFile("appsettings.json", true, true);
        var configuration = builder.Build();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("Default"));
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.HasKey(e => e.AttendanceId).HasName("PK__Attendan__8B69263CB8065B4E");

            entity.ToTable("Attendance");

            entity.HasIndex(e => new { e.CourseId, e.UserId, e.SessionDate }, "UC_Attendance").IsUnique();

            entity.Property(e => e.AttendanceId).HasColumnName("AttendanceID");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Course).WithMany(p => p.Attendances)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Attendanc__Cours__619B8048");

            entity.HasOne(d => d.User).WithMany(p => p.Attendances)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Attendanc__UserI__628FA481");
        });

        modelBuilder.Entity<Certificate>(entity =>
        {
            entity.HasKey(e => e.CertificateId).HasName("PK__Certific__BBF8A7E14D0F1BD2");

            entity.HasIndex(e => e.CertificateCode, "UQ__Certific__9B85583008075203").IsUnique();

            entity.Property(e => e.CertificateId).HasColumnName("CertificateID");
            entity.Property(e => e.CertificateCode).HasMaxLength(50);
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");

            entity.HasOne(d => d.User).WithMany(p => p.Certificates)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Certifica__UserI__5DCAEF64");

            entity.HasOne(d => d.Course).WithMany(p => p.Certificates) 
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Certifica__Cours__CUSTOM");
        });



        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK__Courses__C92D71872A99643B");

            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.CourseName).HasMaxLength(100);
            entity.Property(e => e.TeacherId).HasColumnName("TeacherID");

            entity.HasOne(d => d.Teacher).WithMany(p => p.Courses)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Courses__Teacher__4D94879B");
        });

        modelBuilder.Entity<Exam>(entity =>
        {
            entity.HasKey(e => e.ExamId).HasName("PK__Exams__297521A78CED8A70");

            entity.Property(e => e.ExamId).HasColumnName("ExamID");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.ExamDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Room).HasMaxLength(50);

            entity.HasOne(d => d.Course).WithMany(p => p.Exams)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Exams__CourseID__5629CD9C");

            entity.HasOne(d => d.Supervisor).WithMany(p => p.Exams)
                .HasForeignKey(d => d.SupervisorId)
                .HasConstraintName("FK_Exams_Supervisor");
        });

        modelBuilder.Entity<ExamResult>(entity =>
        {
            entity.HasKey(e => e.ResultId).HasName("PK__ExamResu__976902086329666B");

            entity.ToTable("ExamResult");

            entity.Property(e => e.SubmittedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Exam).WithMany(p => p.ExamResults)
                .HasForeignKey(d => d.ExamId)
                .HasConstraintName("FK__ExamResul__ExamI__70DDC3D8");

            entity.HasOne(d => d.User).WithMany(p => p.ExamResults)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__ExamResul__UserI__6FE99F9F");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__20CF2E1216B0E59A");

            entity.ToTable("Notification");

            entity.HasOne(d => d.Registration).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.RegistrationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Notificat__Regis__66603565");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Notificat__UserI__656C112C");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("PK__Question__0DC06FACFC39ADDC");

            entity.ToTable("Questions");

            entity.Property(e => e.Content).HasMaxLength(1000);
            entity.Property(e => e.CorrectAnswer)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.OptionA).HasMaxLength(255);
            entity.Property(e => e.OptionB).HasMaxLength(255);
            entity.Property(e => e.OptionC).HasMaxLength(255);
            entity.Property(e => e.OptionD).HasMaxLength(255);

            entity.HasOne(d => d.Exam).WithMany(p => p.Questions)
                .HasForeignKey(d => d.ExamId)
                .HasConstraintName("FK__Question__ExamId__6D0D32F4");
        });

        modelBuilder.Entity<Registration>(entity =>
        {
            entity.HasKey(e => e.RegistrationId).HasName("PK__Registra__6EF58830F4E77C93");

            entity.Property(e => e.RegistrationId).HasColumnName("RegistrationID");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Pending");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Course).WithMany(p => p.Registrations)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Registrat__Cours__534D60F1");

            entity.HasOne(d => d.User).WithMany(p => p.Registrations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Registrat__UserI__52593CB8");
        });

        modelBuilder.Entity<Result>(entity =>
        {
            entity.HasKey(e => e.ResultId).HasName("PK__Results__9769022828E84472");

            entity.Property(e => e.ResultId).HasColumnName("ResultID");
            entity.Property(e => e.ExamId).HasColumnName("ExamID");
            entity.Property(e => e.SubmittedAt).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Exam).WithMany(p => p.Results)
                .HasForeignKey(d => d.ExamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Results__ExamID__59063A47");

            entity.HasOne(d => d.User).WithMany(p => p.Results)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Results__UserID__59FA5E80");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC7D51DA9B");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534C0676314").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Class).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.FullName).HasMaxLength(150);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(15);
            entity.Property(e => e.Role).HasMaxLength(20);
            entity.Property(e => e.School).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
