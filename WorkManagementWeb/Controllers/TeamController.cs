﻿using Microsoft.AspNetCore.Mvc;
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
        public IActionResult BackSelect() 
        {

            return RedirectToAction("SelectTeam", "Main");
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
        public IActionResult TeamJoblist() 
        {
            if (SessionControl())
            {
                var code = HttpContext.Session.GetString("UserCode");



                if (code != null)
                {
                    var team = dbContext.TeamMembers.Where(x => x.UserCode == code).ToList();
                    return View(team);
                }

            }
            var list = dbContext.TeamJoblists.ToList();
            return View(list);
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
                var select = dbContext.Team.Where(x=>x.MemberCode==ID).ToList();

                return select;
            }
            return null;

        }
    }
}