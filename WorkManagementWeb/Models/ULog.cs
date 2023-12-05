using System.ComponentModel.DataAnnotations;

namespace WorkManagementWeb.Models
{
    public class Log
    {
        [Key]
        public int L_ID { get; set; }
        public string Email { get; set; }
       
        public DateTime Time { get; set; }
    }
}
