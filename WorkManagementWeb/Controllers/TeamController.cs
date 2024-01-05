using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WorkManagementWeb.Models;

namespace WorkManagementWeb.Controllers
{
    public class TeamController : Controller
    {
        private readonly DbContexts dbContext;
        private readonly ILogger<TeamController> _logger;
       
      public TeamController(ILogger<TeamController> logger, DbContexts dbContext) 
        {
            
                this.dbContext = dbContext;
                _logger = logger;
           
           

        }
<<<<<<< HEAD
   
      
=======
    
        public IActionResult SelectedItem() 
        {
            var id = HttpContext.Session.GetInt32("id");
            var filter = dbContext.User.Where(x => x.U_ID == id).FirstOrDefault();
            if (filter != null)
            {
                //var TeamFilter = dbContext.Team.Where(x => x.TCode == filter.TeamCode).ToList();
                //return View(TeamFilter);
                return View();
            }

            return View();
        }
>>>>>>> e1c61019046274e44b629522ef33d33da9749a30

        [HttpGet]
        public IActionResult TeamCreate() 
        {
            if (SessionControl())
            {
                ViewBag.User = HttpContext.Session.GetString("Email") + "(" + HttpContext.Session.GetString("UserCode") + ")";
                return View();
            }
           return RedirectToAction("Index","Home");
        }
        [HttpPost]
        public IActionResult TeamCreate(string TeamName)
        {
            Team team = new Team();
            team.TeamName = TeamName;
           
            string uniqueCode;
            do
            {
                uniqueCode = GenerateRandomCode();
            } while (IsCodeExists(uniqueCode));

            team.TCode = "#" + uniqueCode;

            ViewBag.User = HttpContext.Session.GetString("Email") + "(" + HttpContext.Session.GetString("UserCode") + ")";
            team.MemberCode = HttpContext.Session.GetString("UserCode");
            dbContext.Team.Add(team);
            dbContext.SaveChanges();
            
                return RedirectToAction("TeamList");
          
       
            
        }
        private string GenerateRandomCode()
        {
            Random random = new Random();
            int code = random.Next(0, 10000); // 1000 ile 9999 arasında random sayı üret
            return code.ToString();
        }
        private bool IsCodeExists(string code)
        {
            return dbContext.Team.Any(u => u.TCode == code);
        }
     
        public IActionResult TeamList() 
        {
            var code = HttpContext.Session.GetString("UserCode");



            if (code != null)
            {

               


                return View(GetTeamList(code));
            }
            return View();
        }
<<<<<<< HEAD
    
=======
       
>>>>>>> e1c61019046274e44b629522ef33d33da9749a30

        public bool SessionControl()
        {
            if (HttpContext != null)
            {
                string userEmail = HttpContext.Session.GetString("Email");

                if (!string.IsNullOrEmpty(userEmail))
                {
                    // Session "Email" değeri boş değilse
                    return true;
                }
                return false;
            }
            // Session "Email" değeri boşsa
            return false;
        }


        public List<Team> GetTeamList(string ID)
        {
            var filter = dbContext.Team.FirstOrDefault(x => x.MemberCode == ID);
            if (filter != null)
            {
                var select = dbContext.Team.Where(x=>x.MemberCode==ID).ToList();

                return select;
            }
            return null;

        }

<<<<<<< HEAD


        [HttpGet]
        public IActionResult TeamJobCreaete()
        {
=======
        [HttpGet]
        public IActionResult GetTeamJob(int id) 
        {
            if (SessionControl()) 
            {
                var filter = dbContext.Team.Where(x=>x.T_ID==id).FirstOrDefault();
                if (filter != null) 
                {
                    var query = dbContext.TeamJoblists.Where(x=>x.TeamCode==filter.TCode).ToList();
                    return View(query);
                }

                return View(null);
            }
            return View("Index","Home");
        }


        [HttpGet]
        public IActionResult JobCreate() 
        {

        return View();
        }
<<<<<<< HEAD

=======
        [HttpPost]
        public IActionResult TeamListDelete()
        {
            int? Tıd = HttpContext.Session.GetInt32("Tid");

            if (Tıd.HasValue)
            {
                var query = dbContext.Team.Where(x=>x.T_ID==Tıd).FirstOrDefault();
                dbContext.Remove(query);
                if(query!=null)
                {
                    var TeamJoblistQuery = dbContext.TeamJoblists.Where(x=>x.TeamCode==query.TCode).FirstOrDefault();
                    dbContext.Remove(TeamJoblistQuery);

                    if (TeamJoblistQuery != null) 
                    {
                        //var TeamTaskList = dbContext.TeamTaskNames.Where(x=>x.);
                    }

                }


            }
            //hatalı kod düzeltilecek
            return View();
        }
        [HttpGet]
        public IActionResult TeamJobList(int id)
        {
            
>>>>>>> e1c61019046274e44b629522ef33d33da9749a30
            return View();
>>>>>>> a7391be64bdda0fa4c20dcef1b06dd374c98b405

        }




    }
}
