using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BankaRacun_EF.Models;

namespace BankaRacun_EF.Controllers
{
    public class UplatnicaController : Controller
    {
        private BankaDBContext db = new BankaDBContext();

        // GET: Uplatnica
        public ActionResult Index()
        {
            var uplatnice = db.Uplatnice.Include(u => u.Racun);
            return View(uplatnice.ToList());
        }

        // GET: Uplatnica/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uplatnica uplatnica = db.Uplatnice.Find(id);
            if (uplatnica == null)
            {
                return HttpNotFound();
            }
            return View(uplatnica);
        }

        // GET: Uplatnica/Create
        public ActionResult Create()
        {
            ViewBag.RacunId = new SelectList(db.Racuni, "RacunId", "NosilacRacuna");
            return View();
        }

        // POST: Uplatnica/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UplatnicaId,RacunId,Iznos,DatumPrometa,SvrhaUplate,Uplatilac,Hitno")] Uplatnica uplatnica)
        {
            if (ModelState.IsValid)
            {
                db.Uplatnice.Add(uplatnica);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RacunId = new SelectList(db.Racuni, "RacunId", "NosilacRacuna", uplatnica.RacunId);
            return View(uplatnica);
        }

        // GET: Uplatnica/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uplatnica uplatnica = db.Uplatnice.Find(id);
            if (uplatnica == null)
            {
                return HttpNotFound();
            }
            ViewBag.RacunId = new SelectList(db.Racuni, "RacunId", "NosilacRacuna", uplatnica.RacunId);
            return View(uplatnica);
        }

        // POST: Uplatnica/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UplatnicaId,RacunId,Iznos,DatumPrometa,SvrhaUplate,Uplatilac,Hitno")] Uplatnica uplatnica)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uplatnica).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RacunId = new SelectList(db.Racuni, "RacunId", "NosilacRacuna", uplatnica.RacunId);
            return View(uplatnica);
        }

        // GET: Uplatnica/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uplatnica uplatnica = db.Uplatnice.Find(id);
            if (uplatnica == null)
            {
                return HttpNotFound();
            }
            return View(uplatnica);
        }

        // POST: Uplatnica/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Uplatnica uplatnica = db.Uplatnice.Find(id);
            db.Uplatnice.Remove(uplatnica);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
