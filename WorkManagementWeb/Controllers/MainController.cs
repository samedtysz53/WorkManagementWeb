using Microsoft.AspNetCore.Mvc;
using WorkManagementWeb.Models;

namespace WorkManagementWeb.Controllers
{
    public class MainController : Controller
    {
        FirebaseController firebaseController;
        public MainController(ILogger<HomeController> logger) 
        {
        firebaseController=new FirebaseController();
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Sonuc() 
        {
            return View();
        }
        public IActionResult worklist() 
        {
            var list = firebaseController.GetJoblist();
            return View(list);
        }
        [HttpGet]
        public IActionResult JobCreate() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult JobCreate(JoblistModels joblistModels) 
        {
            firebaseController.CreateJobList(joblistModels);
            return View(); 
        }

    }
}
