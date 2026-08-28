using GiftOfTheGivers.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Runtime.Remoting.Contexts;
namespace GiftOfTheGivers.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Volunteer> Volunteers => Set<Volunteer>();
        public DbSet<ReliefProject> ReliefProjects => Set<ReliefProject>();
        public DbSet<DonationSchedule> DonationSchedules => Set<DonationSchedule>();
        public DbSet<Donation> Donations => Set<Donation>();
        public DbSet<TaxCertificate> TaxCertificates => Set<TaxCertificate>();
        public DbSet<EmergencyUpdate> EmergencyUpdates => Set<EmergencyUpdate>();
        public DbSet<VolunteerSignup> VolunteerSignups => Set<VolunteerSignup>();
        public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ---------------- Roles ---------------
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(r => r.RoleId);
                entity.Property(r => r.RoleName).IsRequired().HasMaxLength(50);
                entity.HasIndex(r => r.RoleName).IsUnique();
            });
            // ---------------- Users ---------------
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.UserId);
                entity.Property(u => u.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(u => u.LastName).IsRequired().HasMaxLength(100);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(255);
                entity.HasIndex(u => u.Email).IsUnique();
                entity.Property(u => u.PasswordHash).IsRequired().HasMaxLength(255);
                entity.Property(u => u.PhoneNumber).HasMaxLength(20);
                entity.Property(u => u.CreatedAt).HasDefaultValueSql("SYSUTCDATETIME()");
                entity.Property(u => u.IsActive).HasDefaultValue(true);
                entity.HasOne(u => u.Role)
                      .WithMany(r => r.Users)
                      .HasForeignKey(u => u.RoleId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
            // ---------------- Volunteers (1:1 with User) ---------------
            modelBuilder.Entity<Volunteer>(entity =>
            {
                entity.HasKey(v => v.VolunteerId);
                entity.Property(v => v.Skills).HasMaxLength(500);
                entity.Property(v => v.Availability).HasMaxLength(255);
                entity.Property(v => v.BackgroundCheckStatus).IsRequired().HasMaxLength(50).HasDefaultValue("Pending");
                entity.Property(v => v.RegisteredAt).HasDefaultValueSql("SYSUTCDATETIME()");
                entity.HasIndex(v => v.UserId).IsUnique(); // enforces the 1:1
                entity.HasOne(v => v.User)
                      .WithOne(u => u.Volunteer)
                      .HasForeignKey<Volunteer>(v => v.UserId)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.ToTable(t => t.HasCheckConstraint(
                    "CK_Volunteers_BackgroundCheckStatus",
                    "BackgroundCheckStatus IN ('Pending','Cleared','Rejected')"));
            });
            // ---------------- ReliefProjects ---------------
            modelBuilder.Entity<ReliefProject>(entity =>
            {
                entity.HasKey(p => p.ProjectId);
                entity.Property(p => p.ProjectName).IsRequired().HasMaxLength(200);
                entity.Property(p => p.Description).HasMaxLength(1000);
                entity.Property(p => p.Location).HasMaxLength(200);
                entity.Property(p => p.Status).IsRequired().HasMaxLength(50).HasDefaultValue("Planned");
                entity.HasOne(p => p.CreatedByUser)
                      .WithMany(u => u.ProjectsCreated)
                      .HasForeignKey(p => p.CreatedByUserId)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.ToTable(t =>
                {
                    t.HasCheckConstraint("CK_Projects_Status",
                        "Status IN ('Planned','Active','Completed','Cancelled')");
                    t.HasCheckConstraint("CK_Projects_Dates",
            });
            });
            "EndDate IS NULL OR EndDate >= StartDate");
            // ---------------- DonationSchedules ---------------
            modelBuilder.Entity<DonationSchedule>(entity =>
            {
                entity.HasKey(s => s.ScheduleId);
                entity.Property(s => s.Frequency).IsRequired().HasMaxLength(20);
                entity.Property(s => s.Status).IsRequired().HasMaxLength(20).HasDefaultValue("Active");
                entity.HasOne(s => s.User)
                      .WithMany(u => u.DonationSchedules)
                      .HasForeignKey(s => s.UserId)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.ToTable(t =>
                {
                    t.HasCheckConstraint("CK_Schedules_Frequency",
                        "Frequency IN ('Weekly','Monthly','Quarterly')");
                    t.HasCheckConstraint("CK_Schedules_Status",
                        "Status IN ('Active','Paused','Cancelled')");
                });
            });
            // ---------------- Donations ---------------
            modelBuilder.Entity<Donation>(entity =>
            {
                entity.HasKey(d => d.DonationId);
                entity.Property(d => d.Amount).HasColumnType("decimal(18,2)");
                entity.Property(d => d.Currency).IsRequired().HasMaxLength(3).IsFixedLength();
                entity.Property(d => d.DonationType).IsRequired().HasMaxLength(20);
                entity.Property(d => d.PaymentStatus).IsRequired().HasMaxLength(20).HasDefaultValue("Pending");
                entity.Property(d => d.TransactionDate).HasDefaultValueSql("SYSUTCDATETIME()");
                entity.HasOne(d => d.User)
                      .WithMany(u => u.Donations)
                      .HasForeignKey(d => d.UserId)
                      .IsRequired(false)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(d => d.Schedule)
                      .WithMany(s => s.Donations)
                      .HasForeignKey(d => d.ScheduleId)
                      .IsRequired(false)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(d => d.Project)
                      .WithMany(p => p.Donations)
                      .HasForeignKey(d => d.ProjectId)
                      .IsRequired(false)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasIndex(d => d.UserId).HasDatabaseName("IX_Donations_UserId");
                entity.HasIndex(d => d.ProjectId).HasDatabaseName("IX_Donations_ProjectId");
                entity.ToTable(t =>
                {
                    t.HasCheckConstraint("CK_Donations_Amount", "Amount > 0");
                    t.HasCheckConstraint("CK_Donations_Currency", "Currency IN ('ZAR','USD','EUR')");
                    t.HasCheckConstraint("CK_Donations_Type", "DonationType IN ('OnceOff','Recurring')");
                    t.HasCheckConstraint("CK_Donations_PaymentStatus",
                        "PaymentStatus IN ('Pending','Completed','Failed','Refunded')");
                    t.HasCheckConstraint("CK_Donations_Anonymous",
                        "(IsAnonymous = 1 AND UserId IS NULL) OR (IsAnonymous = 0)");
                });
            });
            // ---------------- TaxCertificates (1:1 with Donation) ---------------
            modelBuilder.Entity<TaxCertificate>(entity =>
            {
                entity.HasKey(c => c.CertificateId);
                entity.Property(c => c.CertificateNumber).IsRequired().HasMaxLength(50);
                entity.Property(c => c.Amount).HasColumnType("decimal(18,2)");
                entity.Property(c => c.IssueDate).HasDefaultValueSql("CAST(SYSUTCDATETIME() AS DATE)");
                entity.HasIndex(c => c.DonationId).IsUnique();
                entity.HasIndex(c => c.CertificateNumber).IsUnique();
                entity.HasOne(c => c.Donation)
                      .WithOne(d => d.TaxCertificate)
                      .HasForeignKey<TaxCertificate>(c => c.DonationId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
            // ---------------- EmergencyUpdates ---------------
            modelBuilder.Entity<EmergencyUpdate>(entity =>
            {
                entity.HasKey(e => e.UpdateId);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Content).IsRequired().HasMaxLength(2000);
                entity.Property(e => e.Severity).IsRequired().HasMaxLength(20).HasDefaultValue("Medium");
                entity.Property(e => e.PostedAt).HasDefaultValueSql("SYSUTCDATETIME()");
                entity.HasOne(e => e.Project)
                      .WithMany(p => p.EmergencyUpdates)
                      .HasForeignKey(e => e.ProjectId)
                      .IsRequired(false)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.PostedByUser)
                      .WithMany(u => u.EmergencyUpdatesPosted)
                      .HasForeignKey(e => e.PostedByUserId)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.ToTable(t => t.HasCheckConstraint(
                    "CK_Updates_Severity", "Severity IN ('Low','Medium','High','Critical')"));
            });
            // ---------------- VolunteerSignups (junction) ---------------
            modelBuilder.Entity<VolunteerSignup>(entity =>
            {
                entity.HasKey(s => s.SignupId);
                entity.Property(s => s.Status).IsRequired().HasMaxLength(20).HasDefaultValue("Pending");
                entity.Property(s => s.SignupDate).HasDefaultValueSql("CAST(SYSUTCDATETIME() AS DATE)");
                entity.HasOne(s => s.Volunteer)
                      .WithMany(v => v.VolunteerSignups)
                      .HasForeignKey(s => s.VolunteerId)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(s => s.Project)
                      .WithMany(p => p.VolunteerSignups)
                      .HasForeignKey(s => s.ProjectId)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasIndex(s => s.ProjectId).HasDatabaseName("IX_VolunteerSignups_ProjectId");
                entity.HasIndex(s => new { s.VolunteerId, s.ProjectId })
                      .IsUnique()
                      .HasDatabaseName("UQ_Signups_VolunteerProject");
                entity.ToTable(t => t.HasCheckConstraint(
                    "CK_Signups_Status", "Status IN ('Pending','Confirmed','Completed','Cancelled')"));
            });
            // ---------------- AuditLogs ---------------
            modelBuilder.Entity<AuditLog>(entity =>
            {
                entity.HasKey(l => l.LogId);
                entity.Property(l => l.Action).IsRequired().HasMaxLength(50);
                entity.Property(l => l.EntityName).IsRequired().HasMaxLength(100);
                entity.Property(l => l.Timestamp).HasDefaultValueSql("SYSUTCDATETIME()");
                entity.Property(l => l.Details).HasColumnType("nvarchar(max)");
                entity.HasOne(l => l.User)
                      .WithMany(u => u.AuditLogs)
                      .HasForeignKey(l => l.UserId)
                      .IsRequired(false)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasIndex(l => new { l.EntityName, l.EntityId })
                      .HasDatabaseName("IX_AuditLogs_EntityName_EntityId");
            });
        }
    }
}