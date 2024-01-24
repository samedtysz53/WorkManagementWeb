using System.ComponentModel.DataAnnotations;

namespace WorkManagementWeb.Models
{
    public class User
    {
        [Key]
        public int KullaniciID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Roles { get; set; }
    }

    public class Musteri
    {

        [Key]
        public int MusteriID { get; set; }
        public string MusteriAdi { get; set; }
        public string IletisimBilgileri { get; set; }
        public string DigerIlgiliBilgiler { get; set; }
    }

    // İş Emirleri Tablosu
    public class IsEmri
    {

        [Key]
        public int IsEmriID { get; set; }
        public int MusteriID { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public string Durum { get; set; } // (Yeni, Devam Ediyor, Tamamlandı)
        public DateTime OlusturmaTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
        public string DigerIlgiliBilgiler { get; set; }
    }

    // Görevler Tablosu
    public class Gorev
    {

        [Key]
        public int GorevID { get; set; }
        public int IsEmriID { get; set; }
        public int CalisanID { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public string Durum { get; set; } // (Başlamış, Tamamlanmış, İptal Edilmiş)
        public DateTime BaslamaTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
        public string DigerIlgiliBilgiler { get; set; }
    }

    // Zaman Takibi Tablosu
    public class ZamanTakibi
    {

        [Key]
        public int ZamanTakibiID { get; set; }
        public int GorevID { get; set; }
        public int CalisanID { get; set; }
        public DateTime BaslangicZamani { get; set; }
        public DateTime BitisZamani { get; set; }
        public int ToplamCalismaSuresi { get; set; }
    }

    // Malzeme ve Stok Tablosu
    public class MalzemeVeStok
    {

        [Key]
        public int MalzemeID { get; set; }
        public int IsEmriID { get; set; }
        public string MalzemeAdi { get; set; }
        public int Miktar { get; set; }
        public decimal BirimFiyati { get; set; }
        public decimal ToplamMaliyet { get; set; }
    }
}
