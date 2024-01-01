# İş Takip Uygulaması

Bu proje, C# ASP.NET MVC Core ve .NET 7 kullanılarak geliştirilmiş basit bir iş takip uygulamasını içerir. Bu uygulama, işleri takip etmek, görevleri oluşturmak ve yönetmek için kullanılabilir.

## Başlangıç

Bu talimatlar, projeyi yerel bilgisayarınızda çalıştırmak ve geliştirmek için size yardımcı olacaktır. Projeyi başlatmadan önce, bilgisayarınızda .NET 7 SDK'sının yüklü olduğundan emin olun.

### Gereksinimler

- [.NET 7 SDK](https://dotnet.microsoft.com/download)
  


### Kurulum

1. Bu depoyu yerel bilgisayarınıza klonlayın:

    ```bash
    git clone https://github.com/kullanici/adini-buraya-yaz
    ```

2. Projeyi Visual Studio İle Açın

3.appsettings.json dosyası içindeki

 ```bash
 "ConnectionStrings": {
  "DefaultConnection": "Server=Database_Name;Database=WorkManagement;Encrypt=True;TrustServerCertificate=True;Trusted_Connection=True;MultipleActiveResultSets=true",
  ```
  alanına kendi veri tabanı sisteminizin adını giriniz 

4.Uygulamayı Başlatmadan önce Package Manager Console üzerinden
 ```bash 
 Database-Update
 ```
komutunu çalıştırın

5. Uygulamayı başlatın:

    ```bash
    dotnet run
    ```

6. Tarayıcınızda `https://localhost:5001` adresine giderek uygulamayı görüntüleyin.

## Kullanım

- Uygulama başlatıldığında, ana sayfada giriş ekranı göreceksiniz hesap oluşturup giriş yaptıktan sonra 
- "iş listesi oluştur " butonuyla yeni bir iş listesi oluşturabilirsiniz daha sonra iş listesi altında görevler oluşturabilirsiniz.
- Görevleri düzenlemek veya silmek için ilgili işlemleri seçeneklerden gerçekleştirebilirsiniz.

## Katkıda Bulunma

1. Bu depoyu fork edin.
2. Yeni bir özellik dalı (`git checkout -b feature/YeniOzellik`) oluşturun.
3. Değişikliklerinizi yapın ve deponuza ekleyin (`git add .`).
4. Yaptığınız değişiklikleri commit edin (`git commit -m 'Yeni özellik: YeniOzellik'`).
5. Dalınızı ana depoya itin (`git push origin feature/YeniOzellik`).
6. Bir pull isteği oluşturun.

## Lisans

Bu proje MIT lisansı altında lisanslanmıştır - Detaylar için [LICENSE](LICENSE) dosyasına bakın.
