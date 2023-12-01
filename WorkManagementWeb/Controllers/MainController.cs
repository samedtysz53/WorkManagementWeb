using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
            if (SessionControl())
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Sonuc() 
        {
            if (SessionControl())
            {

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public IActionResult worklist() 
        {
            if (SessionControl()) 
            {
                var list = firebaseController.GetJoblist();
                ViewBag.User = HttpContext.Session.GetString("Email");
                return View(list);
            }
            else 
            {
                return RedirectToAction("Index","Home");
            }
           
        }
        [HttpGet]
        public IActionResult JobCreate() 
        {
            if (SessionControl())
            {
                ViewBag.User = HttpContext.Session.GetString("Email");
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public IActionResult JobCreate(JoblistModels joblistModels) 
        {

            if (SessionControl())
            {
                string Name = HttpContext.Session.GetString("Email");
                ViewBag.User = HttpContext.Session.GetString("Email");
                firebaseController.CreateJobList(joblistModels,Name);
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

           
        }

        public bool SessionControl()
        {
            string userEmail = HttpContext.Session.GetString("Email");

            if (!string.IsNullOrEmpty(userEmail))
            {
                // Session "Email" değeri boş değilse
                return true;
            }

            // Session "Email" değeri boşsa
            return false;
        }

        public IActionResult CloseD() 
        {
        HttpContext.Session.Clear();
        return RedirectToAction("Index","Home");
        }
    }
}
