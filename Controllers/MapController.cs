using PaceWeb.Helpers;
using PaceWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PaceWeb.Controllers
{
    public class MapController : Controller
    {
        static readonly IServerDataRestClient RestClient = new ServerDataRestClient();
        private IServerDataRestClient _restClient;
        public MapController()
        {
        }
        public MapController(IServerDataRestClient restClient)
        {
            _restClient = restClient;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetLocations()
        {
            var locations = RestClient.GetEvakuasi2();
            return Json(locations, JsonRequestBehavior.AllowGet);
        }
    }

    
}