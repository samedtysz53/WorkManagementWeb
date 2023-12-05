
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
            var filter = dbContexts.User.FirstOrDefault(x => x.Email == user.Email && x.Password == user.Password);
            if (filter != null) 
            {
                HttpContext.Session.SetString("Email",user.Email);
                return RedirectToAction("Worklist","Main");
            }
            return View();
        }
        [HttpGet]
        public IActionResult register() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult register(User user)
        {
            dbContexts.User.Add(user);
            dbContexts.SaveChanges();
            return View();
        }

    }
}
