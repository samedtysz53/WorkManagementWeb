using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WorkManagementWeb.Models;

namespace WorkManagementWeb.Controllers
{
    public class MainController : Controller
    {
        //FirebaseController firebaseController;
        private readonly DbContexts DbContexts;
        public MainController(ILogger<HomeController> logger,DbContexts contexts) 
        {
          
            this.DbContexts = contexts;
            //firebaseController=new FirebaseController();
          
        }
      

         

     
    }
}
