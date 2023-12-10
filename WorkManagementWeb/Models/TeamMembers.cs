using System.ComponentModel.DataAnnotations;

namespace WorkManagementWeb.Models
{
    public class TeamMembers
    {
        [Key]
        public int ID { get; set; }
        public string TeamCode { get; set; }
        public string UserCode { get; set; }
    }
}
