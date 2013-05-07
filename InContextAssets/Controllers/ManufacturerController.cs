using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InContextAssets.Models;
using InContextAssets.DAL;

namespace InContextAssets.Controllers
{ 
    public class ManufacturerController : Controller
    {
        private InContextAssetsContext db = new InContextAssetsContext();

        //
        // GET: /Manufacturer/

        public ViewResult Index(string sortOrder)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name desc" : "";

            var manufacturers = from m in db.Manufacturers
                                select m;
            if (!String.IsNullOrEmpty(sortOrder) && sortOrder == "Name desc")
	        {
                manufacturers = manufacturers.OrderByDescending(m => m.Name);
	        }
            else
            {
                manufacturers = manufacturers.OrderBy(m => m.Name);
            }            

            return View(manufacturers.ToList());
        }

        //
        // GET: /Manufacturer/Details/5

        public ViewResult Details(int id)
        {
            Manufacturer manufacturer = db.Manufacturers.Find(id);
            return View(manufacturer);
        }

        //
        // GET: /Manufacturer/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Manufacturer/Create

        [HttpPost]
        public ActionResult Create(Manufacturer manufacturer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Manufacturers.Add(manufacturer);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes.");
            }

            return View(manufacturer);
        }
        
        //
        // GET: /Manufacturer/Edit/5
 
        public ActionResult Edit(int id, bool? saveChangesError)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Unable to save changes.";
            }
            Manufacturer manufacturer = db.Manufacturers.Find(id);
            return View(manufacturer);
        }

        //
        // POST: /Manufacturer/Edit/5

        [HttpPost]
        public ActionResult Edit(Manufacturer manufacturer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(manufacturer).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                return RedirectToAction("Edit", new System.Web.Routing.RouteValueDictionary {
                    { "id", manufacturer.ManufacturerID},
                    { "saveChangesError", true} });
            }
            return View(manufacturer);
        }

        //
        // GET: /Manufacturer/Delete/5
 
        public ActionResult Delete(int id, bool? saveChangesError)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Unable to save changes.";
            }
            Manufacturer manufacturer = db.Manufacturers.Find(id);
            return View(manufacturer);
        }

        //
        // POST: /Manufacturer/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Manufacturer manufacturer = db.Manufacturers.Find(id);
                db.Manufacturers.Remove(manufacturer);
                db.SaveChanges();
            }
            catch (DataException)
            {
                return RedirectToAction("Delete", new System.Web.Routing.RouteValueDictionary {
                    { "id", id},
                    { "saveChangesError", true} });
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}