using PaceWeb.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PaceWeb.Controllers
{
    public class EvakuasiController : Controller
    {
        string user = "";
        string token = "";
        static readonly IServerDataRestClient RestClient = new ServerDataRestClient();
        private IServerDataRestClient _restClient;
        public EvakuasiController()
        {
        }
        public EvakuasiController(IServerDataRestClient restClient)
        {
            _restClient = restClient;
        }

        public ActionResult Index()
        {
                user = TempData["Username"].ToString();
                token = TempData["Token"].ToString();
                TempData.Keep();
                var x = RestClient.GetEvakuasi(token);
                ViewBag.result = x;
                ViewBag.msg = TempData["msg"];
                TempData.Remove("msg");
                TempData.Keep();
                return View();
        }
        public ActionResult SaveEvakuasi(HttpPostedFileBase foto, string nama, string alamat, string longitude, string lattitude, string keterangan)
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
                    string x = RestClient.SaveEvakuasi(token, nama, alamat, longitude, lattitude, tempNama, keterangan);
                    TempData["msg"] = x.ToString();

                }
            }
            catch (Exception)
            {
                TempData["msg"] = "Gagal ketika menyimpan file gambar"; ;
            }
            return RedirectToAction("Index", "Evakuasi");

        }
        public JsonResult ShowEvakuasiById(string id)
        {
            user = TempData["Username"].ToString();
            token = TempData["Token"].ToString();
            TempData.Keep();
            var result = RestClient.GetEvakuasi(token).Find(x => x.id.Equals(id.ToString()));
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateEvakuasi(HttpPostedFileBase foto, string id, string nama, string alamat, string longitude, string lattitude, string keterangan)
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
                    string x = RestClient.UpdateEvakuasi(token, id, nama, alamat, longitude, lattitude, tempNama, keterangan);
                    TempData["msg"] = x.ToString();

                }
            }
            catch (Exception)
            {
                TempData["msg"] = "Gagal ketika menyimpan file gambar"; ;
            }
            return RedirectToAction("Index", "Evakuasi");

        }
        public ActionResult DeleteEvakuasi(string id)
        {
            user = TempData["Username"].ToString();
            token = TempData["Token"].ToString();
            TempData.Keep();
            string x = RestClient.DeleteEvakuasi(token,id);
            TempData["msg"] = x.ToString();
            return RedirectToAction("Index", "Evakuasi");

        }

    }
}