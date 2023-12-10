using Microsoft.EntityFrameworkCore;

namespace WorkManagementWeb.Models
{
    public class DbContexts:DbContext
    {

        public DbContexts(DbContextOptions<DbContexts> options) : base(options)
        {
        }


        public DbSet<JoblistModels> JoblistModels { get; set; }
        public DbSet<TaskListModels> TaskListModels { get; set; }
       
        public DbSet<User> User { get; set; }
        public DbSet<Team> Team { get; set; }
        public DbSet<TeamJoblist> TeamJoblists { get; set; }
        public DbSet<TeamTaskName> TeamTaskNames { get; set; }
        public DbSet<TeamMembers> TeamMembers { get; set; }




    }
}
