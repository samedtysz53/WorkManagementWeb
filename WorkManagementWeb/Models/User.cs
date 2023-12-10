using System.ComponentModel.DataAnnotations;

namespace WorkManagementWeb.Models
{
    public class User
    {
        [Key]
        public int U_ID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Time { get; set; }

        public string RandomCode { get; set; }
      


    }
}
