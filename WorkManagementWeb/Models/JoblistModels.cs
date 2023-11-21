using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WorkManagementWeb.Models
{
    public class JoblistModels
    {
        [Key]
        public int ID { get; set; }
        public string JobListName { get; set; }
        public DateTime Time { get; set; }

    }
}
