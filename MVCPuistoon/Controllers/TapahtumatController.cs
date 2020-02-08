using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCPuistoon.Models;
using System;
using System.Data;
using System.Linq;
using System.Net;
namespace MVCPuistoon.Controllers
{
    public class TapahtumatController : Controller
    {


        // GET: Tapahtumat
        public ActionResult Index()
        {
            using (PuistoonContext db = new PuistoonContext())
            {
                var tapahtuma = db.Tapahtumat.ToList();
                return View(tapahtuma);
            }
        }
        // GET: Tapahtumat/Details/5
        public ActionResult Details(int? id)
        {
            using (PuistoonContext db = new PuistoonContext())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var tapahtuma = db.Tapahtumat.SingleOrDefault(e => e.TapahtumanId == id);
                if (tapahtuma == null)
                {
                    return HttpNotFound();
                }
                return View(tapahtuma);
            }
        }

        // GET: Tapahtumat/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: Tapahtumat/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tapahtumat tapahtuma)
        {
            using (PuistoonContext db = new PuistoonContext())
                if (ModelState.IsValid)
                {
                    db.Tapahtumat.Add(tapahtuma);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            return View(tapahtuma);
        }
        // GET: Tapahtumat/Edit/5
        public ActionResult Edit(int? id)
        {
            using (PuistoonContext db = new PuistoonContext())
            {
                if (id == null)
                {
                    //return HttpNotFoundResult("Not Found");
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var tapahtuma = db.Tapahtumat.SingleOrDefault(e => e.TapahtumanId == id);
                if (tapahtuma == null)
                {
                    return HttpNotFound();
                }
                ViewBag.PuistonId = tapahtuma.PuistonId;
                ViewBag.KategorianId = tapahtuma.KategorianId;
                ViewBag.KäyttäjänId = tapahtuma.KäyttäjänId;


                return View(tapahtuma);
            }
        }
        private ActionResult HttpNotFound()
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(Tapahtumat tapahtuma)
        {
            using (PuistoonContext db = new PuistoonContext())
            {
                if (ModelState.IsValid)
                {
                    db.Entry(tapahtuma).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(tapahtuma);
            }
        }
     


        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            using (PuistoonContext db = new PuistoonContext())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                if (saveChangesError.GetValueOrDefault())
                {
                    ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
                }
                Tapahtumat tapahtumat = db.Tapahtumat.Find(id);
                if (tapahtumat == null)
                {
                    return HttpNotFound();
                }
                return View(tapahtumat);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            using (PuistoonContext db = new PuistoonContext())
            {
                try
                {
                    Tapahtumat tapahtumat = db.Tapahtumat.Find(id);
                    db.Tapahtumat.Remove(tapahtumat);
                    db.SaveChanges();
                }
                catch (DataException/* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    return RedirectToAction("Delete", new { id = id, saveChangesError = true });
                }
                return RedirectToAction("Index");
            }
        }





    }


}