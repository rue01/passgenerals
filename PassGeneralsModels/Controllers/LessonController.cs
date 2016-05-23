//using Microsoft.AspNet.Identity;
//using PassGeneralsModels.Models;
//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//namespace PassGeneralsModels.Controllers
//{
//    [Authorize]
//    public class LessonController : Controller
//    {
//        //private ApplicationDbContext db = System.Web.HttpContext.Current.GetOwinContext().Get<ApplicationDbContext>();

//        //[Authorize(Roles = "Admin")]
//        public ActionResult Index()
//        {
//            ViewBag.LessonTypes = new SelectList(db.LessonTypes, "LessonId", "LessonType");
//            return View();
//        }

//        // GET: Lessons/Details/5
//        public ActionResult Details()
//        {
//            var userId = User.Identity.GetUserId();
//            //var lesson = db.Lessons.Where(c => c.ApplicationUserId == userId).First();
//            //return View(lesson);
//            return View();
//        }

//        //POST: Lessons/Details/5
//        //[HttpPost]
//        //public ActionResult Details(Lesson lesson)
//        //{
//        //    if (ModelState.IsValid)
//        //    {
//        //        db.Entry(lesson).State = EntityState.Modified;
//        //        db.SaveChanges();
//        //        return RedirectToAction("Index");
//        //    }

//        //    return View("Details", lesson);
//        //}

//        // GET: Lessons/Create
//        public ActionResult Create()
//        {
//            List<SelectListItem> lesson = new List<SelectListItem>();
//            lesson.Add(new SelectListItem { Text = "Select", Value = "Select" });
//            lesson.Add(new SelectListItem { Text = "New Learners", Value = "New Learners" });
//            lesson.Add(new SelectListItem { Text = "Regular Learners", Value = "Regular Learners" });
//            lesson.Add(new SelectListItem { Text = "Refresher Course", Value = "Refresher Course" });
//            lesson.Add(new SelectListItem { Text = "Pass Plus Course", Value = "Pass Plus Course" });
//            ViewData["LessonType"] = lesson;
//            return View();
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create(Lesson lesson)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Lessons.Add(lesson);
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            return View("Details", lesson);
//        }

//        // GET: Lessons/Edit/5
//        public ActionResult Edit()
//        {
//            //var userId = User.Identity.GetUserId();
//            //var lesson = db.Lessons.Where(c => c.ApplicationUserId == userId).First();
//            List<SelectListItem> lesson = new List<SelectListItem>();
//            lesson.Add(new SelectListItem { Text = "Select", Value = "Select" });
//            lesson.Add(new SelectListItem { Text = "New Learners", Value = "New Learners" });
//            lesson.Add(new SelectListItem { Text = "Regular Learners", Value = "Regular Learners" });
//            lesson.Add(new SelectListItem { Text = "Refresher Course", Value = "Refresher Course" });
//            lesson.Add(new SelectListItem { Text = "Pass Plus Course", Value = "Pass Plus Course" });
//            ViewData["LessonType"] = lesson;
//            return View();
//        }
      
//        // POST: Customer/Edit/5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include = "LessonType, DateOfLesson, DurationOfLesson")]Lesson lesson)
//        {
//            db.SaveChanges();
//            //if (ModelState.IsValid)
//            //{
//            //    db.Entry(lesson).State = EntityState.Modified;
//            //    db.SaveChanges();
//            //    return RedirectToAction("Index");
//            //}

//            return View("Details", lesson);
//        }
//    }
//}