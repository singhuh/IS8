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
    public class InterviewsController : Controller
    {
        private IS8_DBEntities db = new IS8_DBEntities();
        public ActionResult Index(string searchBy, string search)
        {
            if (searchBy == "InterviewCompany")
            {
                return View(db.Interviews.Where(x => x.InterviewCompany.StartsWith(search) || search == null).ToList());
            }
            else
            {
                return View(db.Interviews.Where(x => x.MNumber.EndsWith(search) || search == null).ToList());
            }
        }

        // GET: Interviews
        //public ActionResult Index()
        //{
        //    var interviews = db.Interviews.Include(i => i.User);
        //    return View(interviews.ToList());
        //}

        // GET: Interviews/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Interview interview = db.Interviews.Find(id);
            if (interview == null)
            {
                return HttpNotFound();
            }
            return View(interview);
        }

        // GET: Interviews/Create
        public ActionResult Create()
        {
            ViewBag.MNumber = new SelectList(db.Users, "MNumber", "LastName");
            return View();
        }

        // POST: Interviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MNumber,InterviewCompany,InterviewDate,Offer")] Interview interview)
        {
            if (ModelState.IsValid)
            {
                db.Interviews.Add(interview);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MNumber = new SelectList(db.Users, "MNumber", "LastName", interview.MNumber);
            return View(interview);
        }

        // GET: Interviews/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Interview interview = db.Interviews.Find(id);
            if (interview == null)
            {
                return HttpNotFound();
            }
            ViewBag.MNumber = new SelectList(db.Users, "MNumber", "LastName", interview.MNumber);
            return View(interview);
        }

        // POST: Interviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MNumber,InterviewCompany,InterviewDate,Offer")] Interview interview)
        {
            if (ModelState.IsValid)
            {
                db.Entry(interview).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MNumber = new SelectList(db.Users, "MNumber", "LastName", interview.MNumber);
            return View(interview);
        }

        // GET: Interviews/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Interview interview = db.Interviews.Find(id);
            if (interview == null)
            {
                return HttpNotFound();
            }
            return View(interview);
        }

        // POST: Interviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Interview interview = db.Interviews.Find(id);
            db.Interviews.Remove(interview);
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
