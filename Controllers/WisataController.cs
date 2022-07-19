using PaceWeb.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PaceWeb.Controllers
{
    public class WisataController : Controller
    {
        string user = "";
        string token = "";
        static readonly IServerDataRestClient RestClient = new ServerDataRestClient();
        private IServerDataRestClient _restClient;
        public WisataController()
        {
        }
        public WisataController(IServerDataRestClient restClient)
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
                var x = RestClient.GetWisata(token);
                ViewBag.result = x;
                ViewBag.msg = TempData["msg"];
                TempData.Remove("msg");
                TempData.Keep();
                return View();
            }
        }
        public ActionResult SaveWisata(HttpPostedFileBase foto, string nama, string alamat, string longitude, string lattitude, string keterangan, string jumlah_pengunjung)
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
                        string x = RestClient.SaveWisata(token, nama, alamat, longitude, lattitude, tempNama, keterangan, jumlah_pengunjung);
                        TempData["msg"] = x.ToString();

                    }
                }
                catch (Exception)
                {
                    TempData["msg"] = "Gagal ketika menyimpan file gambar"; ;
                }
                return RedirectToAction("Index", "Wisata");
            }

        }
        public JsonResult ShowWisataById(string id)
        {
            user = TempData["Username"].ToString();
            token = TempData["Token"].ToString();
            TempData.Keep();
            var result = RestClient.GetWisata(token).Find(x => x.id.Equals(id.ToString()));
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateWisata(HttpPostedFileBase foto, string id, string nama, string alamat, string longitude, string lattitude, string keterangan, string jumlah_pengunjung)
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
                        string x = RestClient.UpdateWisata(token, id, nama, alamat, longitude, lattitude, tempNama, keterangan, jumlah_pengunjung);
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
                return RedirectToAction("Index", "Wisata");
            }

        }
        public ActionResult DeleteWisata(string id)
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
                string x = RestClient.DeleteWisata(token, id);
                TempData["msg"] = x.ToString();
                return RedirectToAction("Index", "Wisata");
            }

        }

    }
}