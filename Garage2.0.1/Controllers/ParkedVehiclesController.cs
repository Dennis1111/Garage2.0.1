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
        public ActionResult Index(string regNr="")
        {
            return View(db.ParkedVehicle.ToList());
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

        /*
        public ActionResult Checkout()
        {
            return View();
        }*/


        // GET: ParkedVehicles/Delete/5
        public ActionResult Checkout(string id)
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

        /*
        // GET: ParkedVehicles/Details/5
        [HttpPost]
        public ActionResult Checkout(string id)
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
        }*/

        //GET: Receipt
        //[HttpPost]
        //public ActionResult Receipt(String nameToFind)
        public ActionResult Receipt(ReceiptViewModel receiptViewModel)
        {
            /*
            if (nameToFind == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkedVehicle parkedVehicle = db.ParkedVehicle.Find(nameToFind);

            if (parkedVehicle == null)
            {
                return HttpNotFound();
            }

            var checkOutTime = DateTime.Now;
            var parkedTime = checkOutTime.Subtract(parkedVehicle.ParkingTime);
            var hours = (int)parkedTime.TotalHours;
            var pricePerHour = 10;

            ReceiptViewModel receiptViewModel = new ReceiptViewModel()
            {
                RegistrationNumber = parkedVehicle.RegistrationNumber,
                ParkingTime = parkedVehicle.ParkingTime,
                CheckOutTime = checkOutTime,
                Hours = (int)parkedTime.TotalHours,
                Price = hours * pricePerHour
            };*/
            return View(receiptViewModel);          
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
            if (ModelState.IsValid)
            {
                parkedVehicle.ParkingTime = DateTime.Now;
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
                db.Entry(parkedVehicle).State = EntityState.Modified;
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

        /*
        // POST: ParkedVehicles/Delete/5
        [HttpPost, ActionName("Checkout")]
        [ValidateAntiForgeryToken]
        public ActionResult CheckoutConfirmed(string id)
        {
            ParkedVehicle parkedVehicle = db.ParkedVehicle.Find(id);
            db.ParkedVehicle.Remove(parkedVehicle);
            db.SaveChanges();
            return RedirectToAction("Index");
        }*/

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
            var hours = (int)parkedTime.TotalHours;
            var pricePerHour = 10;

            ReceiptViewModel receiptViewModel = new ReceiptViewModel()
            {
                RegistrationNumber = parkedVehicle.RegistrationNumber,
                ParkingTime = parkedVehicle.ParkingTime,
                CheckOutTime = checkOutTime,
                Hours = (int)parkedTime.TotalHours,
                Price = hours * pricePerHour
            };
            return RedirectToAction("Receipt",receiptViewModel);
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
