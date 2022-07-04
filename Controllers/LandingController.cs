using PaceWeb.Helpers;
using PaceWeb.Models;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PaceWeb.Controllers
{
    public class LandingController : Controller
    {
        string nama, id, alamat;
        static readonly IServerDataRestClient RestClient = new ServerDataRestClient();
        private IServerDataRestClient _restClient;
        public LandingController()
        {
        }
        public LandingController(IServerDataRestClient restClient)
        {
            _restClient = restClient;
        }
        public ActionResult Index()
        {
            var x = RestClient.GetWisata2();
            var y = RestClient.GetEvakuasi2();
            var listlatitude = y.Select(c => c.lattitude).ToList();
            var listlongitude = y.Select(d => d.longitude).ToList();

            ViewBag.wisata = x;
            ViewBag.evakuasi = y;
            
            return View();
        }
        public JsonResult LokasiEvakuasiTerdekat()
        {
            var y = RestClient.GetEvakuasi2();
            var from = new GeoCoordinate(-7.76864743226932, 110.42511082625096);
            var to = new GeoCoordinate(Double.Parse(y[0].lattitude, CultureInfo.InvariantCulture), (Double.Parse(y[0].longitude, CultureInfo.InvariantCulture)));
            var tempresult = from.GetDistanceTo(to);

            foreach (Evakuasi evakuasi in y)
            {
                var from2 = new GeoCoordinate(-7.76864743226932, 110.42511082625096);
                var to2 = new GeoCoordinate(Double.Parse(evakuasi.lattitude, CultureInfo.InvariantCulture), (Double.Parse(evakuasi.longitude, CultureInfo.InvariantCulture)));
                var results = from2.GetDistanceTo(to2);
                if (results < tempresult)
                {
                    tempresult = results;
                    id = evakuasi.id;
                    nama = evakuasi.nama;
                    alamat = evakuasi.alamat;
                }
            }
            var finalresults = tempresult;
            var finalid = id;
            var finalnama = nama;
            var finalalamat = alamat;
            var response = new ResultJarak()
            {
                id = id,
                nama = finalnama,
                alamat = finalalamat,
                hasil = Math.Round(finalresults,2).ToString()
            };
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Wisata(string id)
        {
            var x = RestClient.GetWisata2().Find(y => y.id.Equals(id.ToString()));
            ViewBag.foto = x.foto;
            ViewBag.nama = x.nama;
            ViewBag.jumlah = x.jumlah_pengunjung;
            ViewBag.keterangan = x.keterangan;
            return View("Wisata");
        }
        public ActionResult Evakuasi(string id)
        {
            var x = RestClient.GetEvakuasi2().Find(y => y.id.Equals(id.ToString()));
            ViewBag.foto = x.foto;
            ViewBag.nama = x.nama;
            ViewBag.keterangan = x.keterangan;
            return View("Evakuasi");
        }
    }
}