using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Garage2._0._1.DataAccessLayer;
using Garage2._0._1.Models;

namespace Garage2._0._1.Controllers
{
    public class ParkedVehiclesController : Controller
    {
        private RegisterContext db = new RegisterContext();

        // GET: ParkedVehicles
        [HttpGet]
        public ActionResult Index(string column, string ascending, string searchName)
        {
            ViewBag.SearchName = searchName;
            ViewBag.Ascending = ascending == null ? "Ascending" : ascending;
            ViewBag.Column = column == null ? "RegistrationNumber" : column;
            //Switch order when user press same column twice
            if (ViewBag.Column == column)
                ViewBag.Ascending = Toggle(ascending);
            IQueryable<ParkedVehicle> parkedVehicles;
            switch (column)
            {
                case "RegistrationNumber":
                    parkedVehicles = (!String.IsNullOrEmpty(searchName)) ? db.ParkedVehicle.Where(v => v.RegistrationNumber.Equals(searchName)) : db.ParkedVehicle;
                    parkedVehicles = !Ascending(ViewBag.Ascending) ? parkedVehicles.OrderByDescending(v => v.RegistrationNumber) : parkedVehicles.OrderBy(v => v.RegistrationNumber);
                    break;/*
                case "Type":
                    parkedVehicles = (!String.IsNullOrEmpty(searchName)) ? db.ParkedVehicle.Where(v => v.Type.ToString().Equals(searchName)) : db.ParkedVehicle;
                    parkedVehicles = !Ascending(ViewBag.Ascending) ?
                        parkedVehicles.OrderByDescending(v => v.VehicleType) : parkedVehicles.OrderBy(v => v.Type);
                    break;*/
                case "Color":
                    parkedVehicles = (!String.IsNullOrEmpty(searchName)) ? db.ParkedVehicle.Where(v => v.Color.Equals(searchName)) : db.ParkedVehicle;
                    parkedVehicles = !Ascending(ViewBag.Ascending) ?
                        parkedVehicles.OrderByDescending(v => v.Color) : parkedVehicles.OrderBy(v => v.Color);
                    break;
                case "Brand":
                    parkedVehicles = (!String.IsNullOrEmpty(searchName)) ? db.ParkedVehicle.Where(v => v.Brand.Equals(searchName)) : db.ParkedVehicle;
                    parkedVehicles = !Ascending(ViewBag.Ascending) ? parkedVehicles.OrderByDescending(v => v.Brand) : parkedVehicles.OrderBy(v => v.Brand);
                    break;
                case "Wheels":
                    parkedVehicles = (!String.IsNullOrEmpty(searchName)) ? db.ParkedVehicle.Where(v => v.Wheels.ToString().Equals(searchName)) : db.ParkedVehicle;
                    parkedVehicles = !Ascending(ViewBag.Ascending) ? parkedVehicles.OrderByDescending(v => v.Wheels) : parkedVehicles.OrderBy(v => v.Wheels);
                    break;
                default:
                    parkedVehicles = (!String.IsNullOrEmpty(searchName)) ? db.ParkedVehicle.Where(v => v.ParkingTime.ToString().Contains(searchName)) : db.ParkedVehicle;
                    parkedVehicles = !Ascending(ViewBag.Ascending) ?
                        parkedVehicles.OrderByDescending(v => v.ParkingTime) : parkedVehicles.OrderBy(v => v.ParkingTime);
                    break;
            }                       
            return View(parkedVehicles.ToList());
        }

        private Boolean Ascending(string sorting)
        {
            return sorting == "Ascending";
        }

        private string Toggle(string sorting)
        {
            return sorting == "Ascending" ? "Descending" : "Ascending";
        }

        
        // GET: ParkedVehicles/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkedVehicle parkedVehicle = db.ParkedVehicle.Find(id);
            if (parkedVehicle == null)
            {
                return HttpNotFound();
            }
            return View(parkedVehicle);
        }       

        // GET: ParkedVehicles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ParkedVehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RegistrationNumber,Type,Color,Brand,Wheels,ParkingTime")] ParkedVehicle parkedVehicle)
        {
            ViewBag.RegNrTaken = false;
            if (db.ParkedVehicle.Where(v => v.RegistrationNumber == parkedVehicle.RegistrationNumber).Count() > 0)
            {
                ViewBag.RegNrTaken = true;
                return View(parkedVehicle);
            }
            if (ModelState.IsValid)
            {
                parkedVehicle.ParkingTime = DateTime.Now;
                parkedVehicle.RegistrationNumber = parkedVehicle.RegistrationNumber.ToUpper();
                db.ParkedVehicle.Add(parkedVehicle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(parkedVehicle);
        }

        // GET: ParkedVehicles/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkedVehicle parkedVehicle = db.ParkedVehicle.Find(id);
            if (parkedVehicle == null)
            {
                return HttpNotFound();
            }
            return View(parkedVehicle);
        }

        // POST: ParkedVehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RegistrationNumber,Type,Color,Brand,Wheels,ParkingTime")] ParkedVehicle parkedVehicle)
        {            
            if (ModelState.IsValid)
            {   
                //parkedVehicle.ParkingTime = db.ParkedVehicle.AsNoTracking().Where(v => v.RegistrationNumber == parkedVehicle.RegistrationNumber).First().ParkingTime;
                db.Entry(parkedVehicle).State = EntityState.Modified;
                db.Entry(parkedVehicle).Property(x => x.ParkingTime).IsModified = false;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(parkedVehicle);
        }

        // GET: ParkedVehicles/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkedVehicle parkedVehicle = db.ParkedVehicle.Find(id);
            if (parkedVehicle == null)
            {
                return HttpNotFound();
            }
            return View(parkedVehicle);
        }

        // POST: ParkedVehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ParkedVehicle parkedVehicle = db.ParkedVehicle.Find(id);
            db.ParkedVehicle.Remove(parkedVehicle);
            db.SaveChanges();
            var checkOutTime = DateTime.Now;
            var parkedTime = checkOutTime.Subtract(parkedVehicle.ParkingTime);
            var minutes = (int)parkedTime.TotalMinutes;
            var pricePerMinute = 0.02;

            ReceiptViewModel receiptViewModel = new ReceiptViewModel()
            {
                RegistrationNumber = parkedVehicle.RegistrationNumber,
                ParkingTime = parkedVehicle.ParkingTime,
                CheckOutTime = checkOutTime,
                Minutes = (int)parkedTime.TotalHours,
                Price = minutes * pricePerMinute
            };
            return RedirectToAction("Receipt", receiptViewModel);
        }

        public ActionResult Receipt(ReceiptViewModel receipt) {
            return View(receipt);
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