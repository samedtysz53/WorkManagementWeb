using System.ComponentModel.DataAnnotations;

namespace WorkManagementWeb.Models
{
    public class TeamTaskName
    {
        [Key]
        public int T_TID { get; set; }
        public string TTaskName { get; set; }
        public DateTime Time { get; set; }
        public string Added_by { get; set; }

        public string? finisher { get; set; }
        public DateTime? EndTime { get; set; }
    }
}
