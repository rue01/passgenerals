using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PassGeneralsModels.Models;

namespace PassGeneralsModels.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        //private ApplicationDbContext db = System.Web.HttpContext.Current.GetOwinContext().Get<ApplicationDbContext>();

        [Authorize] //(Roles ="Admin")]
        public ActionResult Index(string sortOrder, string searchString)
        {
            var userId = User.Identity.GetUserId();

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "firstname_desc" : "";
            //ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            var customer = from c in db.Customers 
                           select c;
            if (!String.IsNullOrEmpty(searchString))
            {
                customer = customer.Where(c => c.CustomerFirstname.Contains(searchString)
                                       || c.CustomerLastname.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "firstname_desc":
                    customer = customer.OrderByDescending(c => c.CustomerFirstname);
                    break;
                case "lastname_asc":
                    customer  = customer.OrderBy(c => c.CustomerLastname);
                    break;
                //case "date_desc":
                //    customer = customer.OrderByDescending(s => s.EnrollmentDate);
                //    break;
                default:
                    customer = customer.OrderBy(c => c.CustomerFirstname);
                    break;
            }

            return View(customer.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details()
        {
            var User_Id = User.Identity.GetUserId();
            var customer = db.Customers.Where(c => c.ApplicationUserId == User_Id).First();
            return View(customer);
        }

        //Created a DetailsForAdmin page which should allow Admin to see details of any registered customer from Customer/Index
        [Authorize] //(Roles ="Admin")]
        public ActionResult DetailsForAdmin(int id)
        {
            var customer = db.Customers.Find(id);
            return View("Details", customer);
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerFirstname,CustomerLastname,CustomerAddress,CustomerPhone")]Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
         

            var userId = User.Identity.GetUserId();
            var customer = db.Customers.Where(c => c.ApplicationUserId == userId).First();
            if (customer == null)
            {
                return HttpNotFound();
            }

            return View(customer);
        }

        //// GET: Customer/Edit/5
        //[HttpPost]
        //public ActionResult Edit(Customer customer)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Customers.Add(customer);
        //        db.SaveChanges();
        //        return RedirectToAction("index");
        //    }
        //    return View(customer);
        //}

        //Created a EditForAdmin page which should allow Admin to see details of any registered customer from Customer/Index
        [Authorize] //(Roles ="Admin")]
        public ActionResult EditForAdmin(int id)
        {
            var customer = db.Customers.Find(id);
            return View("Edit", customer);
        }


        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Edit([Bind(Include = "CustomerId, CustomerFirstname, CustomerLastname, CustomerAddress,CustomerPhone")]Customer customer)
        {

            //var result = await UserManager.CreateAsync(user, model.Password);
            //if (result.Succeeded)
            //{
            //    UserManager.AddClaim(user.Id, new Claim(ClaimTypes.GivenName, model.CustomerFirstname));


           // var userId = await Microsoft.AspNet.Identity.UserManager.FindByIdAsync(User.Identity.GetUserId());

            if (ModelState.IsValid)
            //if (userId.Succeeded)
            {
                db.Entry(customer).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            //LOOK AT http://www.asp.net/mvc/overview/getting-started/getting-started-with-ef-using-mvc/implementing-basic-crud-functionality-with-the-entity-framework-in-asp-net-mvc-application TO DO A CUSTOMISED ERROR ON PAGE
            //return View(customer);
            return View("Details", customer);
        }


        // GET: Customer/Delete/5
        [Authorize(Roles ="Admin")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Customer/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection collection)
        {            
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            db.Customers.Remove(customer);
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
