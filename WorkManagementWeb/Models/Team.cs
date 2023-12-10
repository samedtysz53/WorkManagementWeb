using System.ComponentModel.DataAnnotations;

namespace WorkManagementWeb.Models
{
    public class Team
    {
        [Key]
        public int T_ID { get; set; }
        public string TeamName { get; set; }
        public string TCode  { get; set; }
      public string MemberCode { get; set; }

    }
}
