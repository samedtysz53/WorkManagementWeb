
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using WorkManagementWeb.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Firebase.Auth;
namespace WorkManagementWeb.Controllers
{
    public class HomeController : Controller
    {
      
        private readonly ILogger<HomeController> _logger;
      
     

        // Firebase yapılandırma bilgileri
        

        // HomeController'ın constructor'ı
        public HomeController(ILogger<HomeController> logger)
        {
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
        public async Task<IActionResult> Index(UserProfile userModel)
        {
            try
            {
                FirebaseAuthService firebaseAuthService = new FirebaseAuthService();
                bool loginresult = await firebaseAuthService.Login(userModel);
                if (loginresult == true)
                {
                    HttpContext.Session.SetString("Email", userModel.Email);
                    userModel.Email = HttpContext.Session.GetString("Email").ToString();
                    return RedirectToAction("worklist", "Main");
                }
                else
                {
                    ViewBag.ErrorMessages = "Hatakı şifre veya kullanıcı adı";
                    return View();
                }

            }
            catch (Exception e) 
            {
                ViewBag.ErrorMessages = "Hatakı şifre veya kullanıcı adı";
                return View(); 
            }
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserProfile userModel)
        {
            //create the user
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
