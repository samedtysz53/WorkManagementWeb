using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SelectedItem() 
        {
            var id = HttpContext.Session.GetInt32("id");
            var filter = dbContext.User.Where(x => x.U_ID == id).FirstOrDefault();
            if (filter != null)
            {
                var TeamFilter = dbContext.Team.Where(x => x.TCode == filter.TeamCode).ToList();
                return View(TeamFilter);
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
            dbContext.Team.Add(team);
            dbContext.SaveChanges();
            var Code = HttpContext.Session.GetString("UserCode");
            var query = dbContext.User.FirstOrDefault(x => x.RandomCode == Code);
            if (query != null) 
            {
                query.TeamCode = team.TCode;
               
                dbContext.SaveChanges();
            }
            return View();
            
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
        public IActionResult BackSelect() 
        {

            return RedirectToAction("SelectTeam", "Main");
        }

        public IActionResult TeamJoblist() 
        {
            if (SessionControl())
            {
                var code = HttpContext.Session.GetString("UserCode");
                
             

                if (code!=null) 
                {
                    var filter = dbContext.User.FirstOrDefault(x => x.RandomCode == code);
                    if(filter!= null) 
                    {
                        var filter2 = dbContext.Team.Where(x => x.TCode == code).ToList();
                        return View(filter2);
                    }
                    else 
                    {
                        return View();
                    }
                }
              
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
    }
}
