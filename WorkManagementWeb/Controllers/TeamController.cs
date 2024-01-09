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


        [HttpGet]
        public IActionResult TeamCreate()
        {
            if (SessionControl())
            {
                ViewBag.User = HttpContext.Session.GetString("Email") + "(" + HttpContext.Session.GetString("UserCode") + ")";
                return View();
            }
            return RedirectToAction("Index", "Home");
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
            if (SessionControl()) { 
            var code = HttpContext.Session.GetString("UserCode");



            if (code != null)
            {




                return View(GetTeamList(code));
            }
            }
            else { 
            return View("Index","Home");
            }
            return View();
                 
        }

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
                var select = dbContext.Team.Where(x => x.MemberCode == ID).ToList();

                return select;
            }
            return null;

        }



        [HttpGet]
        public IActionResult TeamJobCreaete()
        {
            return View();

        }
        [HttpGet]
            public IActionResult GetTeamJob(int id)
            {
                if (SessionControl())
                {
                    var filter = dbContext.Team.Where(x => x.T_ID == id).FirstOrDefault();
                    if (filter != null)
                    {
                        var query = dbContext.TeamJoblists.Where(x => x.TeamCode == filter.TCode).ToList();
                    HttpContext.Session.SetInt32("Stid",id);
                    HttpContext.Session.SetInt32("ID", id);
                    return View(query);
                    }

                    return View(null);
                }
                return View("Index", "Home");
            }
        [HttpPost]
        
        public IActionResult GetTeamJob(string taskname, string Description)
        {
            var JID = HttpContext.Session.GetInt32("ID");

            var filter = dbContext.Team.FirstOrDefault(x => x.T_ID == JID);


            TeamJoblist taskListModels = new TeamJoblist();
            taskListModels.TeamJobName = taskname;
            taskListModels.TeamCode=filter.TCode;
            taskListModels.Time = DateTime.Now;
          
             
            dbContext.TeamJoblists.Add(taskListModels);
            dbContext.SaveChanges();
            return RedirectToAction("GetTeamJob");
        }

        [HttpGet]
            public IActionResult JobCreate()
            {

                return View();
            }

            [HttpPost]
            public IActionResult TeamListDelete()
            {
                int? Tıd = HttpContext.Session.GetInt32("Tid");

                if (Tıd.HasValue)
                {
                    var query = dbContext.Team.Where(x => x.T_ID == Tıd).FirstOrDefault();
                    dbContext.Remove(query);
                    if (query != null)
                    {
                        var TeamJoblistQuery = dbContext.TeamJoblists.Where(x => x.TeamCode == query.TCode).FirstOrDefault();
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
            public IActionResult TeamJobList()
            {


                return View();


            }
        [HttpPost]
        public IActionResult TeamJobList(string TeamJobName)
        {
          var session= HttpContext.Session.GetInt32("Stid");
            var filter = dbContext.Team.Where(x=>x.T_ID== session).FirstOrDefault();
            if (filter != null) 
            {
                TeamJoblist teamJoblist = new TeamJoblist();
                teamJoblist.TeamJobName = TeamJobName;
                teamJoblist.TeamCode = filter.TCode;
                teamJoblist.Time = DateTime.Now;
                dbContext.TeamJoblists.Add(teamJoblist);
                dbContext.SaveChanges();
            }

            return View();


        }
        [HttpGet]
        public IActionResult TSonuc(int id)
        {
            if (SessionControl())
            {
                HttpContext.Session.SetInt32("ID", id);
 

                return View(getTasklist(id));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult TSonuc(string taskname, string Description)
        {
            var JID = HttpContext.Session.GetInt32("ID");

            var filter = dbContext.TeamJoblists.Where(x => x.T_JID == JID).FirstOrDefault();


            TeamTaskName TeamTaskName = new TeamTaskName();
            TeamTaskName.TTaskName = taskname;
            TeamTaskName.TeamID = filter.T_JID;
            TeamTaskName.Time = DateTime.Now;
            TeamTaskName.Done = true;
            TeamTaskName.Added_by = HttpContext.Session.GetString("Email");
            dbContext.TeamTaskNames.Add(TeamTaskName);
            dbContext.SaveChanges();
            return RedirectToAction("TSonuc");
        }

        public List<TeamTaskName> getTasklist(int ID)
        {
            var filter = dbContext.TeamJoblists.FirstOrDefault(x => x.T_JID == ID);
            if (filter != null)
            {
                var list = dbContext.TeamTaskNames.Where(x => x.TeamID == filter.T_JID && x.Done == true).ToList();


                return list;
            }
            return null;

        }

    }

}