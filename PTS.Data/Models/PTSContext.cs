using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Web;

namespace PTS.Data.Models
{
    public partial class PTSContext : DbContext
    {
        public PTSContext()
        {
        }

        public PTSContext(DbContextOptions<PTSContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Person> People { get; set; } 
        public DbSet<Project> Projects { get; set; } 
        public DbSet<Status> Statuses { get; set; } 
        public DbSet<Subtask> Subtasks { get; set; } 
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Team> Teams { get; set; } 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=PTS;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.Address).HasMaxLength(200);

                entity.Property(e => e.Company).HasMaxLength(200);

                entity.Property(e => e.Country).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.TelephoneNo).HasMaxLength(50);

                entity.Property(e => e.Username).HasMaxLength(50);
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_tbl_Person");

                entity.ToTable("Person");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.TelephoneNo).HasMaxLength(50);

                entity.Property(e => e.Username).HasMaxLength(50);
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("Project");

                entity.Property(e => e.ProjectId)
                    .ValueGeneratedNever()
                    .HasColumnName("ProjectID");

                entity.Property(e => e.ActualEndDate)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ActualStartDate)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.AdministratorId).HasColumnName("AdministratorID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.ExpectedEndDate)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ExpectedStartDate)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.Administrator)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.AdministratorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_project_user");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_project_customer");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("Status");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Subtask>(entity =>
            {
                entity.ToTable("Subtask");

                entity.Property(e => e.SubtaskId).HasColumnName("SubtaskID");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.TaskId).HasColumnName("TaskID");

                entity.Property(e => e.TeamMemberId).HasColumnName("TeamMemberID");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Subtasks)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_subtask_status");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.Subtasks)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_subtask_task");

                entity.HasOne(d => d.TeamMember)
                    .WithMany(p => p.Subtasks)
                    .HasForeignKey(d => d.TeamMemberId)
                    .HasConstraintName("fk_subtask_person");
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.ToTable("Task");

                entity.Property(e => e.TaskId)
                    .ValueGeneratedNever()
                    .HasColumnName("TaskID");

                entity.Property(e => e.ActualDateCompleted)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ActualDateStarted)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ExpectedDateCompleted)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ExpectedDateStarted)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.ProjectId).HasColumnName("ProjectID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.TeamId).HasColumnName("TeamID");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_task_project");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_task_status");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_task_team");

                entity.HasMany(d => d.Predecessors)
                    .WithMany(p => p.Tasks)
                    .UsingEntity<Dictionary<string, object>>(
                        "Predecessor",
                        l => l.HasOne<Task>().WithMany().HasForeignKey("PredecessorId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("fk_predecessor_task"),
                        r => r.HasOne<Task>().WithMany().HasForeignKey("TaskId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("fk_predecessor"),
                        j =>
                        {
                            j.HasKey("TaskId", "PredecessorId").HasName("predecessor_pk");

                            j.ToTable("Predecessor");

                            j.IndexerProperty<Guid>("TaskId").HasColumnName("TaskID");

                            j.IndexerProperty<Guid>("PredecessorId").HasColumnName("PredecessorID");
                        });

                entity.HasMany(d => d.Tasks)
                    .WithMany(p => p.Predecessors)
                    .UsingEntity<Dictionary<string, object>>(
                        "Predecessor",
                        l => l.HasOne<Task>().WithMany().HasForeignKey("TaskId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("fk_predecessor"),
                        r => r.HasOne<Task>().WithMany().HasForeignKey("PredecessorId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("fk_predecessor_task"),
                        j =>
                        {
                            j.HasKey("TaskId", "PredecessorId").HasName("predecessor_pk");

                            j.ToTable("Predecessor");

                            j.IndexerProperty<Guid>("TaskId").HasColumnName("TaskID");

                            j.IndexerProperty<Guid>("PredecessorId").HasColumnName("PredecessorID");
                        });
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.ToTable("Team");

                entity.Property(e => e.TeamId).HasColumnName("TeamID");

                entity.Property(e => e.Location).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.TeamLeaderId).HasColumnName("TeamLeaderID");

                entity.HasOne(d => d.TeamLeader)
                    .WithMany(p => p.Teams)
                    .HasForeignKey(d => d.TeamLeaderId)
                    .HasConstraintName("fk_team_person");

                entity.HasMany(d => d.Users)
                    .WithMany(p => p.TeamsNavigation)
                    .UsingEntity<Dictionary<string, object>>(
                        "TeamMember",
                        l => l.HasOne<Person>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("fk_teammember_person"),
                        r => r.HasOne<Team>().WithMany().HasForeignKey("TeamId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("fk_teammember_team"),
                        j =>
                        {
                            j.HasKey("TeamId", "UserId").HasName("Teammember_pk");

                            j.ToTable("TeamMember");

                            j.IndexerProperty<int>("TeamId").HasColumnName("TeamID");

                            j.IndexerProperty<int>("UserId").HasColumnName("UserID");
                        });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
