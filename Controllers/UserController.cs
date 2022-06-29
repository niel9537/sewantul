using PaceWeb.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PaceWeb.Controllers
{
    public class UserController : Controller
    {
        string user = "";
        string token = "";
        static readonly IServerDataRestClient RestClient = new ServerDataRestClient();
        private IServerDataRestClient _restClient;
        public UserController()
        {
        }
        public UserController(IServerDataRestClient restClient)
        {
            _restClient = restClient;
        }

        public ActionResult Index()
        {
                user = TempData["Username"].ToString();
                token = TempData["Token"].ToString();
                TempData.Keep();
                var x = RestClient.GetUsers(token);
                ViewBag.result = x;
                ViewBag.msg = TempData["msg"];
                TempData.Remove("msg");
                TempData.Keep();
                return View();
        }
        public ActionResult SaveUsers(string username, string password, string nama, string email, string alamat, string notelp, string jeniskelamin)
        {
            user = TempData["Username"].ToString();
            token = TempData["Token"].ToString();
            TempData.Keep();
            string x = RestClient.SaveUsers(token,username, password, nama, email, alamat, notelp, jeniskelamin);
            TempData["msg"] = x.ToString();
            return RedirectToAction("Index", "User");

        }
        public JsonResult ShowUserById(string id)
        {
            user = TempData["Username"].ToString();
            token = TempData["Token"].ToString();
            TempData.Keep();
            var result = RestClient.GetUsers(token).Find(x => x.id.Equals(id.ToString()));
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateUsers(string id,string username, string password, string nama, string email, string alamat, string notelp, string jeniskelamin)
        {
            user = TempData["Username"].ToString();
            token = TempData["Token"].ToString();
            TempData.Keep();
            string x = RestClient.UpdateUsers(token,id,username, password, nama, email, alamat, notelp, jeniskelamin);
            TempData["msg"] = x.ToString();
            return RedirectToAction("Index", "User");

        }
        public ActionResult DeleteUsers(string id)
        {
            user = TempData["Username"].ToString();
            token = TempData["Token"].ToString();
            TempData.Keep();
            string x = RestClient.DeleteUsers(token,id);
            TempData["msg"] = x.ToString();
            return RedirectToAction("Index", "User");

        }

    }
}