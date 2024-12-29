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
    public class HotelsController : Controller
    {
        private Tourism_DB1Entities db = new Tourism_DB1Entities();

        // GET: Hotels
        public ActionResult Index(string SearchString)
        {
            var hotels = db.Hotels.Include(h => h.Rate);

             hotels = from s in db.Hotels
              select s;

            if (!String.IsNullOrEmpty(SearchString))
            {
                hotels = hotels.Where(s => s.H_Name.Contains(SearchString));
                 
            }


            return View(hotels.ToList());
        }








        // GET: Hotels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel hotel = db.Hotels.Find(id);
            if (hotel == null)
            {
                return HttpNotFound();
            }
            return View(hotel);
        }











        // GET: Hotels/Create
        public ActionResult Create()
        {
            ViewBag.R_Id = new SelectList(db.Rates, "R_Id", "Gov_Name");
            return View();
        }

        // POST: Hotels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,H_Name,R_Id,H_Image,H_Image2,H_Image3,H_Image4,H_Image5,rating,Performance,H_Description,H_Description1,H_Description2,H_Description3,H_Description4,H_Description5,H_Description6,H_Location,H_Video,R_Rating")] Hotel hotel, HttpPostedFileBase file, HttpPostedFileBase file2, HttpPostedFileBase file3, HttpPostedFileBase file4, HttpPostedFileBase file5)
        {
            if (ModelState.IsValid)
            {


                //..\\Images\\
                if (file != null)
                {
                    file.SaveAs(HttpContext.Server.MapPath("~/Content/Image/For_Reser/") + file.FileName);
                    hotel.H_Image = file.FileName;
                }
                if (file2 != null)
                {
                    file2.SaveAs(HttpContext.Server.MapPath("~/Content/Image/For_Reser/") + file2.FileName);
                    hotel.H_Image2 = file2.FileName;
                }
                if (file3 != null)
                {
                    file3.SaveAs(HttpContext.Server.MapPath("~/Content/Image/For_Reser/") + file3.FileName);
                    hotel.H_Image3 = file3.FileName;
                }
                if (file4 != null)
                {
                    file4.SaveAs(HttpContext.Server.MapPath("~/Content/Image/For_Reser/") + file4.FileName);
                    hotel.H_Image4 = file4.FileName;
                }
                if (file5 != null)
                {
                    file5.SaveAs(HttpContext.Server.MapPath("~/Content/Image/For_Reser/") + file5.FileName);
                    hotel.H_Image5 = file5.FileName;
                }

                db.Hotels.Add(hotel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.R_Id = new SelectList(db.Rates, "R_Id", "Gov_Name", hotel.R_Id);
            return View(hotel);
        }

        // GET: Hotels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel hotel = db.Hotels.Find(id);
            if (hotel == null)
            {
                return HttpNotFound();
            }
            ViewBag.R_Id = new SelectList(db.Rates, "R_Id", "Gov_Name", hotel.R_Id);
            return View(hotel);
        }

        // POST: Hotels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,H_Name,R_Id,H_Image,H_Image2,H_Image3,H_Image4,H_Image5,rating,Performance,H_Description,H_Description1,H_Description2,H_Description3,H_Description4,H_Description5,H_Description6,H_Location,H_Video,R_Rating")] Hotel hotel, HttpPostedFileBase file, HttpPostedFileBase file2, HttpPostedFileBase file3, HttpPostedFileBase file4, HttpPostedFileBase file5)
        {
            if (ModelState.IsValid)
            {

                //..\\Images\\
                if (file != null)
                {
                    file.SaveAs(HttpContext.Server.MapPath("~/Content/Image/For_Reser/") + file.FileName);
                    hotel.H_Image = file.FileName;
                }
                if (file2 != null)
                {
                    file2.SaveAs(HttpContext.Server.MapPath("~/Content/Image/For_Reser/") + file2.FileName);
                    hotel.H_Image2 = file2.FileName;
                }
                if (file3 != null)
                {
                    file3.SaveAs(HttpContext.Server.MapPath("~/Content/Image/For_Reser/") + file3.FileName);
                    hotel.H_Image3 = file3.FileName;
                }
                if (file4 != null)
                {
                    file4.SaveAs(HttpContext.Server.MapPath("~/Content/Image/For_Reser/") + file4.FileName);
                    hotel.H_Image4 = file4.FileName;
                }
                if (file5 != null)
                {
                    file5.SaveAs(HttpContext.Server.MapPath("~/Content/Image/For_Reser/") + file5.FileName);
                    hotel.H_Image5 = file5.FileName;
                }







                db.Entry(hotel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.R_Id = new SelectList(db.Rates, "R_Id", "Gov_Name", hotel.R_Id);
            return View(hotel);
        }

        // GET: Hotels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel hotel = db.Hotels.Find(id);
            if (hotel == null)
            {
                return HttpNotFound();
            }
            return View(hotel);
        }

        // POST: Hotels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Hotel hotel = db.Hotels.Find(id);
            db.Hotels.Remove(hotel);
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
