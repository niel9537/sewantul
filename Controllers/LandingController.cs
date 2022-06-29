using PaceWeb.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PaceWeb.Controllers
{
    public class LandingController : Controller
    {
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
            ViewBag.wisata = x;
            return View();
        }
    }
}