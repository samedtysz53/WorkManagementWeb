
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using WorkManagementWeb.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WorkManagementWeb.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;



        // Firebase yapılandırma bilgileri

        private readonly DbContexts dbContexts;
        // HomeController'ın constructor'ı
        public HomeController(ILogger<HomeController> logger, DbContexts dbContexts)
        {
            this.dbContexts = dbContexts;
            _logger = logger;

        }

        [HttpGet]
        // Ana sayfaya yönlendirme işlemi
        public IActionResult Index()
        {
            //return RedirectToAction("WorkList","Main");
            return View();
        }
        [HttpPost]
        // Ana sayfaya yönlendirme işlemi
        public IActionResult Index(User user)
        {
            //return RedirectToAction("WorkList","Main");
            var filter = dbContexts.Users.FirstOrDefault(x => x.Email == user.Email && x.Password == user.Password);
            if (filter != null) 
            {
               
                HttpContext.Session.SetInt32("ID",user.KullaniciID);
              
                return RedirectToAction("Gorev", "Home");
            }
            return View();
        }
        public IActionResult Gorev() 
        {
            return View();
        }
        [HttpGet]
        public IActionResult register() 
        {
            return View();
        }
      
    }
}
