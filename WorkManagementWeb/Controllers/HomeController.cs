
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
                HttpContext.Session.SetString("Eposta", user.Email);
                var filter1= dbContexts.Users.Where(x=>x.Email==user.Email).FirstOrDefault();
                HttpContext.Session.SetString("Unit", filter1.unit);
              
                return RedirectToAction("Gorev", "Home");
            }
            return View();
        }
        public IActionResult Gorev() 
        {
            ViewBag.User = HttpContext.Session.GetString("Eposta");
            ViewBag.Unit = HttpContext.Session.GetString("Unit");
            var filter = dbContexts.Gorev.ToList();
          
            //kullanıcının birimine göre veri çeksin
            return View(filter);
        }
        public IActionResult GorevAdd()
        {
            ViewBag.User = HttpContext.Session.GetString("Eposta");
            ViewBag.Unit = HttpContext.Session.GetString("Unit");
 

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
            dbContexts.Users.Add(user);
            dbContexts.SaveChanges();
            HttpContext.Session.SetString("Eposta",user.Email);

            RedirectToAction("Gorev", "Home");
            return View();
        }
        public IActionResult UserList() 
        {
            if (CheckRole()==0)
            {

               
               var list = dbContexts.Users.ToList();
                return View(list);
            
            }else if (CheckRole()==1) 
            {
                var filter2 = dbContexts.Users.Where(x => x.unit == HttpContext.Session.GetString("Unit")).ToList();
                return View(filter2);
            }
            else if (CheckRole() == 2) 
            {
                ViewBag.Yetki = "Personel";
                var filter2 = dbContexts.Users.Where(x => x.unit == HttpContext.Session.GetString("Unit")).ToList();
                return View(filter2);
            }
            else
            {
                ViewBag.Per = "Bu Sayfayı Görüntülemeye Yetkiniz Yok";
                return View();
            }
           
        }
        [HttpGet]
        public IActionResult UserAdd() 
        {
            if (CheckRole() == 0|| CheckRole()==1)
            {
                return View();
            }
            else
            {
                ViewBag.Per = "Bu Sayfayı Görüntülemeye Yetkiniz Yok !!";
            }
            return View();
        }
        [HttpPost]
        public IActionResult UserAdd(User user)
        {
          
                var query=dbContexts.Users.Where(x => x.Email==user.Email).FirstOrDefault();
            if (query == null) 
            {
                dbContexts.Users.Add(user);
                dbContexts.SaveChanges();
                RedirectToAction("UserList", "Home");
            }
            else 
            {
                ViewBag.Error = "Bu Mail Adresi Kullanımda";
            }
            
           
            return View();
        }

        public IActionResult Customer()
        {
          
            if(CheckRole()==0)
            {
                
                return View(dbContexts.Musteri.ToList());
            }
            ViewBag.Per = "Bu Sayfayı Görüntülemeye Yetkiniz Yok";
            
            return View();
          
        }
        public IActionResult workorders() 
        {
            return View();
        }

        public int CheckRole() 
        {
            var filter = dbContexts.Users.Where(x => x.Email == HttpContext.Session.GetString("Eposta")).FirstOrDefault();
            if (filter.Roles == "Yonetici")
            {

               return 0;
            }
            else if (filter.Roles == "Müdür") 
            {
                return 1;
            }
            else if(filter.Roles == "Personel") 
            {
                return 2;
            }
            return 3;
        }
    }
}
