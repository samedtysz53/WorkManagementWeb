using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

using FireSharp.Interfaces;
using FireSharp.Config;
using FireSharp.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace WorkManagementWeb.Models
{
    public class FirebaseController
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "pKqeO8p410N7jIb1k5WIiCKJovItF6divZyEFQe0\r\n",
            BasePath = "https://workmanagement-8c1ad-default-rtdb.europe-west1.firebasedatabase.app/"
        };
        IFirebaseClient client;

        public async Task CreateJobList(JoblistModels joblistModels) 
        {
            JoblistModels joblistModels1 = new JoblistModels();
            joblistModels1.JobListName = joblistModels.JobListName;
            joblistModels1.Time = DateTime.Now;

            // Firebase Client oluşturma
            client = new FireSharp.FirebaseClient(config);
            var data = joblistModels1;

            // Firebase'e yeni veri ekleme
            PushResponse response = client.Push(joblistModels1.JobListName,"");
            //data.JobListName = response.Result.name;
            //SetResponse setResponse = client.Set("/" + data.JobListName, data);
        }
        public List<JoblistModels> GetJoblist() 
        {
            client = new FireSharp.FirebaseClient(config);

            // Firebase Realtime Database'den veri çekme
            FirebaseResponse response = client.Get("Joblist");
            dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);

            // Veriyi işleme
            var list = new List<JoblistModels>();
            if (data != null)
            {
                foreach (var item in data)
                {
                    list.Add(JsonConvert.DeserializeObject<JoblistModels>(((JProperty)item).Value.ToString()));
                }
                return list;
            }
            else
            {
                return null;
            }
        }
        public async void UpdateJoblist() 
        {
        
        }
        public async void DeleteJoblist()
        {

        }

        public async void CreateTaskList()
        {

        }
        public async void GetTasklist()
        {

        }
        public async void UpdateTasklist()
        {

        }
        public async void DeleteTasklist()
        {

        }

    }
}
