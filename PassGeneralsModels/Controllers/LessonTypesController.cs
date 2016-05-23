using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PassGeneralsModels.Models;

namespace PassGeneralsModels.Controllers
{
    public class LessonTypesController : Controller
    {
        private ApplicationDbContext db = System.Web.HttpContext.Current.GetOwinContext().Get<ApplicationDbContext>();

        // GET: LessonTypes
        public ActionResult Index()
        {
            var lessonTypes = db.LessonTypes.Include(l => l.lesson);
            return View(lessonTypes.ToList());
        }

        // GET: LessonTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LessonTypes lessonTypes = db.LessonTypes.Find(id);
            if (lessonTypes == null)
            {
                return HttpNotFound();
            }
            return View(lessonTypes);
        }

        // GET: LessonTypes/Create
        public ActionResult Create()
        {
            ViewBag.LessonType = new SelectList("1", "2", "3", "4");
            return View();
        }

        // POST: LessonTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LessonId")] LessonTypes lessonTypes)
        {
            if (ModelState.IsValid)
            {
                db.LessonTypes.Add(lessonTypes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LessonId = new SelectList("1", "2", "3", "4");
            return View(lessonTypes);
        }

        // GET: LessonTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LessonTypes lessonTypes = db.LessonTypes.Find(id);
            if (lessonTypes == null)
            {
                return HttpNotFound();
            }
            ViewBag.LessonId = new SelectList(db.Lessons, "LessonId", "LessonType", lessonTypes.LessonId);
            return View(lessonTypes);
        }

        // POST: LessonTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,LessonType,LessonId")] LessonTypes lessonTypes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lessonTypes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LessonId = new SelectList(db.Lessons, "LessonId", "LessonType", lessonTypes.LessonId);
            return View(lessonTypes);
        }

        // GET: LessonTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LessonTypes lessonTypes = db.LessonTypes.Find(id);
            if (lessonTypes == null)
            {
                return HttpNotFound();
            }
            return View(lessonTypes);
        }

        // POST: LessonTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LessonTypes lessonTypes = db.LessonTypes.Find(id);
            db.LessonTypes.Remove(lessonTypes);
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
