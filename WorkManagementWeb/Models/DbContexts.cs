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
        public DbSet<DeleteTaskList> DeleteTaskLists { get; set; }



    }
}
