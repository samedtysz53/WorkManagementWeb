using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.Diagnostics;
using WorkManagementWeb.Models;

namespace WorkManagementWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly DbContexts _dbContext;

        public HomeController(ILogger<HomeController> logger, DbContexts dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return RedirectToAction("WorkList");
        }
        [HttpGet]
        public IActionResult JobCreate() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult JobCreate(JoblistModels joblistModels)
        {

            JoblistModels joblistModels1 = new JoblistModels();
            joblistModels1.JobListName = joblistModels.JobListName;
            joblistModels1.Time = DateTime.Now;

             
            _dbContext.JoblistModels.Add(joblistModels1);
            _dbContext.SaveChanges();

            return RedirectToAction("WorkList");
        }

        [HttpGet]
        public IActionResult WorkList() 
        {
            return View(_dbContext.JoblistModels.ToList());
        }

        [HttpGet]
        public IActionResult Sonuc(int id) 
        {
            ViewBag.Mesaj = $"İş Listesi oluşturuldu: {id}";
            return View();
        }
        
        
       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}