
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using WorkManagementWeb.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Firebase.Auth;
namespace WorkManagementWeb.Models
{
    public class FirebaseAuthService
    {
        FirebaseAuthProvider auth;

        public FirebaseAuthService() 
        {

            auth = new FirebaseAuthProvider(
                        new FirebaseConfig("AIzaSyDaJnKoz9qgdOqK06ewVQbo2HIsKwLxbGc"));
        }
        public async Task<bool> Login(UserProfile UserProfile) 
        {
            //log in the user
            try
            {
                var fbAuthLink = await auth
                                .SignInWithEmailAndPasswordAsync(UserProfile.Email, UserProfile.Password);
                string token = fbAuthLink.FirebaseToken;
                //saving the token in a session variable
                if (token != null)
                {

                    return true;

                    //return RedirectToAction("worklist", "Main");
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e) { return false; }
        }
    }
}
