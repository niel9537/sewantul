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
            var total = x.Sum(e => Int32.Parse(e.jumlah_pengunjung));
            var lokasievakuasi = y.Count;
            var lokasiwisata = x.Count;
            ViewBag.totalwisatawan = total;
            ViewBag.totallokasievakuasi = lokasievakuasi;
            ViewBag.totallokasiwisata = lokasiwisata;
            return View();
        }
        [HttpPost]
        public JsonResult LokasiEvakuasiTerdekat(string latt, string longs)
        {

            var y = RestClient.GetEvakuasi2();
            var from = new GeoCoordinate(Double.Parse(latt, CultureInfo.InvariantCulture), Double.Parse(longs, CultureInfo.InvariantCulture));
            var to = new GeoCoordinate(Double.Parse(y[0].lattitude, CultureInfo.InvariantCulture), (Double.Parse(y[0].longitude, CultureInfo.InvariantCulture)));
            var tempresult = from.GetDistanceTo(to);
            string idresult = "0";
            foreach (Evakuasi evakuasi in y)
            {
                var from2 = new GeoCoordinate(Double.Parse(latt, CultureInfo.InvariantCulture), Double.Parse(longs, CultureInfo.InvariantCulture));
                var to2 = new GeoCoordinate(Double.Parse(evakuasi.lattitude, CultureInfo.InvariantCulture), (Double.Parse(evakuasi.longitude, CultureInfo.InvariantCulture)));
                var results = from2.GetDistanceTo(to2);
                if (results < tempresult)
                {
                    tempresult = results;
                    id = evakuasi.id;
                    nama = evakuasi.nama;
                    alamat = evakuasi.alamat;
                    idresult = evakuasi.id;
                }
            }
            if (idresult.Equals("0"))
            {
                var finalresults = tempresult;
                var finalid = id;
                var finalnama = y[0].nama;
                var finalalamat = y[0].alamat;
                var response = new ResultJarak()
                {
                    id = id,
                    nama = finalnama,
                    alamat = finalalamat,
                    hasil = Math.Round(finalresults, 2).ToString()
                };
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var finalresults = tempresult;
                var finalid = id;
                var finalnama = nama;
                var finalalamat = alamat;
                var response = new ResultJarak()
                {
                    id = id,
                    nama = finalnama,
                    alamat = finalalamat,
                    hasil = Math.Round(finalresults, 2).ToString()
                };
                return Json(response, JsonRequestBehavior.AllowGet);
            }
           
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