using Firebase.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.Diagnostics;
using WorkManagementWeb.Models;
using Firebase.Database;
using FireSharp.Interfaces;
using FireSharp.Config;
using FireSharp.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WorkManagementWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DbContexts _dbContext;

        // Firebase yapılandırma bilgileri
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "pKqeO8p410N7jIb1k5WIiCKJovItF6divZyEFQe0\r\n",
            BasePath = "https://workmanagement-8c1ad-default-rtdb.europe-west1.firebasedatabase.app/"
        };
        IFirebaseClient client;

        // HomeController'ın constructor'ı
        public HomeController(ILogger<HomeController> logger, DbContexts dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        // Ana sayfaya yönlendirme işlemi
        public IActionResult Index()
        {
            return RedirectToAction("WorkList");
        }

        // Joblist oluşturma sayfasına yönlendirme işlemi
        [HttpGet]
        public IActionResult JobCreate()
        {
            return View();
        }

        // Joblist oluşturma işlemini gerçekleştiren HTTP Post metodu
        [HttpPost]
        public async Task<IActionResult> JobCreateAsync(JoblistModels joblistModels)
        {
            // Yeni bir JoblistModels örneği oluşturma
            JoblistModels joblistModels1 = new JoblistModels();
            joblistModels1.JobListName = joblistModels.JobListName;
            joblistModels1.Time = DateTime.Now;

            // Firebase Client oluşturma
            client = new FireSharp.FirebaseClient(config);
            var data = joblistModels1;

            // Firebase'e yeni veri ekleme
            PushResponse response = client.Push("Joblist/", data);
            data.JobListName = response.Result.name;
            SetResponse setResponse = client.Set("/" + data.ID, data);

            /*  
            // Eğer Entity Framework kullanılarak SQL Server'a veri eklemek isteniyorsa bu bloğu kullanabilirsiniz
            _dbContext.JoblistModels.Add(joblistModels1);
            _dbContext.SaveChanges();
            */

            return RedirectToAction("WorkList");
        }

        // Joblist listeleme sayfasına yönlendirme işlemi
        [HttpGet]
        public async Task<IActionResult> WorkListAsync()
        {
            // Firebase Client oluşturma
            client = new FireSharp.FirebaseClient(config);

            // Firebase Realtime Database'den veri çekme
            FirebaseResponse response = client.Get("Joblist");
            dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);

            // Veriyi işleme
            var list = new List<JoblistModels>();
            if (data != null)
            {
                foreach (var item in data)
                {
                    list.Add(JsonConvert.DeserializeObject<JoblistModels>(((JProperty)item).Value.ToString()));
                }
                return View(list);
            }
            else
            {
                return View(null);
            }
        }

        // İş sonucu sayfasını gösterme işlemi
        [HttpGet]
        public IActionResult Sonuc(int id)
        {
            ViewBag.Mesaj = $"İş Listesi oluşturuldu: {id}";
            return View();
        }

        // Diğer sayfalara yönlendirme işlemleri
        public IActionResult Day()
        {
            return View();
        }

        public IActionResult Planned()
        {
            return View();
        }

        public IActionResult Completed_Tasks()
        {
            return View();
        }

        public IActionResult Tasks()
        {
            return View();
        }

        // Hata sayfasını gösterme işlemi
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
