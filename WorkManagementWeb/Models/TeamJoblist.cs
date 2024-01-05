using System.ComponentModel.DataAnnotations;

namespace WorkManagementWeb.Models
{
    public class TeamJoblist
    {
        [Key]
        public int T_JID { get; set; }

        public string TeamJobName { get; set; }
        public string TeamCode { get; set; }
        public DateTime Time { get; set; }
     
    }
}
