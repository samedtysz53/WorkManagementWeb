
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
                HttpContext.Session.SetString("UserCode",filter.RandomCode);
                return RedirectToAction("SelectTeam", "Main");
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
            string uniqueCode;
            do
            {
                uniqueCode = GenerateRandomCode();
            } while (IsCodeExists(uniqueCode));

            user.RandomCode = "#"+uniqueCode;

            dbContexts.User.Add(user);
            dbContexts.SaveChanges();

            return RedirectToAction("worklist", "Main");
        }
        private string GenerateRandomCode()
        {
            Random random = new Random();
            int code = random.Next(0, 10000); // 1000 ile 9999 arasında random sayı üret
            return code.ToString();
        }
        private bool IsCodeExists(string code)
        {
            return dbContexts.User.Any(u => u.RandomCode == code);
        }
    }
}
