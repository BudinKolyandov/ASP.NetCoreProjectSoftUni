namespace BugTracker.Data
{
    using System.Linq;

    using BugTracker.Data.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : IdentityDbContext<User, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> AspNetUsers { get; set; }

        public DbSet<ApplicationRole> AspNetRoles { get; set; }

        public DbSet<Bug> Bugs { get; set; }

        public DbSet<BugHistory> BugsHistories { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<News> News { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Assignment> Assignments { get; set; }

        public DbSet<ProjectUser> ProjectsUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureUserIdentityRelations(modelBuilder);

            var entityTypes = modelBuilder.Model.GetEntityTypes().ToList();

            modelBuilder.Entity<ProjectUser>(pu =>
            {
                pu.HasKey(x => new { x.ProjectId, x.UserId });

                pu.HasOne(x => x.Project)
                .WithMany(x => x.Developers)
                .HasForeignKey(x => x.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);

                pu.HasOne(x => x.User)
                .WithMany(x => x.Projects)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Assignment>(a =>
            {
                a.HasOne(x => x.Assignee)
                .WithMany(x => x.Assignments)
                .HasForeignKey(x => x.AssigneeId)
                .OnDelete(DeleteBehavior.Restrict);
            });
        }

        private static void ConfigureUserIdentityRelations(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(e => e.Claims)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Logins)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Roles)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
