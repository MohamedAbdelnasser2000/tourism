using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using tourism.Models;

namespace tourism.Controllers
{
    public class BookingsController : Controller
    {
        private Tourism_DB1Entities db = new Tourism_DB1Entities();

        // GET: Bookings
        public ActionResult Index()
        {
            var bookings = db.Bookings.Include(b => b.Room).Include(b => b.Hotel);
            return View(bookings.ToList());
        }

        // GET: Bookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }









        //================================================



        // GET: Bookings/Create
        [HttpGet]
        public ActionResult Create()
        {
            //int? id
           //IQueryable<Room> room = (from r in db.Rooms
           //                           where r.r_Id == id
           //                           select r);

            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Booking booking = db.Bookings.Find(id);
            //if (booking == null)
            //{
            //    return HttpNotFound();
            //}

            // ViewBag.Rooms = room;
            ViewBag.r_Id = new SelectList(db.Rooms, "r_Id", "R_Name");

            ViewBag.Id = new SelectList(db.Hotels, "Id", "H_Name");

            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "B_Id,Name,Mail,Phone,Passport,A_Date,D_Date,Any_Question,r_Id,Id")] Booking booking, HttpPostedFileBase file,Room room)
        {

          
            if (ModelState.IsValid)
            {
                //..\\Images\\
                if (file != null)
                {
                    file.SaveAs(HttpContext.Server.MapPath("~/Content/Image/For_Reser/") + file.FileName);

                    booking.Passport = file.FileName;

                }

                db.Bookings.Add(booking);
                db.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("Message");
            }

            //ViewBag.Rooms = room ;
          ViewBag.r_Id = new SelectList(db.Rooms, "r_Id", "R_Name", booking.r_Id);
            ViewBag.Id = new SelectList(db.Hotels, "Id", "H_Name", booking.Id);
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.r_Id = new SelectList(db.Rooms, "r_Id", "R_Name", booking.r_Id);
            ViewBag.Id = new SelectList(db.Hotels, "Id", "H_Name", booking.Id);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "B_Id,Name,Mail,Phone,Passport,A_Date,D_Date,Any_Question,r_Id,Id")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }



            ViewBag.r_Id = new SelectList(db.Rooms, "r_Id", "R_Name", booking.r_Id);
            ViewBag.Id = new SelectList(db.Hotels, "Id", "H_Name", booking.Id);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);
            db.Bookings.Remove(booking);
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

        public ActionResult Message()
        {
            return View();
        }
      
    }
}
