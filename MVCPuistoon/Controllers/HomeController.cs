using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MVCPuistoon.Models;


namespace MVCPuistoon.Controllers
{



    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IConfiguration configuration;
        public HomeController(IConfiguration configuration, ILogger<HomeController> logger)
        {
            this.configuration = configuration;
            _logger = logger;
        }

       // GET: Home
        public IActionResult PuistonTapahtumat()
        {
            CultureInfo ci = new CultureInfo("en-US");
            string Markers = "[";
            string conString = "";
            SqlCommand cmd = new SqlCommand("SELECT * FROM Puistot");
            using (SqlConnection con = new SqlConnection(conString))
            {
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                       
                        Markers += "{";
                        Markers += string.Format("'id': '{0}',", sdr["PuistonID"]);
                        Markers += string.Format("'nimi': '{0}',", sdr["PuistonNimi"]);
                        Markers += string.Format(ci,"'lat': '{0}',", sdr["Lat"]);
                        Markers += string.Format(ci,"'lng': '{0}',", sdr["Long"]);
                        Markers += string.Format("'kuvaus': '{0}',", sdr["PuistonKuvaus"]);
                        Markers += string.Format("'url': '{0}',", sdr["PuistonURL"]);
                        Markers += string.Format("'zoom': '{0}'", sdr["ZoomLevel"]);
                        Markers += "},";
                    }
                }
                con.Close();
            }

            Markers += "];";
            ViewBag.Markers = Markers;

            string merkit = "[";
            string conString2 = "";
            SqlCommand cmd2 = new SqlCommand("SELECT * FROM Palvelut WHERE TyyppiID = 1");    // hakee alepat
            using (SqlConnection con = new SqlConnection(conString2))
            {
                cmd2.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd2.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        merkit += "{";
                        merkit += string.Format("'id': '{0}',", sdr["PalvelutID"]);
                        merkit += string.Format("'tyyppi': '{0}',", sdr["TyyppiID"]);
                        merkit += string.Format(ci, "'lat': '{0}',", sdr["Lat"]);
                        merkit += string.Format(ci, "'lng': '{0}',", sdr["Long"]);
                        merkit += string.Format("'nimi': '{0}',", sdr["Kuvaus"]);
                        merkit += string.Format("'url': '{0}',", sdr["Lisätietoja"]);
                        merkit += "},";
                    }
                }
                con.Close();
            }
            merkit += "];";
            ViewBag.Merkit = merkit;

            string vessat = "[";
            string conString3 = "";
            SqlCommand cmd3 = new SqlCommand("SELECT * FROM Palvelut WHERE TyyppiID = 2");    // hakee vessat
            using (SqlConnection con = new SqlConnection(conString3))
            {
                cmd3.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd3.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        vessat += "{";
                        vessat += string.Format("'id': '{0}',", sdr["PalvelutID"]);
                        vessat += string.Format("'tyyppi': '{0}',", sdr["TyyppiID"]);
                        vessat += string.Format(ci, "'lat': '{0}',", sdr["Lat"]);
                        vessat += string.Format(ci, "'lng': '{0}',", sdr["Long"]);
                        vessat += string.Format("'nimi': '{0}',", sdr["Kuvaus"]);
                        vessat += string.Format("'url': '{0}',", sdr["Lisätietoja"]);
                        vessat += "},";
                    }
                }
                con.Close();
            }
            vessat += "];";
            ViewBag.Vessat = vessat;

            string pyorat = "[";
            string conString4 = "";
            SqlCommand cmd4 = new SqlCommand("SELECT * FROM Palvelut WHERE TyyppiID = 3");    // hakee pyörät
            using (SqlConnection con = new SqlConnection(conString4))
            {
                cmd4.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd4.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        pyorat += "{";
                        pyorat += string.Format("'id': '{0}',", sdr["PalvelutID"]);
                        pyorat += string.Format("'tyyppi': '{0}',", sdr["TyyppiID"]);
                        pyorat += string.Format(ci, "'lat': '{0}',", sdr["Lat"]);
                        pyorat += string.Format(ci, "'lng': '{0}',", sdr["Long"]);
                        pyorat += string.Format("'nimi': '{0}',", sdr["Kuvaus"]);
                        pyorat += string.Format("'url': '{0}',", sdr["Lisätietoja"]);
                        pyorat += "},";
                    }
                }
                con.Close();
            }
            pyorat += "];";
            ViewBag.Pyorat = pyorat;
            PuistoonContext db = new PuistoonContext();
            return View(db.Tapahtumat.ToList());
        }

        public JsonResult GetTapahtuma(int id)
        {
            using (PuistoonContext db = new PuistoonContext())
            {
                var tapahtumadata = db.Tapahtumat.Where(x => x.PuistonId == id).ToList();
                return Json(tapahtumadata);
            }
        }
   
        PuistoonContext context = new PuistoonContext();
        public ActionResult Testi()
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
        [HttpPost]
        public JsonResult GetAllLocationById(int id)
        {
            var data = context.Puistot.Where(x => x.PuistonId == id).SingleOrDefault();
            return Json(data);

        }

        public IActionResult SaaHelsinki()
        {
            return View();
        }

        public IActionResult SaaPaikallinen()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
