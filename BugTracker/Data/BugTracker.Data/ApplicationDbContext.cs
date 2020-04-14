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

        public DbSet<UserNews> UsersNews { get; set; }

        public DbSet<CompanyUser> CompaniesUsers { get; set; }

        public DbSet<JoinRequest> JoinsRequests { get; set; }

        public DbSet<AssignmentUser> AssignmentsUsers { get; set; }

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
                .OnDelete(DeleteBehavior.Cascade);

                pu.HasOne(x => x.User)
                .WithMany(x => x.Projects)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<UserNews>(pu =>
            {
                pu.HasKey(x => new { x.NewsId, x.UserId });

                pu.HasOne(x => x.News)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.NewsId)
                .OnDelete(DeleteBehavior.Cascade);

                pu.HasOne(x => x.User)
                .WithMany(x => x.News)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<CompanyUser>(cu =>
            {
                cu.HasKey(x => new { x.CompanyId, x.UserId });

                cu.HasOne(x => x.Company)
                .WithMany(x => x.Employees)
                .HasForeignKey(x => x.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);

                cu.HasOne(x => x.User)
                .WithMany(x => x.Companies)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<AssignmentUser>(au =>
            {
                au.HasKey(x => new { x.UserId, x.AssignmentId });

                au.HasOne(x => x.Assignment)
                .WithMany(x => x.Assignees)
                .HasForeignKey(x => x.AssignmentId)
                .OnDelete(DeleteBehavior.Cascade);

                au.HasOne(x => x.User)
                .WithMany(x => x.Assignments)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
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
