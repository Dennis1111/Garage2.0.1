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

        private List<SelectListItem> GetVehicleTypeSelectList()
        {
            List<SelectListItem> vehicleTypeSelectList = new List<SelectListItem>();
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
            List<SelectListItem> columnSelectList = new List<SelectListItem>();
            columnSelectList = new List<SelectListItem>();
            //columnSelectList.Add(new SelectListItem() { Text = "Any", Value = "Any" });
            columnSelectList.Add(new SelectListItem() { Text = "Owner", Value = "Owner" });
            columnSelectList.Add(new SelectListItem() { Text = "Vehicle Type", Value = "VehicleType" });
            columnSelectList.Add(new SelectListItem() { Text = "Registration Number", Value = "RegNr" });
            return columnSelectList;
        }

        private List<SelectListItem> GetSortingSelectList()
        {
            List<SelectListItem> sortSelectList = new List<SelectListItem>();
            sortSelectList = new List<SelectListItem>();
            sortSelectList.Add(new SelectListItem() { Text = "Ascending", Value = "Ascending" });
            sortSelectList.Add(new SelectListItem() { Text = "Descending", Value = "Descending" });
            return sortSelectList;
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
        //public ActionResult Index(string SelectedColumn, string ascending, string searchName, string selectedVehicleType)
        public ActionResult Index()
        {
            IQueryable<ParkedVehicle> parkedVehicles = db.ParkedVehicle;

            ParkedVehiclesViewModel model = new ParkedVehiclesViewModel
            {
                //SearchName = searchName,
                //SortOrder = ascending,
                //SelectedColumn = SelectedColumn,
                ColumnSelectList = GetColumnSelectList(),
                ParkedVehicles = parkedVehicles
            };
            return View(model);
        }

        // GET: ParkedVehicles
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Index(string SelectedColumn, string ascending, string searchName, string selectedVehicleType)
        public ActionResult Index([Bind(Include = "SearchName,SelectedSorting,SelectedColumn")] ParkedVehiclesViewModel model)
        {
            IQueryable<ParkedVehicle> parkedVehicles;
            switch (model.SelectedColumn)
            {
                case "Owner":
                    var splitted = model.SearchName.Split(' ');
                    string FirstName, LastName;
                    if (splitted.Length != 2)
                    {
                        FirstName = "";
                        LastName = "";
                        parkedVehicles = db.ParkedVehicle;
                        break;
                    }
                    else
                    {
                        FirstName = splitted[0];
                        LastName = splitted[1];
                    }
                    var member = db.Member.Where(m => m.FirstName.ToLower() == FirstName.ToLower() && m.LastName.ToLower() == LastName.ToLower());
                    var userFound = (member.Count() == 0);
                    parkedVehicles = db.ParkedVehicle.Where(v => v.MembersId == member.FirstOrDefault().Id);
                    parkedVehicles = model.SelectedSorting.Equals("Descending") ? parkedVehicles.OrderByDescending(v => v.RegistrationNumber) : parkedVehicles.OrderBy(v => v.RegistrationNumber);
                    break;
                case "RegNr":
                    parkedVehicles = (!String.IsNullOrEmpty(model.SearchName)) ? db.ParkedVehicle.Where(v => v.RegistrationNumber.Equals(model.SearchName)) : db.ParkedVehicle;
                    parkedVehicles = model.SelectedSorting.Equals("Descending") ? parkedVehicles.OrderByDescending(v => v.RegistrationNumber) : parkedVehicles.OrderBy(v => v.RegistrationNumber);
                    break;
                default:
                    parkedVehicles = (!String.IsNullOrEmpty(model.SearchName)) ? db.ParkedVehicle.Where(v => v.VehicleType.Type.Equals(model.SearchName)) : db.ParkedVehicle;
                    parkedVehicles = model.SelectedSorting.Equals("Descending") ?
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
            model.ColumnSelectList = GetColumnSelectList();
            model.ParkedVehicles = parkedVehicles;
            return View(model);
        }

        public ActionResult Statistics()
        {
            //var result = IEnumerable<Tuple<string, int>> GroupByVehicleType()
            var list = db.ParkedVehicle.ToList();
            var query = list.GroupBy(c => c.GetType().Name).Select(c => new Tuple<string, int>(c.Key, c.Count()));
            return View(new StatisticsModelView() { Statistics = query });
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
            return View(model);
        }

        // POST: ParkedVehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstName,LastName,RegistrationNumber,Color,Brand,Wheels,ParkingTime,SelectedVehicleTypeId")] ParkVehicleViewModel model)
        {
            model.Post = true;
            model.VehicleTypeSelectList = db.VehicleTypes;
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
                    VehicleTypeId = model.SelectedVehicleTypeId
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
            ParkVehicleViewModel model = new ParkVehicleViewModel
            {
                RegistrationNumber = parkedVehicle.RegistrationNumber,
                Brand = parkedVehicle.Brand,
                Color = parkedVehicle.Color,
                Wheels = parkedVehicle.Wheels,
                VehicleTypeSelectList = db.VehicleTypes
            };
            return View(model);
        }

        // POST: ParkedVehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RegistrationNumber,Type,Color,Brand,Wheels,SelectedVehicleTypeId")] ParkVehicleViewModel model)
        {
            ParkedVehicle parkedVehicle = new ParkedVehicle
            {
                RegistrationNumber = model.RegistrationNumber.ToUpper(),
                Color = model.Color,
                Brand = model.Brand,
                Wheels = model.Wheels,
                VehicleTypeId = model.SelectedVehicleTypeId
            };

            if (ModelState.IsValid)
            {
                db.Entry(parkedVehicle).State = EntityState.Modified;
                db.Entry(parkedVehicle).Property(x => x.ParkingTime).IsModified = false;
                db.Entry(parkedVehicle).Property(x => x.MembersId).IsModified = false;
                //db.Entry(parkedVehicle).Property(x => x.VehicleTypeId).IsModified = false;
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
            var member = db.Member.FirstOrDefault(m => m.Id == parkedVehicle.MembersId);
            //We are guaranteed to find a member in database unless deleted while parking which should be prevented..
            //var foundMember = GetMember(model.FirstName, model.LastName);

            db.ParkedVehicle.Remove(parkedVehicle);
            db.SaveChanges();
            var checkOutTime = DateTime.Now;
            var parkedTime = checkOutTime.Subtract(parkedVehicle.ParkingTime);
            var totalMinutes = (int)parkedTime.TotalMinutes;
            int minutes = totalMinutes;
            int hours = 0;
            while (minutes >= 60)
            {
                hours++;
                minutes -= 60;
            }
            var pricePerMinute = 0.2;

            ReceiptViewModel receiptViewModel = new ReceiptViewModel()
            {
                FirstName = member.FirstName,
                LastName = member.LastName,
                MinutePrice = pricePerMinute,
                RegistrationNumber = parkedVehicle.RegistrationNumber,
                ParkingTime = parkedVehicle.ParkingTime,
                CheckOutTime = checkOutTime,
                Hours = hours,
                Minutes = minutes,
                Price = totalMinutes * pricePerMinute
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