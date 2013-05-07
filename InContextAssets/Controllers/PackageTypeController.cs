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
    public class PackageTypeController : Controller
    {
        private InContextAssetsContext db = new InContextAssetsContext();

        //
        // GET: /PackageType/

        public ViewResult Index(string sortOrder)
        {
            ViewBag.PackageTypeSortParm = String.IsNullOrEmpty(sortOrder) ? "Type desc" : "";

            var packageTypes = from p in db.PackageTypes
                               select p;

            if (!String.IsNullOrEmpty(sortOrder) && sortOrder == "Type desc")
            {
                packageTypes = packageTypes.OrderByDescending(p => p.Type);
            }
            else
            {
                packageTypes = packageTypes.OrderBy(p => p.Type);
            }
            return View(packageTypes.ToList());
        }

        //
        // GET: /PackageType/Details/5

        public ViewResult Details(int id)
        {
            PackageType packagetype = db.PackageTypes.Find(id);
            return View(packagetype);
        }

        //
        // GET: /PackageType/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /PackageType/Create

        [HttpPost]
        public ActionResult Create(PackageType packagetype)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.PackageTypes.Add(packagetype);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes.");
            }

            return View(packagetype);
        }
        
        //
        // GET: /PackageType/Edit/5
 
        public ActionResult Edit(int id, bool? saveChangesError)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Unable to save changes.";
            }
            PackageType packagetype = db.PackageTypes.Find(id);
            return View(packagetype);
        }

        //
        // POST: /PackageType/Edit/5

        [HttpPost]
        public ActionResult Edit(PackageType packagetype)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(packagetype).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                return RedirectToAction("Edit", new System.Web.Routing.RouteValueDictionary {
                    { "id", packagetype.PackageTypeID},
                    { "saveChangesError", true} });
            }
            return View(packagetype);
        }

        //
        // GET: /PackageType/Delete/5
 
        public ActionResult Delete(int id, bool? saveChangesError)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Unable to save changes.";
            }
            PackageType packagetype = db.PackageTypes.Find(id);
            return View(packagetype);
        }

        //
        // POST: /PackageType/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                PackageType packagetype = db.PackageTypes.Find(id);
                db.PackageTypes.Remove(packagetype);
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