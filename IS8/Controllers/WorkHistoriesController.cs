using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IS8.Models;

namespace IS8.Controllers
{
    public class WorkHistoriesController : Controller
    {
        private IS8_DBEntities db = new IS8_DBEntities();

        // GET: WorkHistories
        public ActionResult Index(string searchBy, string search)
        {
            if (searchBy == "CompanyName")
            {
                return View(db.WorkHistories.Where(x => x.CompanyName.StartsWith(search) || search == null).ToList());
            }
            else
            {
                return View(db.WorkHistories.Where(x => x.TitleName.StartsWith(search) || search == null).ToList());
            }
        }
        //public ActionResult Index()
        //{
        //    var workHistories = db.WorkHistories.Include(w => w.Company).Include(w => w.Title).Include(w => w.User);
        //    return View(workHistories.ToList());
        //}

        // GET: WorkHistories/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkHistory workHistory = db.WorkHistories.Find(id);
            if (workHistory == null)
            {
                return HttpNotFound();
            }
            return View(workHistory);
        }

        // GET: WorkHistories/Create
        public ActionResult Create()
        {
            ViewBag.CompanyName = new SelectList(db.Companies, "CompanyName", "CompanyName");
            ViewBag.TitleName = new SelectList(db.Titles, "TitleName", "TitleName");
            ViewBag.MNumber = new SelectList(db.Users, "MNumber", "LastName");
            return View();
        }

        // POST: WorkHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MNumber,CompanyName,TitleName,StartDate,EndDate,FTE")] WorkHistory workHistory)
        {
            if (ModelState.IsValid)
            {
                db.WorkHistories.Add(workHistory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CompanyName = new SelectList(db.Companies, "CompanyName", "CompanyName", workHistory.CompanyName);
            ViewBag.TitleName = new SelectList(db.Titles, "TitleName", "TitleName", workHistory.TitleName);
            ViewBag.MNumber = new SelectList(db.Users, "MNumber", "LastName", workHistory.MNumber);
            return View(workHistory);
        }

        // GET: WorkHistories/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkHistory workHistory = db.WorkHistories.Find(id);
            if (workHistory == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyName = new SelectList(db.Companies, "CompanyName", "CompanyName", workHistory.CompanyName);
            ViewBag.TitleName = new SelectList(db.Titles, "TitleName", "TitleName", workHistory.TitleName);
            ViewBag.MNumber = new SelectList(db.Users, "MNumber", "LastName", workHistory.MNumber);
            return View(workHistory);
        }

        // POST: WorkHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MNumber,CompanyName,TitleName,StartDate,EndDate,FTE")] WorkHistory workHistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workHistory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyName = new SelectList(db.Companies, "CompanyName", "CompanyName", workHistory.CompanyName);
            ViewBag.TitleName = new SelectList(db.Titles, "TitleName", "TitleName", workHistory.TitleName);
            ViewBag.MNumber = new SelectList(db.Users, "MNumber", "LastName", workHistory.MNumber);
            return View(workHistory);
        }

        // GET: WorkHistories/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkHistory workHistory = db.WorkHistories.Find(id);
            if (workHistory == null)
            {
                return HttpNotFound();
            }
            return View(workHistory);
        }

        // POST: WorkHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            WorkHistory workHistory = db.WorkHistories.Find(id);
            db.WorkHistories.Remove(workHistory);
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
