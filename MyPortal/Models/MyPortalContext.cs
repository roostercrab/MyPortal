using MyPortal.Models;

namespace MyPortal
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MyPortalContext : DbContext
    {
        public MyPortalContext()
            : base("name=MyPortalContext")
        {
        }

        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<LogType> LogTypes { get; set; }
        public virtual DbSet<RegGroup> RegGroups { get; set; }
        public virtual DbSet<Result> Results { get; set; }
        public virtual DbSet<ResultSet> ResultSets { get; set; }
        public virtual DbSet<Staff> Staffs { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<TrainingCertificate> TrainingCerts { get; set; }
        public virtual DbSet<TrainingCours> TrainingCourses { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Log>()
                .Property(e => e.Author)
                .IsUnicode(false);

            modelBuilder.Entity<Log>()
                .Property(e => e.Message)
                .IsUnicode(false);

            modelBuilder.Entity<LogType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<LogType>()
                .HasMany(e => e.Logs)
                .WithRequired(e => e.LogType)
                .HasForeignKey(e => e.Type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RegGroup>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<RegGroup>()
                .Property(e => e.Tutor)
                .IsUnicode(false);

            modelBuilder.Entity<ResultSet>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<ResultSet>()
                .HasMany(e => e.Results)
                .WithRequired(e => e.ResultSet1)
                .HasForeignKey(e => e.ResultSet)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Staff>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<Staff>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Staff>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Staff>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Staff>()
                .HasMany(e => e.Logs)
                .WithRequired(e => e.Staff)
                .HasForeignKey(e => e.Author)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Staff>()
                .HasMany(e => e.RegGroups)
                .WithRequired(e => e.Staff)
                .HasForeignKey(e => e.Tutor)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Staff>()
                .HasMany(e => e.Subjects)
                .WithRequired(e => e.Staff)
                .HasForeignKey(e => e.Leader)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Staff>()
                .HasMany(e => e.TrainingCerts)
                .WithRequired(e => e.Staff1)
                .HasForeignKey(e => e.Staff)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.RegGroup)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.YearGroup)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.AccountBalance)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.Logs)
                .WithRequired(e => e.Student1)
                .HasForeignKey(e => e.Student)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.Results)
                .WithRequired(e => e.Student1)
                .HasForeignKey(e => e.Student)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Subject>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Subject>()
                .Property(e => e.Leader)
                .IsUnicode(false);

            modelBuilder.Entity<Subject>()
                .HasMany(e => e.Results)
                .WithRequired(e => e.Subject1)
                .HasForeignKey(e => e.Subject)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TrainingCertificate>()
                .Property(e => e.Staff)
                .IsUnicode(false);

            modelBuilder.Entity<TrainingCertificate>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<TrainingCours>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<TrainingCours>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.PasswordHash)
                .IsUnicode(false);
        }
    }
}
