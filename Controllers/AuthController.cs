using PaceWeb.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PaceWeb.Controllers
{
    public class AuthController : Controller
    {
        static readonly IServerDataRestClient RestClient = new ServerDataRestClient();
        private IServerDataRestClient _restClient;
        public AuthController()
        {
        }
        public AuthController(IServerDataRestClient restClient)
        {
            _restClient = restClient;
        }
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult SignIn(string username, string password)
        {
            string result = RestClient.SignIn(username, password);
            if (result.Equals("9"))
            {
                return RedirectToAction("Index", "Auth");
            }
            else
            {
                TempData["Username"] = username;
                TempData["Token"] = result;
                TempData.Keep();
                return RedirectToAction("Index", "Home");

            }

        }

        public ActionResult SignOut()
        {
/*            TempData.Remove("Username");
            TempData.Remove("Token");*/
            TempData.Clear();
            return RedirectToAction("Index", "Auth");

        }


    }
}