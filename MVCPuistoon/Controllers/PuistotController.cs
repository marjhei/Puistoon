using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using MVCPuistoon.Models;
using System.Web;



namespace GoogleMaps.Controllers
{
    public class PuistotController : Controller
    {
       PuistoonContext context = new PuistoonContext();
        public ActionResult Index()
        {
            ViewBag.ListOfDropdown = context.Puistot.ToList();
            return View();
        }
        [HttpPost]
        public JsonResult GetAllLocation()
        {
            var data = context.Puistot.ToList();
            return Json(data);
        }
        [HttpGet]
        public JsonResult GetAllLocationById(int id)
        {
            var data = context.Puistot.Where(x => x.PuistonId == id).SingleOrDefault();
            return Json(data);
        }
    }
}