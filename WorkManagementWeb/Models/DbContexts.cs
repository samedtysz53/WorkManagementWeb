using Microsoft.EntityFrameworkCore;

namespace WorkManagementWeb.Models
{
    public class DbContexts:DbContext
    {

        public DbContexts(DbContextOptions<DbContexts> options) : base(options)
        {
        }


        public DbSet<User> Users { get; set; }
        public DbSet<Musteri> Musteri { get; set; }
       
     
        public DbSet<IsEmri> TIsEmrieam { get; set; }
        public DbSet<Gorev> Gorev { get; set; }
        public DbSet<ZamanTakibi> ZamanTakibi { get; set; }
        public DbSet<MalzemeVeStok> MalzemeVeStok { get; set; }




    }
}
