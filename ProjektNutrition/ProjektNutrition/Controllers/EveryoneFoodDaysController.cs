﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjektNutrition.Models;
using Microsoft.AspNet.Identity;

namespace ProjektNutrition.Controllers
{
    [Authorize]
    public class EveryoneFoodDaysController : Controller
    {
        private Entities db = new Entities();

        // GET: EveryoneFoodDays
        public ActionResult Index()
        {
            var id = User.Identity.GetUserId();
            var foodDay = db.FoodDay.Include(f => f.AspNetUsers).Where(model => model.AspNetUsers.Id == id);
            return View(foodDay.ToList());
        }

        // GET: EveryoneFoodDays/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodDay foodDay = db.FoodDay.Find(id);
            if (foodDay == null)
            {
                return HttpNotFound();
            }
            return View(foodDay);
        }

        // GET: EveryoneFoodDays/Create
        public ActionResult Create()
        {
            //ViewBag.Users_id = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: EveryoneFoodDays/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Users_id,date,sumCaloric,sumProtein,sumCarb,sumFat,description")] FoodDay foodDay)
        {
            if (ModelState.IsValid)
            {
                foodDay.Users_id = User.Identity.GetUserId();
                db.FoodDay.Add(foodDay);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Users_id = new SelectList(db.AspNetUsers, "Id", "Email", foodDay.Users_id);
            return View(foodDay);
        }

        // GET: EveryoneFoodDays/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodDay foodDay = db.FoodDay.Find(id);
            if (foodDay == null)
            {
                return HttpNotFound();
            }
            return View(foodDay);
        }

        // POST: EveryoneFoodDays/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Users_id,date,sumCaloric,sumProtein,sumCarb,sumFat,description")] FoodDay foodDay)
        {
            foodDay.Users_id = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Entry(foodDay).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Users_id = new SelectList(db.AspNetUsers, "Id", "Email", foodDay.Users_id);
            return View(foodDay);
        }

        // GET: EveryoneFoodDays/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodDay foodDay = db.FoodDay.Find(id);
            if (foodDay == null)
            {
                return HttpNotFound();
            }
            return View(foodDay);
        }

        // POST: EveryoneFoodDays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FoodDay foodDay = db.FoodDay.Find(id);
            if(foodDay.Product_FoodDay == null)
            {
                db.FoodDay.Remove(foodDay);
                db.SaveChanges();
            }
            
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
