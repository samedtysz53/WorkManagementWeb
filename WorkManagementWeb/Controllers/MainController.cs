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
      

        [HttpGet]
        public IActionResult Register() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(User user)
        {

            return View();
        }

        [HttpGet]
        public IActionResult Sonuc(int id) 
        {
            if (SessionControl())
            {
                HttpContext.Session.SetInt32("ID",id);

               
                return View(getTasklist(id));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult Sonuc(string taskname,string Description) 
        {
            var JID = HttpContext.Session.GetInt32("ID");

            var filter = DbContexts.JoblistModels.FirstOrDefault(x=>x.ID==JID);
           

            TaskListModels taskListModels = new TaskListModels();
            taskListModels.TaskName = taskname;
            taskListModels.JobListName = filter.JobListName;
            taskListModels.Time = DateTime.Now;
            taskListModels.Description = Description;
            taskListModels.Done = true;
            DbContexts.TaskListModels.Add(taskListModels);
            DbContexts.SaveChanges();
            return RedirectToAction("Sonuc");
        }
        public IActionResult IMenu() { return PartialView("IMenu"); }
        public IActionResult worklist() 
        {
            if (SessionControl()) 
            {
                //var list = firebaseController.GetJoblist();
                ViewBag.User = HttpContext.Session.GetString("Email");
                return View(GetJoblist());
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
                joblistModels.Time = DateTime.Now;
                string Name = HttpContext.Session.GetString("Email");
                ViewBag.User = HttpContext.Session.GetString("Email");
                //firebaseController.CreateJobList(joblistModels,Name);
                JoblistAdd(joblistModels);
                return RedirectToAction("worklist","Main");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

           
        }

        [HttpGet]
        public IActionResult JobDelete(int id) 
        {
            if (SessionControl())
            {
                var filter = DbContexts.JoblistModels.FirstOrDefault(j => j.ID == id);
                if (filter != null)
                {
                    DbContexts.JoblistModels.Remove(filter);
                    DbContexts.SaveChanges();
                }
                return View("worklist");
            }
            return RedirectToAction("Index","Home");
        }


        [HttpGet]
        public IActionResult Day() 
        {
            if (SessionControl())
            {
                var filter = DbContexts.TaskListModels.Where(x => x.Time >= DateTime.Today && x.Time < DateTime.Today.AddDays(1) && x.Done == true).ToList();

                return View(filter);
            }
            return RedirectToAction("Index", "Home");
        }
       
        public IActionResult SelectTeam() 
        {
            if (SessionControl())
            {


                return View();
            }
            return RedirectToAction("Index", "Home");
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

      public IActionResult TaskEnd(int id) 
        {
            if (SessionControl()) 
            {
                var filter = DbContexts.TaskListModels.FirstOrDefault(x => x.ID == id && x.Done == true);
                if (filter != null)
                {
                    filter.Done = false;
                    filter.EndTime = DateTime.Now;
                    // Diğer güncelleme işlemlerini buraya ekleyin

                    // Değişiklikleri kaydedin
                    DbContexts.SaveChanges();
                }
            }
            return RedirectToAction("CompletedTask");
        }

        public IActionResult CompletedTask() 
        {

            if (SessionControl()) 
            {
                var filter = DbContexts.TaskListModels.Where(x => x.Done == false).ToList();
                return View(filter);
            }
            return RedirectToAction("Index","Main");
        }
        public void JoblistAdd(JoblistModels joblistModels) 
        {
            joblistModels.Email = HttpContext.Session.GetString("Email");
           
            DbContexts.JoblistModels.Add(joblistModels);
            DbContexts.SaveChanges();
           

        }
        public void JoblistUpdate(string JName) 
        {
            var filter = DbContexts.JoblistModels.FirstOrDefault(j => j.JobListName == JName);
            if (filter != null) 
            {
                filter.JobListName = JName;

                var filter2 = DbContexts.TaskListModels.FirstOrDefault(t=>t.JobListName==JName);
                if(filter2 != null) 
                {

                filter2.JobListName=JName;
                DbContexts.SaveChanges();
                
                }
                DbContexts.SaveChanges();
            }

        }

        public void JoblistDelte(string JName) 
        {

            var filter = DbContexts.JoblistModels.FirstOrDefault(j=>j.JobListName==JName);
            if (filter != null) 
            {
                DbContexts.JoblistModels.Remove(filter);
                DbContexts.SaveChanges();
            }


        }

        public List<JoblistModels> GetJoblist() 
        {
            var query = DbContexts.JoblistModels.ToList();
            return query;
        }

        public List<TaskListModels> getTasklist(int ID) 
        {
            var filter = DbContexts.JoblistModels.FirstOrDefault(x => x.ID == ID);
            if (filter != null)
            {
                var list = DbContexts.TaskListModels.Where(x => x.JobListName == filter.JobListName && x.Done == true).ToList();


                return list;
            }
            return null;

        }
        [HttpGet]
        public IActionResult TaskUpdate(int id) 
        {
            if (SessionControl())
            {
                var filter = DbContexts.TaskListModels.FirstOrDefault(x => x.ID == id);
                HttpContext.Session.SetInt32("id", id);
                return View(filter);
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult TaskUpdate(string Taskname,string Description)
        {
            var id = HttpContext.Session.GetInt32("id");
            var filter = DbContexts.TaskListModels.FirstOrDefault(x => x.ID == id);
            filter.TaskName = Taskname;
            filter.Description = Description;
            DbContexts.SaveChanges();
            return RedirectToAction("worklist","Main");
        }

    }
}
