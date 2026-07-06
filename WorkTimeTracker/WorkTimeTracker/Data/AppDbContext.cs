using Microsoft.EntityFrameworkCore;
using WorkTimeTracker.Models;

namespace WorkTimeTracker
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Project> Projects { get; set; }
        public DbSet<WorkTask> Tasks { get; set; }
        public DbSet<TimeLog> TimeLogs { get; set; }
    }
}