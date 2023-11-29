
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
        FirebaseAuthProvider auth;
        private readonly ILogger<HomeController> _logger;
      
     

        // Firebase yapılandırma bilgileri
        

        // HomeController'ın constructor'ı
        public HomeController(ILogger<HomeController> logger)
        {
            auth = new FirebaseAuthProvider(
                        new FirebaseConfig("AIzaSyDaJnKoz9qgdOqK06ewVQbo2HIsKwLxbGc"));
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
            //log in the user
            try
            {
                var fbAuthLink = await auth
                                .SignInWithEmailAndPasswordAsync(userModel.Email, userModel.Password);
                string token = fbAuthLink.FirebaseToken;
                //saving the token in a session variable
                if (token != null)
                {
                    HttpContext.Session.SetString("_UserToken", token);

                    return RedirectToAction("worklist", "Main");
                }
                else
                {
                    return View();
                }
            }catch(Exception e) { ViewBag.ErrorMessages = "Kullanıcı adı veya şifre hatalı"; return View(); }
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserProfile userModel)
        {
            //create the user
            await auth.CreateUserWithEmailAndPasswordAsync(userModel.Email, userModel.Password);
            //log in the new user
            var fbAuthLink = await auth
                            .SignInWithEmailAndPasswordAsync(userModel.Email, userModel.Password);
            string token = fbAuthLink.FirebaseToken;
            //saving the token in a session variable
            if (token != null)
            {
                HttpContext.Session.SetString("_UserToken", token);

                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        // Hata sayfasını gösterme işlemi
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
