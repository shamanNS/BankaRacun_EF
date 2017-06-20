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
    public class RacunController : Controller
    {
        private BankaDBContext db = new BankaDBContext();

        // GET: Racun
        public ActionResult Index()
        {
            var racuni = db.Racuni.Include(r => r.Uplatnice).ToList();

            foreach (Racun r in racuni)
            {
                r.IzracunajSaldo();
            }
            return View(racuni);
        }



        /*
         Googlati još i/ili pitati na predavanjima kako osigurati da
         ovaj metod ne može da se pozove direktnim kucanjem odgovarajućeg
         URL-a, a da pri tom i dalje može da se pozove KLIKOM NA 
         "Aktiviraj/Deaktiviraj LINK u Index View-u.

            Probao sam kombinacije ChildActionOnly, Html.Action/Html.RenderAction (jer prave
            sub-request, ali problem je što oni treba nešto da vrate/renderuju u view)
            i sa AntiForgeryToken (sa logikom "zaobilaženjem View-a faliće taj token i neće dozvoliti izvršavanje Action-a) 
            ali posle video da to izgleda ima efekta samo za 
            HttpPost tj slanje pomoću forme, a meni trenutno action "gađa"
            najobičniji Html.ActionLink

            Možda probati neki refactor da jedan action poziva drugi koji je private i/ili
            obeležen sa [NonAction] atributom
             */
        [HttpGet]
        //[ChildActionOnly]
        //[ValidateAntiForgeryToken]
        public ActionResult ToggleActiveStatus(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var racun = db.Racuni.Find(id);
            racun.Aktivan = !racun.Aktivan;
            db.SaveChanges();


            return RedirectToAction("Index");
        }

        // GET: Racun/Details/5
       
        
        public ActionResult Details(int? id, string filter)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Racun racun = db.Racuni.Include(r => r.Uplatnice).SingleOrDefault(r => r.RacunId == id);
            if (racun == null)
            {
                return HttpNotFound();
            }


            if (filter != null && (filter == "uplate" || filter == "isplate"))
            {
                List<Uplatnica> filtriraneUplatnice = null;
                switch (filter)
                {
                    case "uplate":
                        filtriraneUplatnice = racun.Uplatnice.Where(u => u.Iznos > 0).ToList();
                        break;
                    case "isplate":
                        filtriraneUplatnice = racun.Uplatnice.Where(u => u.Iznos < 0).ToList();
                        break;
                    default:
                        break;
                }
                
                racun.Uplatnice = filtriraneUplatnice;
            }
            
            return View(racun);
        }

        // GET: Racun/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Racun/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RacunId,NosilacRacuna,BrojRacuna,Aktivan,OnlineBanking")] Racun racun)
        {
            if (ModelState.IsValid)
            {
                db.Racuni.Add(racun);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(racun);
        }

        // GET: Racun/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Racun racun = db.Racuni.Find(id);
            if (racun == null)
            {
                return HttpNotFound();
            }
            return View(racun);
        }

        // POST: Racun/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RacunId,NosilacRacuna,BrojRacuna,Aktivan,OnlineBanking")] Racun racun)
        {
            if (ModelState.IsValid)
            {
                db.Entry(racun).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(racun);
        }

        // GET: Racun/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Racun racun = db.Racuni.Find(id);
            if (racun == null)
            {
                return HttpNotFound();
            }
            return View(racun);
        }

        // POST: Racun/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Racun racun = db.Racuni.Find(id);
            db.Racuni.Remove(racun);
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
