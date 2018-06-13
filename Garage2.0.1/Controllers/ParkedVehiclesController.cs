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
        private List<SelectListItem> vehicleTypeSelectList;
        private List<SelectListItem> columnSelectList;

        private List<SelectListItem> GetVehicleTypeSelectList()
        {
            if (vehicleTypeSelectList == null)
            {//var
                vehicleTypeSelectList = new List<SelectListItem>();
                //vehicleTypeSelectList.Add(new SelectListItem() { Text = "Any", Value = "0" });
                // int count = 1;

                foreach (var type in db.VehicleTypes)
                {
                    //vehicleTypeSelectList.Add(new SelectListItem() { Text = type.Type, Value = count.ToString() });
                    vehicleTypeSelectList.Add(new SelectListItem() { Text = type.Type, Value = type.Type });

                }
            }
            return vehicleTypeSelectList;
        }

        private List<SelectListItem> GetColumnSelectList()
        {
            if (columnSelectList == null)
            { 
                columnSelectList = new List<SelectListItem>();
                columnSelectList.Add(new SelectListItem() { Text = "Any", Value = "Any" });
                columnSelectList.Add(new SelectListItem() { Text = "Owner", Value = "Owner" });
                columnSelectList.Add(new SelectListItem() { Text = "Vehicle Type", Value = "VehicleType" });
                columnSelectList.Add(new SelectListItem() { Text = "Registration Number", Value = "RegNr" });
            }
            return columnSelectList;
        }

        private Member GetMember(string firstName, string lastName)
        {
            var query = db.Member.Where(m => m.FirstName.ToLower() == firstName.ToLower() && m.LastName.ToLower() == lastName.ToLower());
            if (query.Any())
                return query.FirstOrDefault();
            else
                return null;
        }

        public ActionResult Start()
        {
            return View();
        }

        // GET: ParkedVehicles
        [HttpGet]
        public ActionResult Index(string SelectedColumn, string ascending, string searchName, string selectedVehicleType)
        {
            IQueryable<ParkedVehicle> parkedVehicles;
            switch (SelectedColumn)
            {
                case "Owner":
                    var splitted = searchName.Split(' ');
                    if (splitted.Length != 2)
                    {
                        parkedVehicles = db.ParkedVehicle;
                        break;
                    }
                    var FirstName = splitted[0];
                    var LastName = splitted[1];
                    var member = db.Member.Where(m => m.FirstName.ToLower() == FirstName.ToLower() && m.LastName.ToLower() == LastName.ToLower());
                    var userFound = (member.Count() == 0);
                    parkedVehicles = db.ParkedVehicle.Where(v => v.MembersId == member.FirstOrDefault().Id);
                    break;
                case "RegNr":
                    parkedVehicles = (!String.IsNullOrEmpty(searchName)) ? db.ParkedVehicle.Where(v => v.RegistrationNumber.Equals(searchName)) : db.ParkedVehicle;
                    parkedVehicles = !Ascending(ViewBag.Ascending) ? parkedVehicles.OrderByDescending(v => v.RegistrationNumber) : parkedVehicles.OrderBy(v => v.RegistrationNumber);
                    break;
                default:
                    parkedVehicles = (!String.IsNullOrEmpty(searchName)) ? db.ParkedVehicle.Where(v => v.VehicleType.Type.Equals(searchName)) : db.ParkedVehicle;
                    parkedVehicles = !Ascending(ViewBag.Ascending) ?
                        parkedVehicles.OrderByDescending(v => v.VehicleType.Type) : parkedVehicles.OrderBy(v => v.VehicleType.Type);
                    break;
                    /*case "Color":
                        parkedVehicles = (!String.IsNullOrEmpty(searchName)) ? db.ParkedVehicle.Where(v => v.Color.Equals(searchName)) : db.ParkedVehicle;
                        parkedVehicles = !Ascending(ViewBag.Ascending) ?
                            parkedVehicles.OrderByDescending(v => v.Color) : parkedVehicles.OrderBy(v => v.Color);
                        break;
                    case "Brand":
                        parkedVehicles = (!String.IsNullOrEmpty(searchName)) ? db.ParkedVehicle.Where(v => v.Brand.Equals(searchName)) : db.ParkedVehicle;
                        parkedVehicles = !Ascending(ViewBag.Ascending) ? parkedVehicles.OrderByDescending(v => v.Brand) : parkedVehicles.OrderBy(v => v.Brand);
                        break;*/
                    /*case "Wheels":
                        parkedVehicles = (!String.IsNullOrEmpty(searchName)) ? db.ParkedVehicle.Where(v => v.Wheels.ToString().Equals(searchName)) : db.ParkedVehicle;
                        parkedVehicles = !Ascending(ViewBag.Ascending) ? parkedVehicles.OrderByDescending(v => v.Wheels) : parkedVehicles.OrderBy(v => v.Wheels);
                        break;*/
                    /*default:
                        parkedVehicles = (!String.IsNullOrEmpty(searchName)) ? db.ParkedVehicle.Where(v => v.ParkingTime.ToString().Contains(searchName)) : db.ParkedVehicle;
                        parkedVehicles = !Ascending(ViewBag.Ascending) ?
                            parkedVehicles.OrderByDescending(v => v.ParkingTime) : parkedVehicles.OrderBy(v => v.ParkingTime);
                        break;*/
            }

            ParkedVehiclesViewModel model = new ParkedVehiclesViewModel
            {
                SearchName = searchName,
                SortOrder = ascending,
                //Column = column,
                SelectedColumn = SelectedColumn,
                ColumnSelectList = GetColumnSelectList(),
                ParkedVehicles = parkedVehicles
            };
            return View(model);
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
            ParkVehicleViewModel model = new ParkVehicleViewModel()
            {
                VehicleTypeSelectList = db.VehicleTypes,    //GetVehicleTypeSelectList(),
                MemberFound = false,
                Post = false
            };
            //ViewBag.
            return View(model);
        }

        // POST: ParkedVehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstName,LastName,RegistrationNumber,Color,Brand,Wheels,ParkingTime,SelectedVehicleTypeId")] ParkVehicleViewModel model)
        {            
            if (db.ParkedVehicle.Any(v => v.RegistrationNumber == model.RegistrationNumber))
            {
                ModelState.AddModelError("RegistrationNumber", "registration number exist");
                model.RegNrTaken = true;
                //return View(model);
            }

            var foundMember = GetMember(model.FirstName, model.LastName);
            if (foundMember == null)
            {
                model.MemberFound = false;
                return View(model);
            }
            model.MemberFound = true;

            if (ModelState.IsValid)
            {
                ParkedVehicle parkedVehicle = new ParkedVehicle
                {                    
                    RegistrationNumber = model.RegistrationNumber.ToUpper(),
                    Color = model.Color,
                    Brand = model.Brand,
                    Wheels = model.Wheels,
                    ParkingTime = DateTime.Now,
                    MembersId = foundMember.Id,
                    VehicleTypeId = model.SelectedVehicleTypeId//db.VehicleTypes.Where(v => v.Type == SelectedVehicleType).FirstOrDefault().Id;
                    
                };
                db.ParkedVehicle.Add(parkedVehicle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
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

        public ActionResult Receipt(ReceiptViewModel receipt)
        {
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