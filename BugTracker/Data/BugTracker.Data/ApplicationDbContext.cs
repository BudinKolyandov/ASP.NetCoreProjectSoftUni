namespace BugTracker.Data
{
    using BugTracker.Data.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> AspNetUsers { get; set; }

        public DbSet<Bug> Bugs { get; set; }

        public DbSet<BugHistory> BugsHistories { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<News> News { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<ProjectUser> ProjectsUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
            base.OnModelCreating(modelBuilder);
        }
    }
}
