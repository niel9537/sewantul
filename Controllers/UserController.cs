using PaceWeb.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
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
            if (TempData["Username"] == null || TempData["Token"] == null)
            {
                return RedirectToAction("Index", "Auth");
            }
            else
            {
                user = TempData["Username"].ToString();
                token = TempData["Token"].ToString();
                TempData.Keep();
                var x = RestClient.GetUsers(token);
                var xresult = from user in x
                              where user.role == "2"
                              select user;
                ViewBag.result = xresult;
                ViewBag.msg = TempData["msg"];
                TempData.Remove("msg");
                TempData.Keep();
                return View();
            }
        }
        public ActionResult SaveUsers(HttpPostedFileBase foto, string username, string password, string nama, string email, string alamat, string notelp, string jeniskelamin)
        {
            if (TempData["Username"] == null || TempData["Token"] == null)
            {
                return RedirectToAction("Index", "Auth");
            }
            else
            {
                try
                {

                    //Method 2 Get file details from HttpPostedFileBase class    
                    user = TempData["Username"].ToString();
                    token = TempData["Token"].ToString();
                    TempData.Keep();
                    if (foto != null)
                    {
                        Console.WriteLine(nama);
                        string tempNama = foto.FileName + DateTime.Now.ToString("MMddyyyyHHmmssfff");
                        if (tempNama.Contains("png"))
                        {
                            tempNama = tempNama.Replace('.', '@') + ".png";
                        }
                        else
                        {
                            tempNama = tempNama.Replace('.', '@') + ".jpg";
                        }
                        string path = Path.Combine(Server.MapPath("~/Assets"), Path.GetFileName(tempNama));
                        foto.SaveAs(path);
                        string x = RestClient.SaveUsers(token, tempNama, username, password, nama, email, alamat, notelp, jeniskelamin);
                        TempData["msg"] = x.ToString();

                    }
                    else
                    {
                        TempData["msg"] = "Sertakan file foto ketika melakukan insert/update"; ;
                    }

                }
                catch (Exception)
                {
                    TempData["msg"] = "Gagal ketika menyimpan file gambar"; ;
                }

                return RedirectToAction("Index", "User");
            }

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
            if (TempData["Username"] == null || TempData["Token"] == null)
            {
                return RedirectToAction("Index", "Auth");
            }
            else
            {
                user = TempData["Username"].ToString();
                token = TempData["Token"].ToString();
                TempData.Keep();
                string x = RestClient.UpdateUsers(token, id, username, password, nama, email, alamat, notelp, jeniskelamin);
                TempData["msg"] = x.ToString();
                return RedirectToAction("Index", "User");
            }

        }
        public ActionResult DeleteUsers(string id)
        {
            if (TempData["Username"] == null || TempData["Token"] == null)
            {
                return RedirectToAction("Index", "Auth");
            }
            else
            {
                user = TempData["Username"].ToString();
                token = TempData["Token"].ToString();
                TempData.Keep();
                string x = RestClient.DeleteUsers(token, id);
                TempData["msg"] = x.ToString();
                return RedirectToAction("Index", "User");
            }

        }

    }
}