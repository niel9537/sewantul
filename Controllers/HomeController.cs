using PaceWeb.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PaceWeb.Controllers
{
    public class HomeController : Controller
    {

        string user = "";
        string token = "";
        static readonly IServerDataRestClient RestClient = new ServerDataRestClient();
        private IServerDataRestClient _restClient;
        public HomeController()
        {
        }
        public HomeController(IServerDataRestClient restClient)
        {
            _restClient = restClient;
        }
        public ActionResult Index()
        {
            if (TempData["Username"] == null || TempData["Token"] == null)
            {
                return RedirectToAction("Index", "Auth");
            }
            else
            {
                user = TempData["Username"].ToString();
                token = TempData["Token"].ToString();
                if (user.Equals("superadmin")){
                    TempData["Role"] = "superadmin";
                }
                else
                {

                    TempData["Role"] = "admin";
                    var x = RestClient.GetUsers(token);
                    var xresult = from users in x
                              where users.username == user
                              select users;
                    ViewBag.foto = xresult.ToList()[0].foto.ToString();
                    ViewBag.nama = xresult.ToList()[0].nama.ToString();
                    ViewBag.email = xresult.ToList()[0].email.ToString();
                }
                TempData.Keep();
                return View();
            }

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}