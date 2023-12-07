using System.ComponentModel.DataAnnotations;

namespace WorkManagementWeb.Models
{
    public class TaskListModels
    {
        [Key]
        public int ID { get; set; }
        public string TaskName { get; set; }
        public DateTime Time { get; set; }
        public string JobListName { get; set; }
        public string Description { get; set; }
        public DateTime? EndTime { get; set; }
        public bool Done { get; set; }
    }
}
