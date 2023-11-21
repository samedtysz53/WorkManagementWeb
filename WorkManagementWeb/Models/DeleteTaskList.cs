using System.ComponentModel.DataAnnotations;

namespace WorkManagementWeb.Models
{
    public class DeleteTaskList
    {
        [Key]
        public int ID { get; set; }
        public string DeleteTaskName { get; set; }
        public string Time { get; set; }
        public string JobListName { get; set; }

    }
}
