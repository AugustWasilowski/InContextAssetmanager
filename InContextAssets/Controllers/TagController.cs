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
    public class TagController : Controller
    {
        private InContextAssetsContext db = new InContextAssetsContext();

        //
        // GET: /Tag/

        public ViewResult Index(string sortOrder)
        {
            ViewBag.KeySortParm = String.IsNullOrEmpty(sortOrder) ? "Key desc" : "";
            ViewBag.ValueSortParm = sortOrder == "Value" ? "Value desc" : "Value";

            var tags = from t in db.Tags
                       select t;
            switch (sortOrder)
            {
                case "Key desc":
                    tags = tags.OrderByDescending(t => t.Key);
                    break;
                case "Key":
                    tags = tags.OrderBy(t => t.Key);
                    break;
                case "Value desc":
                    tags = tags.OrderByDescending(t => t.Value);
                    break;
                case "Value":
                    tags = tags.OrderBy(t => t.Value);
                    break;
                default:
                    tags = tags.OrderBy(t => t.Key);
                    break;
            }

            return View(tags.ToList());
        }

        //
        // GET: /Tag/Details/5

        public ViewResult Details(int id)
        {
            Tag tag = db.Tags.Find(id);
            return View(tag);
        }

        //
        // GET: /Tag/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Tag/Create

        [HttpPost]
        public ActionResult Create(Tag tag)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Tags.Add(tag);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes.");
            }
            return View(tag);
        }
        
        //
        // GET: /Tag/Edit/5
 
        public ActionResult Edit(int id, bool? saveChangesError)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Unable to save changes.";
            }
            Tag tag = db.Tags.Find(id);
            return View(tag);
        }

        //
        // POST: /Tag/Edit/5

        [HttpPost]
        public ActionResult Edit(Tag tag)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(tag).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                return RedirectToAction("Edit", new System.Web.Routing.RouteValueDictionary {
                    { "id", tag.TagID},
                    { "saveChangesError", true} });
            }
            return View(tag);
        }

        //
        // GET: /Tag/Delete/5
 
        public ActionResult Delete(int id, bool? saveChangesError)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Unable to save changes.";
            }
            Tag tag = db.Tags.Find(id);
            return View(tag);
        }

        //
        // POST: /Tag/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Tag tag = db.Tags.Find(id);
                db.Tags.Remove(tag);
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