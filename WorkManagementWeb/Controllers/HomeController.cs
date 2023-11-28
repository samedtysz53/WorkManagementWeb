 
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.Diagnostics;
using WorkManagementWeb.Models;
 
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
        

        // HomeController'ın constructor'ı
        public HomeController(ILogger<HomeController> logger, DbContexts dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        // Ana sayfaya yönlendirme işlemi
        public IActionResult Index()
        {
            return RedirectToAction("WorkList","Main");
        }

        // Joblist oluşturma sayfasına yönlendirme işlemi
        [HttpGet]
        public IActionResult JobCreate()
        {
            return View();
        }

     
        [HttpPost]
        public async Task<IActionResult> JobCreateAsync(JoblistModels joblistModels)
        {
            

            return RedirectToAction("WorkList");
        }

        // Joblist listeleme sayfasına yönlendirme işlemi
         
       

        // İş sonucu sayfasını gösterme işlemi
        [HttpGet]
        public IActionResult Sonuc(int id)
        {
            ViewBag.Mesaj = $"İş Listesi oluşturuldu: {id}";
            return View();
        }
 

        // Diğer sayfalara yönlendirme işlemleri
 
        [HttpGet]
        public IActionResult Upgrade(int id) 
        {

            return View();
        }
        [HttpPost]
        public IActionResult Upgrade(int id,string Name)
        {

            return View();
        }


 
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
