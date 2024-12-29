using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using tourism.Models;

namespace tourism.Controllers
{
    public class RoomsController : Controller
    {
        private Tourism_DB1Entities db = new Tourism_DB1Entities();

        // GET: Rooms
        public ActionResult Index()
        {
            var rooms = db.Rooms.Include(r => r.Hotel);
            return View(rooms.ToList());
        }

        // GET: Rooms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        // GET: Rooms/Create
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.Hotels, "Id", "H_Name");
            return View();
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "r_Id,R_Name,R_Description,R_Image1,R_Image2,R_Image3,R_Image4,R_Image5,R_Image6,Id,Number_Of_Beds,R_Breadth,R_Price")] Room room, HttpPostedFileBase file, HttpPostedFileBase file2, HttpPostedFileBase file3, HttpPostedFileBase file4,  HttpPostedFileBase file6)
        {
            if (ModelState.IsValid)
            {

                //..\\Images\\
                if (file != null)
                {
                    file.SaveAs(HttpContext.Server.MapPath("~/Content/Image/For_Reser/") + file.FileName);

                    room.R_Image1 = file.FileName;

                }

               if (file2 != null)
               {
                   file2.SaveAs(HttpContext.Server.MapPath("~/Content/Image/For_Reser/") + file2.FileName);
                   room.R_Image2 = file2.FileName;
               }
               if (file3 != null)
               {
                   file3.SaveAs(HttpContext.Server.MapPath("~/Content/Image/For_Reser/") + file3.FileName);
                   room.R_Image3 = file3.FileName;
               }
               if (file4 != null)
               {
                   file4.SaveAs(HttpContext.Server.MapPath("~/Content/Image/For_Reser/") + file4.FileName);
                   room.R_Image4 = file4.FileName;
               }
               //if (file5 != null)
               //{
               //    file5.SaveAs(HttpContext.Server.MapPath("~/Content/Image/For_Reser/") + file5.FileName);
               //    room.R_Image5 = file5.FileName;
               //}


               //if (file6 != null && file.ContentLength > 0)
               //{
               //     var fileName = Path.GetFileName(file6.FileName);
               //     if (fileName != null)
               //     {
               //         var path = Path.Combine(Server.MapPath("~/Content/Image/For_Reser/"), fileName);
               //         file6.SaveAs(path);
               //     }


               //     file6.SaveAs(HttpContext.Server.MapPath("~/Content/Image/For_Reser/") + file6.FileName);
               //    room.R_Image6 = file6.FileName;
               //}







                db.Rooms.Add(room);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.Hotels, "Id", "H_Name", room.Id);
            return View(room);
        }


        //---------------------------------------------------------------------------============================

        public ActionResult IndexBooking( )
        {
            IQueryable<Room> room = (from r in db.Rooms
                                     
                                     select r);
            var rooms = db.Rooms.Include(r => r.Hotel);
            return View(rooms);
        }


        [HttpGet]
        public ActionResult Booking(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.Hotels, "Id", "H_Name", room.Id);
            return View(room);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Booking([Bind(Include = "r_Id,Id, Name,Mail.Phone,Passport,A_Date,A_Date,D_Date,Any_Question")] Room room,int id)
        {
            if (ModelState.IsValid)
            {
                
                db.Rooms.Add(room);
                db.SaveChanges();
                return RedirectToAction("IndexBooking");
            }

            ViewBag.Id = new SelectList(db.Hotels, "Id", "H_Name", room.Id);
            return View(room);

        }
        //===============================================================================================

            // GET: Rooms/Edit/5
            public ActionResult Edit(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Room room = db.Rooms.Find(id);
                if (room == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Id = new SelectList(db.Hotels, "Id", "H_Name", room.Id);
                return View(room);
            }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "r_Id,R_Name,R_Description,R_Image1,R_Image2,R_Image3,R_Image4,R_Image5,R_Image6,Id,Number_Of_Beds,R_Breadth,R_Price")] Room room)
        {
            if (ModelState.IsValid)
            {
                db.Entry(room).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.Hotels, "Id", "H_Name", room.Id);
            return View(room);
        }

        // GET: Rooms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Room room = db.Rooms.Find(id);
            db.Rooms.Remove(room);
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
