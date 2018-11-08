using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarInsurance.Models;

namespace CarInsurance.Controllers
{
    public class InsureeController : Controller
    {
        private InsuranceEntities db = new InsuranceEntities();

        // GET: Insuree
        public ActionResult Index()
        {
            return View(db.Insurees.ToList());
        }

        //public ActionResult Submit(string FirstName, string LastName, string EmailAddress, DateTime DateOfBirth, 
        //    int CarYear, string CarMake, string CarModel, bool DUI, int SpeedingTickets, bool CoverageType)
        //{
        //    decimal TotalMonthlyPrice = 50;
        //    if (DateTime.Now.Year - DateOfBirth.Year < 18)
        //    {
        //        TotalMonthlyPrice += 100;
        //    }
        //    else if (DateTime.Now.Year - DateOfBirth.Year < 25 || DateTime.Now.Year - DateOfBirth.Year > 100)
        //    {
        //        TotalMonthlyPrice += 25;
        //    }
        //    if (CarYear < 2000 || CarYear > 2015)
        //    {
        //        TotalMonthlyPrice += 25;

        //    }
        //    if (CarMake == "Porsche")
        //    {
        //        TotalMonthlyPrice += 25;

        //    }
        //    if (CarMake == "Porsche" && CarModel == "911 Carrera")
        //    {
        //        TotalMonthlyPrice += 25;

        //    }
        //    TotalMonthlyPrice = (TotalMonthlyPrice + (10 * SpeedingTickets));
        //    if (DUI)
        //    {
        //        TotalMonthlyPrice = (TotalMonthlyPrice + (TotalMonthlyPrice / 4));
        //    }
        //    if (CoverageType)
        //    {
        //        TotalMonthlyPrice = (TotalMonthlyPrice + (TotalMonthlyPrice / 2));

        //    }

        //    return View(TotalMonthlyPrice);
        //}
        //public ActionResult Submit(Insuree i)
        //{
        //    decimal TotalMonthlyPrice = 50m;
        //    if (DateTime.Now.Year - i.DateOfBirth.Year < 18)
        //    {
        //        TotalMonthlyPrice += 100m;
        //    }
        //    else if(DateTime.Now.Year - i.DateOfBirth.Year < 25 || DateTime.Now.Year - i.DateOfBirth.Year > 100)
        //    {
        //        TotalMonthlyPrice += 25m;
        //    }
        //    if(i.CarYear < 2000 || i.CarYear > 2015)
        //    {
        //        TotalMonthlyPrice += 25m;

        //    }
        //    if(i.CarMake == "Porsche")
        //    {
        //        TotalMonthlyPrice += 25m;

        //    }
        //    if (i.CarMake == "Porsche" && i.CarModel == "911 Carrera")
        //    {
        //        TotalMonthlyPrice += 25m;

        //    }
        //    TotalMonthlyPrice = (TotalMonthlyPrice + (10 * i.SpeedingTickets));
        //    if (i.DUI)
        //    {
        //        TotalMonthlyPrice = (TotalMonthlyPrice + (TotalMonthlyPrice / 4));
        //    }
        //    if (i.CoverageType)
        //    {
        //        TotalMonthlyPrice = (TotalMonthlyPrice + (TotalMonthlyPrice / 2));

        //    }

        //    return View(TotalMonthlyPrice); 
        //}

        // GET: Insuree/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insuree insuree = db.Insurees.Find(id);
            if (insuree == null)
            {
                return HttpNotFound();
            }
            return View(insuree);
        }

        // GET: Insuree/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Insuree/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,DUI,SpeedingTickets,CoverageType,Quote")] Insuree insuree)
        {
            if (ModelState.IsValid)
            {
                insuree.Quote = 50m;
                int age = DateTime.Now.Year - insuree.DateOfBirth.Year;
                if(age < 18)
                {
                    insuree.Quote += 100m;
                }
                else if(age < 25 || age > 100)
                {
                    insuree.Quote += 25m;

                }

                if(insuree.CarMake.ToLower() == "porsche")
                {
                    insuree.Quote += 25m;

                }
                if (insuree.CarMake.ToLower() == "porsche" && insuree.CarModel.ToLower() == "911 carrera")
                {
                    insuree.Quote += 25m;

                }
                insuree.Quote = (insuree.Quote + (10m * insuree.SpeedingTickets));

                if (insuree.DUI)
                {
                    insuree.Quote = (insuree.Quote + (insuree.Quote / 4));
                }

                if (insuree.CoverageType)
                {
                    insuree.Quote = (insuree.Quote + (insuree.Quote / 2));
                }


                db.Insurees.Add(insuree);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(insuree);
        }

        // GET: Insuree/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insuree insuree = db.Insurees.Find(id);
            if (insuree == null)
            {
                return HttpNotFound();
            }
            return View(insuree);
        }

        // POST: Insuree/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,DUI,SpeedingTickets,CoverageType,Quote")] Insuree insuree)
        {
            if (ModelState.IsValid)
            {
                db.Entry(insuree).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(insuree);
        }

        // GET: Insuree/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insuree insuree = db.Insurees.Find(id);
            if (insuree == null)
            {
                return HttpNotFound();
            }
            return View(insuree);
        }

        // POST: Insuree/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Insuree insuree = db.Insurees.Find(id);
            db.Insurees.Remove(insuree);
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
