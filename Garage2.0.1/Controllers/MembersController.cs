﻿using System;
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
    public class MembersController : Controller
    {
        private RegisterContext db = new RegisterContext();

        // GET: Members
        public ActionResult Index()
        {
            return View(db.Member.ToList());
        }

        // GET: Members/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Member.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }



        // GET: Members/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName")] Member member)
        {
            if (db.Member.Any(m => m.FirstName.ToLower() == member.FirstName.ToLower() && m.LastName.ToLower() == member.LastName.ToLower()))
            {
                ModelState.AddModelError("FirstName", "User Already Exist");
                ModelState.AddModelError("LastName", "User Already Exist");
            }
            if (ModelState.IsValid)
            {

                db.Member.Add(member);

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(member);
        }

        // GET: Members/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Member.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(member);
        }

        // GET: Members/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Member.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }
        public ActionResult Search(string FirstName)
        {
            var member = db.Member.ToList();

            if (string.IsNullOrWhiteSpace(FirstName))
            {
                ModelState.AddModelError("Name", "Couldn't search for null");
                return View(member);
            }

            FirstName = FirstName.ToLower();
            var keywords = FirstName.Split(' ');

            if (keywords.Length == 1) member = member.Where(m => m.FirstName.ToLower() == FirstName || m.LastName.ToLower() == FirstName).ToList();
            if (keywords.Length == 2) member = member.Where(m => m.FirstName.ToLower() == keywords[0] && m.LastName.ToLower() == keywords[1]
                                                            || m.LastName.ToLower() == keywords[0] && m.FirstName.ToLower() == keywords[1]).ToList();
            if (keywords.Length > 2)
            {
                foreach (var item in keywords)
                {
                    var newmembers = new List<Member>();
                    newmembers.Concat(member.Where(m => m.FirstName.ToLower() == item || m.LastName.ToLower() == item).ToList());
                }
            }

            return View(member);
        }



        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Member member = db.Member.Find(id);
            db.Member.Remove(member);
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
